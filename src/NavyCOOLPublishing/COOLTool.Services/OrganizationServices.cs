using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using COOLTool.Services.Models.Input;
using InputRequest = COOLTool.Services.Models.Input.OrganizationRequest;
using ThisEntity = COOLTool.Services.Models.Input.Agency;
using OutputRequest = RA.Models.Input.OrganizationRequest;
using OutputRequestEntity = RA.Models.Input.Organization;
using RA.Models.Input;

using RServices = COOLTool.Services.RegistryServices;

namespace COOLTool.Services
{
	public class OrganizationServices : MappingHelpers
	{
		//test method



		/// <summary>
		/// Publish an agency record
		/// </summary>
		/// <param name="input">Agency record</param>
		/// <param name="publisherApiKey">Provided the ApiKey of the publisher (COOL)</param>
		/// <param name="owningOrgCtid">The CTID for the owning org could be in the agency recored or derived data. If not then this parameter could be used.</param>
		/// <param name="isValid">Returns true or false if publish fails.</param>
		/// <param name="messages">Returns any error message if publish fails. Note: warning messages can be returned even if isValid is true.</param>
		/// <param name="crEnvelopeId">The identifier from the registry is returned. It could b e stored to enable lookup by the envelopeId - or ignored.</param>
		/// <param name="community">Blank if using the default community, or navy</param>
		/// <returns>organization formatted as JSON-LD (Payload) from the registry</returns>
		/// <returns></returns>
		public string Publish(InputRequest input, RequestHelper helper)
		{
			List<string> messages = new List<string>();
			var request = new OutputRequest();
			request.DefaultLanguage = "en-US";

			string filePrefix = string.Format( "Organization_{0}", input.Agency.CTID );
			//save this for use in postman
			string coolInput = JsonConvert.SerializeObject( input, HelperServices.GetJsonSettings() );
			Utilities.LoggingHelper.WriteLogFile( 5, filePrefix + "_coolInput.json", coolInput, "", false );

			//=======================================================
			//map from Agency record to the API organization request class
			MapToAssistant( input.Agency, request.Organization, ref messages );

			//if the current org is child org, will need to get parent org CTID
			request.PublishForOrganizationIdentifier = input.OwningOrganizationCTID;

			request.Community = input.Community;
			//serialize the input (also for logging)
			string postBody = JsonConvert.SerializeObject( request, GetJsonSettings() );
			
			Utilities.LoggingHelper.WriteLogFile( 5, filePrefix + "_coolRaOutput.json", postBody, "", false );

			if ( messages.Count > 0 )
			{
				helper.SetMessages( messages );
				helper.IsValidRequest = false;
				return "";
			}

			//=======================================================
			AssistantRequestHelper req = new AssistantRequestHelper()
			{
				EndpointType = "organization",
				RequestType = "publish",
				CTID = request.PublishForOrganizationIdentifier,
				Identifier = filePrefix,
				InputPayload = postBody
			};
			req.PublisherApiKey = helper.ApiKey;
			//
			helper.IsValidRequest = new RServices().PublishRequest( req );
			//
			helper.SetMessages( req.Messages );
			helper.RegistryEnvelopeId = req.EnvelopeIdentifier ?? "";
			helper.GraphUrl = req.GraphUrl;
			helper.EnvelopeUrl = req.EnvelopeUrl;
			if ( !helper.IsValidRequest )
			{
				//anything??
			}
			return req.FormattedPayload;
		}

		public void MapToAssistant(ThisEntity input, OutputRequestEntity output, ref List<string> messages)
		{
			//will there be any QA orgs?
			//March 10, 2020 - confirmed there will not be any QA orgs, so can hard code.
			output.Type = "CredentialOrganization";

			output.Name = input.CA_AgencyName;
			output.Description = input.Description;
			output.SubjectWebpage = input.CA_AgencyHomePageURL;
			//A ctid is required, and would have to be maintained by the organization for use in making updates to previously published organizations
			if ( IsCtidValid( input.CTID, "organization.CTID", ref messages, true )) 
			{
				output.Ctid = input.CTID.ToLower();
			}

			//
			CurrentEntityName = output.Name;
			CurrentEntityType = "Organization";
			CurrentCtid = output.Ctid;
			//
			//custom method for mapping a list of possible multiple types, to a list of strings (with valid concept terms)
			//if not valuesa, could have a temporary default of orgType:Business
			if ( input.AgentType != null && input.AgentType.Count() > 0 )
				output.AgentType = input.AgentType;
			else
				output.AgentType.Add( "orgType:Business" );
			//possibly could allow a default of agentSector:Public if no value
			output.AgentSectorType = !string.IsNullOrWhiteSpace(input.AgentSectorType) ? input.AgentSectorType : "agentSector:Public";
			if ( !string.IsNullOrWhiteSpace( input.CA_AgencyAcronym ) )
				output.AlternateName.Add( input.CA_AgencyAcronym );

			var place = new RA.Models.Input.Place()
			{
				Address1 = input.CA_AgencyStreetAddress1,
				City = input.CA_AgencyCity,
				AddressRegion = input.CA_AgencyState,
				PostalCode = input.CA_AgencyZip,
				Country = !string.IsNullOrWhiteSpace( input.CA_AgencyCountry ) ? input.CA_AgencyCountry : "United States"
			};
			//a phone number is added to a contact point property in the place/address
			if (!string.IsNullOrWhiteSpace(input.CA_AgencyPhonePrimary))
			{
				var cp = new RA.Models.Input.ContactPoint
				{
					Name = "Contact"
				};
				cp.PhoneNumbers.Add( input.CA_AgencyPhonePrimary );
				place.ContactPoint.Add( cp );
			}
			output.Address.Add( place );

			//Agency.CA_AgencyContact (email or webpage)
			if ( !string.IsNullOrWhiteSpace( input.CA_AgencyContact ) )
			{
				if (input.CA_AgencyContact.IndexOf( "@" ) > 0 )
					output.Email.Add( input.CA_AgencyContact );
				else if( input.CA_AgencyContact.IndexOf( "http" ) == 0 )
				{
					//if a webpage, where to put it? Social media may be an option, or availabilityListing
					output.SocialMedia.Add( input.CA_AgencyContact );
				}
			}


			//is there any QA data for the organization - NO
			//consider
			//output.AlternateName = MapToStringList( input.AlternateName );
			//output.AlternativeIdentifier = MappingHelpers.AssignTextValueProfileListToList( input.AlternativeIdentifiers );
		}
	}
}
