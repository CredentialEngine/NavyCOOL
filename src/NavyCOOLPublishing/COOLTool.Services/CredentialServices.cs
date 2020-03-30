using System.Collections.Generic;
using System.Linq;
using COOLTool.Services.Models.Input;

using Newtonsoft.Json;

using InputEntity = COOLTool.Services.Models.Input.Credential;
using InputRequest = COOLTool.Services.Models.Input.CredentialRequest;
using OutputRequest = RA.Models.Input.CredentialRequest;
using OutputRequestEntity = RA.Models.Input.Credential;
using RMI = RA.Models.Input;
using RServices = COOLTool.Services.RegistryServices;

namespace COOLTool.Services
{
	public class CredentialServices : MappingHelpers
	{

		/// <summary>
		/// Publish a credential
		/// </summary>
		/// <param name="input">Credential record</param>
		/// <param name="publisherApiKey">Provided the ApiKey of the publisher (COOL)</param>
		/// <param name="isValid">Returns true or false if publish fails.</param>
		/// <param name="messages">Returns any error message if publish fails. Note: warning messages can be returned even if isValid is true.</param>
		/// <param name="community">Blank if using the default community, or navy</param>
		/// <returns>Credential formatted as JSON-LD (Payload) from the registry</returns>
		public string Publish(InputRequest input, RequestHelper helper)
		{
			List<string> messages = new List<string>();
			var request = new OutputRequest();
			request.DefaultLanguage = "en-US";
			//
			string filePrefix = string.Format( "Credential_{0}", input.Credential.CTID );
			//save this for use in postman
			string coolInput = JsonConvert.SerializeObject( input, HelperServices.GetJsonSettings() );
			Utilities.LoggingHelper.WriteLogFile( 5, filePrefix + "_coolInput.json", coolInput, "", false );

			try
			{

				//map from COOL record to the API organization request class
				MapToAssistant( input.Credential, request.Credential, ref messages );

				//the CTID for the owning org must be present or the publish will fail
				request.PublishForOrganizationIdentifier = input.OwningOrganizationCTID;

				//NOTE
				request.Community = input.Community;
				//serialize the input (for logging)
				string postBody = JsonConvert.SerializeObject( request, GetJsonSettings() );
				//optional to save the input to a file
				Utilities.LoggingHelper.WriteLogFile( 5, filePrefix + "_coolRaOutput.json", postBody, "", false );

				if ( messages.Count > 0 )
				{
					helper.SetMessages( messages );
					helper.IsValidRequest = false;
					return "";
				}

				AssistantRequestHelper req = new AssistantRequestHelper()
				{
					EndpointType = "credential",
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
			catch ( System.Exception ex )
			{
				Utilities.LoggingHelper.LogError( ex, "CredentialServices.Publish", false );
				return "";
			}

			
		}

		public  void MapToAssistant(InputEntity input, OutputRequestEntity output, ref List<string> messages)
		{
			//A ctid is required, and would have to be maintained by the organization for use in making updates to previously published credentials
			
			if ( IsCtidValid( input.CTID, "Credential.CTID", ref messages, true ) )
			{
				output.Ctid = input.CTID.ToLower();
			}

			output.Name = input.CE_CertTitle;
			//
			CurrentEntityName = output.Name;
			CurrentEntityType = "Credential";
			CurrentCtid = output.Ctid;
			//
			output.Description = input.CE_CertDescription;
			//SubjectWebpage will be a NavyCOOL url. 
			output.SubjectWebpage = AssignValidUrlAsString( input.CE_CertURL, "Subject Webpage", ref messages, true );
			//by agreement, this will be the URL on the provider site
			output.AvailableOnlineAt = AssignValidUrlAsString( input.AvailableOnlineAt, "AvailableOnlineAt", ref messages, false );

			if ( !string.IsNullOrWhiteSpace( input.CE_CertAcronym ))
				output.AlternateName.Add( input.CE_CertAcronym );

			//?????????????????????????
			if (input.CE_CertStatus == 1)
			{
				output.CredentialStatusType = "Active";
			}
			else if( input.CE_CertStatus == 3 )
			{
				output.CredentialStatusType = "Suspended";
			} else
			{
				messages.Add( string.Format( "Error - Invalid CE_CertStatus: {0}. Valid values are 1-Active, and 3-Suspended.", input.CE_CertStatus ) );
			}

			//			
			//may need a conversion here from Federal License to License, and National Credential to Certification

			if ( input.CE_CertType == "C" )
				output.CredentialType = "Certification";
			else if ( input.CE_CertType == "L" )
				output.CredentialType = "License";
			else
			{
				messages.Add( string.Format( "Error - a valid certification type was not provided: {0}. Valid values are C for certificate, or L for License.", input.CE_CertType ));
			}

			//if ( input.CredentialType == "State License" )
			//{
			//	//require a Jurisdiction or reject?
			//	if (input.Jurisdiction == null || input.Jurisdiction.Count() == 0)
			//	{
			//		messages.Add( "Error (maybe) - a Jurisdiction must be provided for a State License." );
			//	}
			//	input.CredentialType = "License";
			//} else if( input.CredentialType == "Federal License" )
			//{
			//	//Not sure if a jurisdiction will be necessary?
			//	//should there be other useful info to indicate as a federal license?
			//	input.CredentialType = "License";
			//}
			////
			//output.CredentialType = input.CredentialType;

			//list of owning organization
			//use an organization reference for flexibity
			if ( IsCtidValid( input.CA_AgencyCTID, "Agency.CTID", ref messages, true ))
			{
				var orgRef = new RMI.OrganizationReference() { CTID = input.CA_AgencyCTID };
				output.OwnedBy.Add( orgRef );
				//if owner always offers:
				output.OfferedBy.Add( orgRef );
			}


			// cred_Rating
			// Rating related to this resource.
			// URI to a Rating
			// NOTE: API will accept just the CTID and properly format the URI based on the target environment
			foreach (var item in input.cred_Rating)
			{
				if ( IsCtidValid( item, "cred_Rating.CTID", ref messages, true ) )
					output.HasRating.Add( item.ToLower() );
			}

			//occupations - for the ***credential***
			//IF there are just Onet codes, then can provide them in the list property ONET_Codes
			output.ONET_Codes = input.cred_ONET;


			//accreditations
			//**SAMPLE** - where a CTID is not known, or available
			/*
			output.AccreditedBy.Add( new RMI.OrganizationReference()
			{
				Name= "ANAB (ANSI)",
				SubjectWebpage= "https://anab.ansi.org/",
				Type = "QACredentialOrganization",
				Description = "The ANSI National Accreditation Board (ANAB) is a non-governmental organization that provides accreditation services and training to public- and private-sector organizations, serving the global marketplace."
			} );
			*/
			output.AccreditedBy = HandleAccreditations( input.cred_AccreditedBy, ref messages );

			//condition profile(s)
			output.Requires = MapConditionProfiles( input.cred_ConditionProfiles, ref messages );

			//similar if there are recommended conditions
			//output.Recommends = MapConditionProfiles( input.Recommends, ref messages );

			//financial assistance
			output.FinancialAssistance = HandleFinancialAssistance( input.cred_FinancialAssistance, ref messages );

			//
			//if( input.Jurisdiction != null && input.Jurisdiction.Count > 0 )
			//{
			//	output.Jurisdiction = MapJurisdictions( input.Jurisdiction, ref messages );
			//}

			//Could include the contact information for the provider
			//if( input.Addresses != null && input.Addresses.Count > 0 )
			//{
			//	output.AvailableAt = FormatAvailableAtList( input.Addresses, ref messages );
			//}

			//renewal
			//sample for 2 years
			//output.RenewalFrequency = new RMI.DurationItem()
			//{
			//	Years = 2
			//};
			if (!string.IsNullOrWhiteSpace(input.CE_RenewalPeriod))
			{
				if ( input.CE_RenewalPeriod.ToLower().IndexOf( "lifetime") > -1 
					|| input.CE_RenewalPeriod.ToLower().IndexOf( "jump" ) > -1 
					|| input.CE_RenewalPeriod.ToLower().IndexOf( "dive" ) > -1
					|| input.CE_RenewalPeriod.ToLower().IndexOf( "varies" ) > -1
					|| input.CE_RenewalPeriod.ToLower().IndexOf( "not renewable" ) > -1
					)
				{
					//skip
				} 
				else if ( input.CE_RenewalPeriod.IndexOf( "P" ) == 0 )
				{
					//ISO 8601 format
					output.RenewalFrequency = new RMI.DurationItem()
					{
						Duration_ISO8601 = input.CE_RenewalPeriod.Trim()
					};
				}
				else
				{
					var rn = AssignDuration( input.CE_RenewalPeriod, ref messages, false, true );
					if ( rn != null )
						output.RenewalFrequency = rn.ExactDuration;
				}
			}
			//renewal conditions
			//list of condition profiles with renewal information
			//not sure of the input format
			//output.Renewal = MapConditionProfiles( input.Renewal );

			//others
			//keywords?
			//output.Keyword = input.Keywords;

			//costs??
		}

		public List<RMI.OrganizationReference> HandleAccreditations(List<string> input, ref List<string> messages )
		{
			var output = new List<RMI.OrganizationReference>();
			if ( input == null || input.Count == 0 )
				return null;

			//get list of QA orgs
			var qaOrgs = GetQAOrganizations();

			var cntr = 0;
			foreach (var item in input)
			{
				cntr++;
				if ( string.IsNullOrWhiteSpace( item ) )
					continue;

				//the input could be just an acronym or acronym | subjectWebpage ANSI-
				string[] parts = item.Split( '|' );
				var acronym = parts[ 0 ].Trim();
				var qaOrg = qaOrgs.FirstOrDefault( s => s.Acronym == acronym );
				if (qaOrg == null || string.IsNullOrWhiteSpace(qaOrg.Acronym))
				{
					messages.Add( string.Format( "Error: QA # {0}, Acronym: {1}. is not a known QA organization", cntr, acronym ) );
					continue;
				}
				var qaOut = new RMI.OrganizationReference()
				{
					Type = qaOrg.Type,
					Name = qaOrg.Name,
					Description = qaOrg.Description,
					SubjectWebpage = qaOrg.SubjectWebpage,
					CTID = qaOrg.CTID
				};

				if ( parts.Count() == 2 )
				{
					//should be url
					qaOut.SubjectWebpage = parts[ 1 ];

				}//else ignore for now

				output.Add( qaOut );
			}

			return output;
		}

		public List<RMI.FinancialAssistanceProfile> HandleFinancialAssistance(List<string> input, ref List<string> messages)
		{

			var output = new List<RMI.FinancialAssistanceProfile>();
			if ( input == null || input.Count == 0 )
				return null;
			//GetFinancialAssistanceTypes
			//get list of QA orgs
			var faTypes = GetFinancialAssistanceTypes();

			var cntr = 0;
			foreach ( var item in input )
			{
				cntr++;
				if ( string.IsNullOrWhiteSpace( item ) )
					continue;

				//the input could be just an acronym or acronym | subjectWebpage
				string[] parts = item.Split( '|' );
				var acronym = parts[ 0 ].Trim();
				var fa = faTypes.FirstOrDefault( s => s.Acronym == acronym );
				if ( fa == null || string.IsNullOrWhiteSpace( fa.Acronym ) )
				{
					messages.Add( string.Format( "Error: # {0}, Acronym: {1}. is not a known Financial Assistance type.", cntr, acronym ) );
					continue;
				}
				var faOut = new RMI.FinancialAssistanceProfile()
				{
					Name = fa.Name,
					Description = fa.Description,
					SubjectWebpage = fa.SubjectWebpage
				};

				if ( parts.Count() == 2 )
				{
					//should be url
					faOut.SubjectWebpage = parts[ 1 ];

				}//else ignore for now

				output.Add( faOut );
			}

			return output;
		}
	}
}
