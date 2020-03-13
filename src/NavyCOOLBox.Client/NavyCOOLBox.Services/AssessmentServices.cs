using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using NavyCOOLBox.Factories;
using NavyCOOLBox.Models.Publishing;
using NavyCOOLBox.Services;

using NavyCOOLBox.Models;
using InputEntity =NavyCOOLBox.Models.Assessment;


namespace NavyCOOLBox.Services
{
	public class AssessmentServices 
	{
		public bool Publish(int recordId, string publisherApiKey, ref List<string> messages, string community = "")
		{
			bool isValid = true;
			var request = new AssessmentRequest();
			//get for recordId and populate an assessment DTO
			//TBD
			//NOTE: an asmt cannot be published without a reference from a credential, so publish the credential first with, typically a reference to the asmt in a requires condition profile
			var input = new InputEntity(); //AssessmentManager.Get_Sample1asDTO();
			if ( recordId == 100 )
				input = AssessmentManager.Get_AircraftDispatcherWrittenExam();
			else if ( recordId == 101 )
				input = AssessmentManager.Get_AircraftDispatcherPracticalExam();
			else
			{
				//not handled
				messages.Add( string.Format("Error - no data available for the recordId", recordId) );
				return false;
			}

			request.Assessment = input;
			request.OwningOrganizationCTID = input.CTID;
			request.Community = community;
			//serialize and call 
			string coolInput = JsonConvert.SerializeObject( input, PublishingServices.GetJsonSettings() );
			AssistantRequestHelper arh = new AssistantRequestHelper()
			{
				OrganizationApiKey = publisherApiKey,
				CTID = request.OwningOrganizationCTID,
				InputPayload = coolInput,
				EndpointType = "organization"
			};


			isValid = new PublishingServices().PublishRequest( arh );
			return isValid;
		}
		/*
		/// <summary>
		/// Publish an agency record
		/// </summary>
		/// <param name="input">Assessment record</param>
		/// <param name="publisherApiKey">Provided the ApiKey of the publisher (COOL)</param>
		/// <param name="isValid">Returns true or false if publish fails.</param>
		/// <param name="messages">Returns any error message if publish fails. Note: warning messages can be returned even if isValid is true.</param>
		/// <param name="community">Blank if using the default community, or navy</param>
		/// <returns>Assessment formatted as JSON-LD (Payload) from the registry</returns>
		/// <returns></returns>
		public string Publish(ThisEntity input, string publisherApiKey, ref bool isValid, ref List<string> messages, string community = "")
		{
			var request = new ThisRequest();
			request.DefaultLanguage = "en-US";

			//map from Assessment record to the API Assessment request class
			MapToAssistant( input, request, ref messages );

			request.Community = community;
			//serialize the input (for logging)
			string jsoninput = JsonConvert.SerializeObject( request, GetJsonSettings() );
			string filePrefix = string.Format( "Assessment_{0}", input.CTID );
			Utilities.LoggingHelper.WriteLogFile( 5, filePrefix + "_raInput.json", jsoninput, "", false );

			#region  Authorization settings

			//the CTID for the owning org must be present or the publish will fail
			request.PublishForOrganizationIdentifier = input.OwnedByOrganizationCTID;

			#endregion

			//format the payload
			string postBody = JsonConvert.SerializeObject( request, MappingHelpers.GetJsonSettings() );

			AssistantRequestHelper req = new AssistantRequestHelper()
			{
				EndpointType = "assessment",
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

		public static void MapToAssistant(ThisEntity input, ThisRequest request, ref List<string> messages)
		{

			ThisRequestEntity output = request.Assessment;
			//language for maps
			request.DefaultLanguage = "en-US";

			output.Name = input.Name;
			output.Description = input.Description;
			output.Ctid = input.CTID.ToLower();
			output.SubjectWebpage = input.SubjectWebpage;
			output.OwnedBy = MapToOrgReferences( input.OwnedByOrganizationCTID, false );
				
			
			//at least one of:
			output.AvailabilityListing = MapStringToList( input.AvailabilityListing );
			output.AvailableOnlineAt = MapStringToList( input.AvailableOnlineAt );
			if ( input.Addresses != null && input.Addresses.Count > 0 )
			{
				output.AvailableAt = FormatAvailableAt( input.Addresses );
			}


			//other TBD
			output.CodedNotation = input.CodedNotation;
			output.ExternalResearch = MapStringToList( input.ExternalResearch );

		}
		*/
	}
}
