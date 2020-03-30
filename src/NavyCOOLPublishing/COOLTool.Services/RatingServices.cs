using System.Collections.Generic;

using COOLTool.Services.Models.Input;

using Newtonsoft.Json;

using InputEntity = COOLTool.Services.Models.Input.Rating;
using InputRequest = COOLTool.Services.Models.Input.RatingRequest;
using OutputRequest = RA.Models.Input.RatingRequest;
using OutputRequestEntity = RA.Models.Input.Rating;
using RServices = COOLTool.Services.RegistryServices;

namespace COOLTool.Services
{
	public class RatingServices : MappingHelpers
	{

		string className = "RatingServices";

		/// <summary>
		/// Make a request to the assistant api
		/// </summary>
		/// <param name="input"></param>
		/// <param name="requestType">format or "publish</param>
		/// <param name="submitter"></param>
		/// <param name="isValid"></param>
		/// <param name="messages"></param>
		/// <param name="crEnvelopeId"></param>
		/// <returns></returns>
		public string Publish(InputRequest input, RequestHelper helper)
		{
			List<string> messages = new List<string>();
			var request = new OutputRequest();
			request.DefaultLanguage = "en-US";

			//
			string filePrefix = string.Format( "Rating_{0}", input.Rating.CTID );
			//save this for use in postman
			string coolInput = JsonConvert.SerializeObject( input, HelperServices.GetJsonSettings() );
			Utilities.LoggingHelper.WriteLogFile( 5, filePrefix + "_coolInput.json", coolInput, "", false );

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
				EndpointType = "rating",
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
		public void MapToAssistant(InputEntity input, OutputRequestEntity output, ref List<string> messages)
		{
			//A ctid is required, and would have to be maintained by the organization for use in making updates to previously published credentials

			if ( IsCtidValid( input.CTID, "Rating.CTID", ref messages, true ) )
			{
				output.CTID = input.CTID.ToLower();
			}

			output.CodedNotation = input.CodedNotation;
			output.Comment = input.Comment;
			output.DatePublished = input.DatePublished ?? "";
			output.Description = input.Description;
			output.HasCredential = input.HasCredential;
			output.HasDoDOccupationType = input.HasDoDOccupationType;
			output.HasOccupationType = input.HasOccupationType;
			output.MainEntityOfPage = input.MainEntityOfPage;
			output.Name = input.Name;
			output.UploadDate = input.UploadDate ?? "";
			output.Version = input.Version;
		}
	}
}
