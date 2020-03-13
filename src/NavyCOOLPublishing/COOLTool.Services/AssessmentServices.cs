using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using COOLTool.Services.Models.Input;
using InputRequest = COOLTool.Services.Models.Input.AssessmentRequest;
using ThisEntity = COOLTool.Services.Models.Input.Assessment;

using OutputRequest = RA.Models.Input.AssessmentRequest;
using OutputRequestEntity = RA.Models.Input.Assessment;
using RA.Models.Input;

using RServices = COOLTool.Services.RegistryServices;

namespace COOLTool.Services
{
	public class AssessmentServices : MappingHelpers
	{

		/// <summary>
		/// Publish an Assessment record
		/// </summary>
		/// <param name="input">Assessment record</param>
		/// <param name="publisherApiKey">Provided the ApiKey of the publisher (COOL)</param>
		/// <param name="isValid">Returns true or false if publish fails.</param>
		/// <param name="messages">Returns any error message if publish fails. Note: warning messages can be returned even if isValid is true.</param>
		/// <param name="community">Blank if using the default community, or navy</param>
		/// <returns>Assessment formatted as JSON-LD (Payload) from the registry</returns>
		/// <returns></returns>
		public string Publish(InputRequest input, RequestHelper helper)
		{
			List<string> messages = new List<string>();
			var request = new OutputRequest();
			request.DefaultLanguage = "en-US";

			string filePrefix = string.Format( "Assessment_{0}", input.Assessment.Ctid );
			//save this for use in postman
			string coolInput = JsonConvert.SerializeObject( input, HelperServices.GetJsonSettings() );
			Utilities.LoggingHelper.WriteLogFile( 5, filePrefix + "_coolInput.json", coolInput, "", false );

			//map from Assessment record to the API Assessment request class
			MapToAssistant( input.Assessment, request.Assessment, ref messages );

			request.Community = input.Community;
			//serialize the input (for logging)
			string jsoninput = JsonConvert.SerializeObject( request, GetJsonSettings() );
			
			Utilities.LoggingHelper.WriteLogFile( 5, filePrefix + "_coolRaOutput.json", jsoninput, "", false );
			if ( messages.Count > 0 )
			{
				helper.SetMessages( messages );
				helper.IsValidRequest = false;
				return "";
			}
			//the CTID for the owning org must be present or the publish will fail
			request.PublishForOrganizationIdentifier = input.OwningOrganizationCTID;


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

			req.PublisherApiKey = helper.ApiKey;

			helper.IsValidRequest = new RServices().PublishRequest( req );
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

		public static void MapToAssistant(ThisEntity input, OutputRequestEntity output, ref List<string> messages)
		{

			output.Name = input.Name;
			output.Description = input.Description;
			output.Ctid = input.Ctid.ToLower();
			output.SubjectWebpage = input.SubjectWebpage;
			//output.OwnedBy = MapToOrgReferences( input.OwnedBy, false );
				
			
			//at least one of:
			//TODO
			//output.AvailabilityListing = MapStringToList( input.AvailabilityListing );
			//output.AvailableOnlineAt = MapStringToList( input.AvailableOnlineAt );
			//if ( input.Addresses != null && input.Addresses.Count > 0 )
			//{
			//	output.AvailableAt = FormatAvailableAt( input.Addresses );
			//}


			//other TBD
			output.CodedNotation = input.CodedNotation;
			//output.ExternalResearch = MapStringToList( input.ExternalResearch );

		}
	}
}
