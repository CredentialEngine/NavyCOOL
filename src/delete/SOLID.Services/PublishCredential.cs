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
using RegistryAssistantServices;
using RServices = RegistryAssistantServices.Services;

namespace COOLTool.Services
{
	public class PublishCredential : MappingHelpers
	{
		public bool Publish(int credentialId, string publisherApiKey, ref List<string> messages, string community = "")
		{
			bool isValid = true;
			//get and populate a credential DTO
			//TBD
			COOLCredential input = new COOLCredential();
			//call a method to fill out the credential DTO
			
			string owningOrgCtid = "??";

			var payload = Publish( input, publisherApiKey, owningOrgCtid, ref isValid, ref messages, community );

			return isValid;
		}

		/// <summary>
		/// Publish a credential
		/// </summary>
		/// <param name="input">Credential record</param>
		/// <param name="publisherApiKey">Provided the ApiKey of the publisher (COOL)</param>
		/// <param name="owningOrgCtid">The CTID for the owning org could be in the credential data. If not then this parameter could be used.</param>
		/// <param name="isValid">Returns true or false if publish fails.</param>
		/// <param name="messages">Returns any error message if publish fails. Note: warning messages can be returned even if isValid is true.</param>
		/// <param name="crEnvelopeId">The identifier from the registry is returned. It could b e stored to enable lookup by the envelopeId - or ignored.</param>
		/// <param name="community">Blank if using the default community, or navy</param>
		/// <returns>Credential formatted as JSON-LD (Payload) from the registry</returns>
		public string Publish(COOLCredential input, string publisherApiKey, string owningOrgCtid, ref bool isValid, ref List<string> messages, string community = "")
		{
			var request = new ThisRequest();
			request.DefaultLanguage = "en-US";

			//map from COOL record to the API organization request class
			MapToAssistant( input, request.Credential, ref messages );

			request.Community = community;
			//serialize the input (for logging)
			string jsoninput = JsonConvert.SerializeObject( request, GetJsonSettings() );
			string filePrefix = string.Format( "Credential_{0}", input.CE_CertTitle );
			Utilities.LoggingHelper.WriteLogFile( 5, filePrefix + "_raInput.json", jsoninput, "", false );

			#region  Authorization settings

			//if the current org is child org, will need to get parent org CTID
			request.PublishForOrganizationIdentifier = owningOrgCtid;
			//if not staff, this will be the CTID for the publishing org
			//using apiKey now for this relationship
			//request.PublishByOrganizationIdentifier = myOrg.ctid;

			#endregion

			//format the payload
			string postBody = JsonConvert.SerializeObject( request, MappingHelpers.GetJsonSettings() );

			AssistantRequestHelper req = new RegistryAssistantServices.AssistantRequestHelper()
			{
				EndpointType = "credential",
				RequestType = "publish",
				CTID = request.PublishForOrganizationIdentifier,
				Identifier = filePrefix,
				InputPayload = postBody
			};
			//AuthorizationToken = credentialEngineApiKey

			if ( IsValidGuid( publisherApiKey ) )
			{
				req.PublisherApiKey = publisherApiKey;
			}

			isValid = new RServices().PublishRequest( req );
			messages.AddRange( req.Messages );
			//ReportRelatedEntitiesToBePublished( ref messages );
			var crEnvelopeId = req.EnvelopeIdentifier ?? "";
			if ( !isValid )
			{
				//anything??
			}
			//globalMonitor.Payload = req.FormattedPayload;
			//return globalMonitor;
			return req.FormattedPayload;
		}

		public static void MapToAssistant(COOLCredential input, ThisRequestEntity output, ref List<string> messages)
		{
			//A ctid is required, and would have to be maintained by the organization for use in making updates to previously published credentials
			output.Ctid = input.CTID;

			output.Name = input.CE_CertTitle;
			output.Description = input.CE_CertDescription;
			output.SubjectWebpage = input.CE_CertURL;
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
			output.AvailableOnlineAt = input.AvailableOnlineAt;

			output.HasRating = input.HasRating;
			

			//list of owning organization
			//use an organization reference for flexibity
			var orgRef = new RMI.OrganizationReference() { CTID = input.ProviderCTID };
			output.OwnedBy.Add( orgRef );
			//if owner always offers:
			output.OfferedBy.Add( orgRef );

			//occupations
			//IF there are just Onet codes, then can provide them in the list property ONET_Codes
			output.ONET_Codes = input.ONET_Codes;
			//if there are additional occupations (without ONet codes), then these can be

			//accreditations
			output.AccreditedBy.Add( new RMI.OrganizationReference()
			{
				CTID = ""
			} );
			//condition profile(s)

			//financial assistance
			foreach( var item in input.FinancialAssistance )
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

			//
			if( input.Addresses != null && input.Addresses.Count > 0 )
			{
				output.AvailableAt = FormatAvailableAtList( input.Addresses, ref messages );
			}
		}
	}
}
