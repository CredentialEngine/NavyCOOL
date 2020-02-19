using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Solid.Models;
using COOLCredential = Solid.Models.CredentialDTO;
using ThisRequest = RA.Models.Input.CredentialRequest;
using ThisRequestEntity = RA.Models.Input.Credential;
using RMI = RA.Models.Input;
using RServices = COOLTool.Services.RegistryServices;

namespace COOLTool.Services
{
	public class PublishCredential : MappingHelpers
	{
		public bool Publish(int credentialId, string publisherApiKey, ref List<string> messages, string community = "")
		{
			bool isValid = true;
			//get by credentialId and populate a credential DTO
			//TBD
			COOLCredential input = new COOLCredential()
			{
				CE_CertID = credentialId,
				CE_CertTitle = "My Test",
				CE_CertDescription = "My description",
				CE_CertAcronym = "Acro",
				CE_CertURL = "http://example.com/navyCool/1",
				CredentialStatusType = "Active",
				CredentialType = "License", 
				CTID = "ce-" + Guid.NewGuid().ToString().ToLower(),
				OwnedByOrganizationCTID = "ce-" + Guid.NewGuid().ToString().ToLower()
				//etc
			}
				;
			//call a method to fill out the credential DTO


			var payload = Publish( input, publisherApiKey, ref isValid, ref messages, community );

			return isValid;
		}

		/// <summary>
		/// Publish a credential
		/// </summary>
		/// <param name="input">Credential record</param>
		/// <param name="publisherApiKey">Provided the ApiKey of the publisher (COOL)</param>
		/// <param name="isValid">Returns true or false if publish fails.</param>
		/// <param name="messages">Returns any error message if publish fails. Note: warning messages can be returned even if isValid is true.</param>
		/// <param name="community">Blank if using the default community, or navy</param>
		/// <returns>Credential formatted as JSON-LD (Payload) from the registry</returns>
		public string Publish(COOLCredential input, string publisherApiKey, ref bool isValid, ref List<string> messages, string community = "")
		{
			var request = new ThisRequest();
			request.DefaultLanguage = "en-US";

			//map from COOL record to the API organization request class
			MapToAssistant( input, request.Credential, ref messages );

			//NOTE
			request.Community = community;
			//serialize the input (for logging)
			string jsoninput = JsonConvert.SerializeObject( request, GetJsonSettings() );
			//optional to save the input to a file
			string filePrefix = string.Format( "Credential_{0}", input.CE_CertTitle );
			Utilities.LoggingHelper.WriteLogFile( 5, filePrefix + "_raInput.json", jsoninput, "", false );

			#region  Authorization settings

			//the CTID for the owning org must be present or the publish will fail
			request.PublishForOrganizationIdentifier = input.OwnedByOrganizationCTID;

			#endregion

			//format the payload
			string postBody = JsonConvert.SerializeObject( request, MappingHelpers.GetJsonSettings() );

			AssistantRequestHelper req = new AssistantRequestHelper()
			{
				EndpointType = "credential",
				RequestType = "publish",
				CTID = request.PublishForOrganizationIdentifier,
				Identifier = filePrefix,
				InputPayload = postBody
			};

			if ( IsValidGuid( publisherApiKey ) )
			{
				req.PublisherApiKey = publisherApiKey;
			}

			isValid = new RServices().PublishRequest( req );
			messages.AddRange( req.Messages );

			var crEnvelopeId = req.EnvelopeIdentifier ?? "";
			if ( !isValid )
			{
				//anything??
			}

			return req.FormattedPayload;
		}

		public static void MapToAssistant(COOLCredential input, ThisRequestEntity output, ref List<string> messages)
		{
			//A ctid is required, and would have to be maintained by the organization for use in making updates to previously published credentials
			output.Ctid = input.CTID.ToLower();

			output.Name = input.CE_CertTitle;
			output.Description = input.CE_CertDescription;
			//SubjectWebpage will be a NavyCOOL url. 
			output.SubjectWebpage = input.CE_CertURL;
			//by agreement, this will be the URL on the provider site
			output.AvailableOnlineAt = input.AvailableOnlineAt;

			output.CredentialStatusType = string.IsNullOrWhiteSpace( input.CredentialStatusType ) ? "Active" : input.CredentialStatusType; 
			//may need a conversion here from Federal License to License, and National Credential to Certification
			if ( input.CredentialType == "State License" )
			{
				//require a Jurisdiction or reject?
				if (input.Jurisdiction == null || input.Jurisdiction.Count() == 0)
				{
					messages.Add( "Error (maybe) - a Jurisdiction must be provided for a State License." );
				}
				input.CredentialType = "License";
			} else if( input.CredentialType == "Federal License" )
			{
				//Not sure if a jurisdiction will be necessary?
				//should there be other useful info to indicate as a federal license?
				input.CredentialType = "License";
			}
			//
			output.CredentialType = input.CredentialType;

			//list of owning organization
			//use an organization reference for flexibity
			var orgRef = new RMI.OrganizationReference() { CTID = input.ProviderCTID };
			output.OwnedBy.Add( orgRef );
			//if owner always offers:
			output.OfferedBy.Add( orgRef );

			/// <summary>
			/// HasRating
			/// Rating related to this resource.
			/// URI to a Rating
			/// NOTE: API will accept just the CTID and properly format the URI based on the target environment
			/// </summary>
			output.HasRating = input.HasRating;
			


			//occupations
			//IF there are just Onet codes, then can provide them in the list property ONET_Codes
			output.ONET_Codes = input.ONET_Codes;
			//if there are additional occupations (without ONet codes), then these can be
			output.AlternativeOccupationType = input.AlternativeOccupations;

			//accreditations
			//**SAMPLE** - where a CTID is not known, or available
			output.AccreditedBy.Add( new RMI.OrganizationReference()
			{
				Name= "ANAB (ANSI)",
				SubjectWebpage= "https://anab.ansi.org/",
				Type = "QACredentialOrganization",
				Description = "The ANSI National Accreditation Board (ANAB) is a non-governmental organization that provides accreditation services and training to public- and private-sector organizations, serving the global marketplace."
			} );

			//condition profile(s)
			output.Requires = MapConditionProfiles( input.Requires, ref messages );

			//similar if there are recommended conditions
			output.Recommends = MapConditionProfiles( input.Recommends, ref messages );

			//financial assistance
			//**SAMPLE** where input has data
			foreach ( var item in input.FinancialAssistance )
			{
				var fa = new RMI.FinancialAssistanceProfile
				{
					Name = item.Name,
					Description = item.Description,
					SubjectWebpage = item.SubjectWebpage,
				};
				output.FinancialAssistance.Add( fa );
			}
			//
			if( input.Jurisdiction != null && input.Jurisdiction.Count > 0 )
			{
				output.Jurisdiction = MapJurisdictions( input.Jurisdiction, ref messages );
			}

			//Could include the contact information for the provider
			if( input.Addresses != null && input.Addresses.Count > 0 )
			{
				output.AvailableAt = FormatAvailableAtList( input.Addresses, ref messages );
			}

			//renewal
			//sample for 2 yeaers
			output.RenewalFrequency = new RMI.DurationItem()
			{
				Years = 2
			};
			//renewal conditions
			//list of condition profiles with renewal information
			//not sure of the input format
			//output.Renewal = MapConditionProfiles( input.Renewal );

			//others
			//keywords?
			output.Keyword = input.Keywords;

			//costs??
		}
	}
}
