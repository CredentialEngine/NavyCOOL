using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Solid.Models;
using ThisEntity = Solid.Models.Agency;
using ThisRequest = RA.Models.Input.OrganizationRequest;
using ThisRequestEntity = RA.Models.Input.Organization;
using RA.Models.Input;

using RServices = COOLTool.Services.RegistryServices;

namespace COOLTool.Services
{
	public class PublishOrganization : MappingHelpers
	{
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
		public string Publish(ThisEntity input, string publisherApiKey, ref bool isValid, ref List<string> messages, ref string crEnvelopeId, string community = "")
		{
			var request = new ThisRequest();
			request.DefaultLanguage = "en-US";

			//map from Agency record to the API organization request class
			MapToAssistant( input, request.Organization, ref messages );

			request.Community = community;
			//serialize the input (for logging)
			string jsoninput = JsonConvert.SerializeObject( request, GetJsonSettings() );
			string filePrefix = string.Format( "Organization_{0}", input.CA_AgencyID );
			Utilities.LoggingHelper.WriteLogFile( 5, filePrefix + "_raInput.json", jsoninput, "", false );

			#region  Authorization settings

			//if the current org is child org, will need to get parent org CTID
			request.PublishForOrganizationIdentifier = input.CTID;
			//if not staff, this will be the CTID for the publishing org
			//using apiKey now for this relationship
			//request.PublishByOrganizationIdentifier = myOrg.ctid;

			#endregion

			//format the payload
			string postBody = JsonConvert.SerializeObject( request, MappingHelpers.GetJsonSettings() );

			AssistantRequestHelper req = new AssistantRequestHelper()
			{
				EndpointType = "organization",
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
			crEnvelopeId = req.EnvelopeIdentifier ?? "";
			if ( !isValid )
			{
				//anything??
			}
			//globalMonitor.Payload = req.FormattedPayload;
			//return globalMonitor;
			return req.FormattedPayload;
		}

		public static void MapToAssistant(ThisEntity input, ThisRequestEntity output, ref List<string> messages)
		{
			//will there be any QA orgs?
			//if so, we will want a means to identify these
			//probably just default to credentialOrganization

			//if ( input.ISQAOrganization )
			//	output.Type = "QACredentialOrganization";
			//else
				output.Type = "CredentialOrganization";

			output.Name = input.CA_AgencyName;
			output.Description = input.Description;
			output.SubjectWebpage = input.CA_AgencyHomePageURL;
			//A ctid is required, and would have to be maintained by the organization for use in making updates to previously published organizations
			output.Ctid = input.CTID;
			//custom method for mapping a list of possible multiple types, to a list of strings (with valid concept terms)
			//if not valuesa, could have a temporary default of orgType:Business
			if ( input.AgentType != null && input.AgentType.Count() > 0 )
				output.AgentType = input.AgentType;
			else
				output.AgentType.Add( "orgType:Business" );
			//possibly could allow a default of agentSector:Public if no value
			output.AgentSectorType = !string.IsNullOrWhiteSpace(input.AgentSectorType) ? input.AgentSectorType : "agentSector:Public";

			var place = new RA.Models.Input.Place()
			{
				Address1 = input.CA_AgencyStreetAddress1,
				City = input.CA_AgencyCity,
				AddressRegion = input.CA_AgencyState,
				PostalCode = input.CA_AgencyZip,
				Country = !string.IsNullOrWhiteSpace( input.CA_AgencyCountry ) ? input.CA_AgencyCountry : "United States"
			};
			//a phone number is added to a contact point property in the place/address
			if (!string.IsNullOrWhiteSpace(input.CA_PhoneNumber))
			{
				var cp = new RA.Models.Input.ContactPoint
				{
					Name = "Contact"
				};
				cp.PhoneNumbers.Add( input.CA_PhoneNumber );
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
			

			//is there any QA data for the organization
		}
	}
}
