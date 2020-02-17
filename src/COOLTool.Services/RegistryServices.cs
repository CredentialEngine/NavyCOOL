using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Runtime.Serialization;
using RA.Models.Input;
using RAResponse = RA.Models.RegistryAssistantResponse;
using Utilities;

namespace COOLTool.Services
{
	 
	public class RegistryServices
	{
		public static string thisClassName = "COOLTool.Services";
		#region Json settings
		public static JsonSerializerSettings GetJsonSettings()
		{
			var settings = new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				ContractResolver = new AlphaNumericContractResolver(),
				Formatting = Formatting.Indented
			};

			return settings;
		}
		//Force properties to be serialized in alphanumeric order
		public class AlphaNumericContractResolver : DefaultContractResolver
		{
			protected override System.Collections.Generic.IList<JsonProperty> CreateProperties(System.Type type, MemberSerialization memberSerialization)
			{
				return base.CreateProperties( type, memberSerialization ).OrderBy( m => m.PropertyName ).ToList();
			}
		}
		#endregion

		public bool FormatRequest(string postBody, string requestType, ref RAResponse response)
		{

			AssistantRequestHelper req = new AssistantRequestHelper()
			{
				EndpointType = requestType,
				RequestType = "Format",
				Identifier = requestType,
				InputPayload = postBody
			};
			string serviceUri = UtilityManager.GetAppKeyValue( "registryAssistantApi" );
			//NOTE: the V2 will be added later
			req.EndpointUrl = serviceUri + string.Format( "{0}/format", requestType );

			if ( PostRequest( req ) )
			{
				response.Payload = req.FormattedPayload;
				response.RegistryEnvelopeIdentifier = req.EnvelopeIdentifier;
				return true;
			}
			else
			{
				response.Payload = req.FormattedPayload;
				response.Messages.AddRange( req.Messages );

				return false;
			}
		}

		/// <summary>
		/// Method for a Format Request 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public bool FormatRequest(AssistantRequestHelper request)
		{
			string serviceUri = UtilityManager.GetAppKeyValue( "registryAssistantApi" );
			request.EndpointUrl = serviceUri + string.Format( "{0}/format", request.EndpointType );


			return PostRequest( request );
		}

		public bool PublishRequest(AssistantRequestHelper request)
		{
			//"https://credentialengine.org/raSandbox/"
			string serviceUri = UtilityManager.GetAppKeyValue( "registryAssistantApi" );
			//serviceUri = "http://localhost:5707/";
			if ( DateTime.Now.Month == 2 && DateTime.Now.Day == 10 && DateTime.Now.Hour > 16 )
			{
				//serviceUri = "https://localhost:44304/";
			}
			request.EndpointUrl = serviceUri + string.Format( "{0}/{1}", request.EndpointType, request.RequestType );

			return PostRequest( request );
		}
		public bool PostRequest(AssistantRequestHelper request)
		{
			RAResponse response = new RAResponse();

			LoggingHelper.DoTrace( 6, string.Format( thisClassName + ".PostRequest, RequestType: {0}, CTID: {1}", request.RequestType, request.CTID ) );
			try
			{
				using ( var client = new HttpClient() )
				{
					client.DefaultRequestHeaders.
						Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
					if ( string.IsNullOrWhiteSpace( request.AuthorizationToken ) )
						request.AuthorizationToken = request.PublisherApiKey;

					if ( !string.IsNullOrWhiteSpace( request.AuthorizationToken ) )
					{
						client.DefaultRequestHeaders.Add( "Authorization", "ApiToken " + request.AuthorizationToken );
					}

					var task = client.PostAsync( request.EndpointUrl,
						new StringContent( request.InputPayload, Encoding.UTF8, "application/json" ) );
					task.Wait();
					var result = task.Result;
					var contents = task.Result.Content.ReadAsStringAsync().Result;

					if ( result.IsSuccessStatusCode == false )
					{
						response = JsonConvert.DeserializeObject<RAResponse>( contents );
						//logging???
						//string queryString = GetRequestContext();
						string status = string.Join( ",", response.Messages.ToArray() );
						request.FormattedPayload = response.Payload ?? "";
						request.Messages.AddRange( response.Messages );

						LoggingHelper.DoTrace( 4, thisClassName + string.Format( ".PostRequest() {0} {1} failed: {2}", request.EndpointType, request.RequestType, status ) );
						LoggingHelper.LogError( thisClassName + string.Format( ".PostRequest()  {0} {1}. Failed\n\rMessages: {2}" + "\r\nResponse: " + response + "\n\r" + contents + ". payload: " + response.Payload, request.EndpointType, request.RequestType, status ) );

					}
					else
					{
						response = JsonConvert.DeserializeObject<RAResponse>( contents );
						//
						if ( response.Successful )
						{
							LoggingHelper.DoTrace( 7, thisClassName + " PostRequest. envelopeId: " + response.RegistryEnvelopeIdentifier );
							LoggingHelper.WriteLogFile( 5, request.Identifier + "_payload_Successful.json", response.Payload, "", false );

							request.FormattedPayload = response.Payload;
							request.EnvelopeIdentifier = response.RegistryEnvelopeIdentifier;
							LoggingHelper.DoTrace( 5, thisClassName + " Published to: " + response.GraphUrl );
							//may have some warnings to display
							request.Messages.AddRange( response.Messages );
						}
						else
						{
							LoggingHelper.DoTrace( 5, thisClassName + " PostRequest FAILED. result: " + response );
							request.Messages.AddRange( response.Messages );
							request.FormattedPayload = response.Payload;
							//LoggingHelper.WriteLogFile( 5, request.Identifier + "_payload_FAILED.json", response.Payload, "", false );
							return false;
						}

					}
					return result.IsSuccessStatusCode;
				}
			}
			catch ( AggregateException ae )
			{
				LoggingHelper.LogError( ae, string.Format( "PostRequest.AggregateException. RequestType:{0}, Identifier: {1}", request.RequestType, request.Identifier ) );
				string message = LoggingHelper.FormatExceptions( ae );
				request.Messages.Add( message );
				return false;
			}
			catch ( Exception exc )
			{
				LoggingHelper.LogError( exc, string.Format( "PostRequest. RequestType:{0}, Identifier: {1}", request.RequestType, request.Identifier ) );
				string message = LoggingHelper.FormatExceptions( exc );
				request.Messages.Add( message );
				return false;

			}
			//return valid;
		}
		public bool DeleteRequest(string CTID, string dataOwnerCtid, string authorizationToken, string crEnvelopeId, string requestType, ref string message)
		{
			RAResponse raResponse = new RAResponse();
			//"https://credentialengine.org/raSandbox/"
			string serviceUri = UtilityManager.GetAppKeyValue( "registryAssistantApi" );
			//might use one delete endpoint, as adding code to handle this.
			string endpointUrl = serviceUri + string.Format( "{0}/delete", requestType );

			DeleteRequest dr = new DeleteRequest()
			{
				CTID = CTID,
				PublishForOrganizationIdentifier = dataOwnerCtid,
				RegistryEnvelopeId = crEnvelopeId
			};

			//format the payload
			string postBody = JsonConvert.SerializeObject( dr, MappingHelpers.GetJsonSettings() );
			try
			{
				using ( var client = new HttpClient() )
				{
					client.DefaultRequestHeaders.
						Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );

					if ( !string.IsNullOrWhiteSpace( authorizationToken ) )
					{
						client.DefaultRequestHeaders.Add( "Authorization", "ApiToken " + authorizationToken );
					}

					HttpRequestMessage hrm = new HttpRequestMessage
					{
						Content = new StringContent( postBody, Encoding.UTF8, "application/json" ),
						Method = HttpMethod.Delete,
						RequestUri = new Uri( endpointUrl )
					};
					var task = client.SendAsync( hrm );
					task.Wait();
					var result = task.Result;
					string response = JsonConvert.SerializeObject( result );
					var contents = task.Result.Content.ReadAsStringAsync().Result;
					//
					if ( result.IsSuccessStatusCode == false )
					{
						//logging???
						//response = contents.Result;
						LoggingHelper.LogError( "RegistryServices.DeleteRequest Failed\n\r" + response + "\n\rError: " + JsonConvert.SerializeObject( contents ) );

						RegistryResponseContent contentsJson = JsonConvert.DeserializeObject<RegistryResponseContent>( contents );
						message = string.Join( "<br/>", contentsJson.Errors.ToArray() );
					}
					else
					{
						raResponse = JsonConvert.DeserializeObject<RAResponse>( contents );
						//
						if ( raResponse.Successful )
						{
							LoggingHelper.DoTrace( 5, string.Format( "DeleteRequest sucessful for requestType:{0}.  CTID: {1}, dataOwnerCtid: {2}, crEnvelopeId: {3} ", requestType, CTID, dataOwnerCtid, crEnvelopeId ) );
						}
						else
						{
							LoggingHelper.DoTrace( 5, thisClassName + " DeleteRequest FAILED. result: " + response );
							//message = string.Join("", raResponse.Messages );
							message = string.Join( ",", raResponse.Messages.ToArray() );
							return false;
						}

					}
					return result.IsSuccessStatusCode;
				}
			}
			catch ( Exception exc )
			{
				LoggingHelper.LogError( exc, string.Format( "DeleteRequest. RequestType:{0}, CTID: {1}", requestType, CTID ) );
				message = LoggingHelper.FormatExceptions( exc );
				return false;

			}
		}


		public void SetAuthorizationTokens()
		{

		}
	}

	public class AssistantRequestHelper
	{
		public AssistantRequestHelper()
		{
			//response = new RAResponse();
			Messages = new List<string>();
			PublisherApiKey = "";
		}
		//input
		public string RequestType { get; set; }
		public string AuthorizationToken { get; set; }

		//when we are ready to really use the apiKey, change to an alias for AuthorizationToken
		public string PublisherApiKey { get; set; }
		public string CTID { get; set; } //used for deletes
		public string Submitter { get; set; }
		public string InputPayload { get; set; }
		public string EndpointType { get; set; }
		public string EndpointUrl { get; set; }

		public string Identifier { get; set; }
		public string EnvelopeIdentifier { get; set; }

		public string FormattedPayload { get; set; }

		//public string Status { get; set; }
		public List<string> Messages { get; set; }
		//public RAResponse response { get; set; }
	}
	public class RegistryResponseContent
	{
		[JsonProperty( PropertyName = "errors" )]
		public List<string> Errors { get; set; }

		[JsonProperty( PropertyName = "json_schema" )]
		public List<string> JsonSchema { get; set; }

	}
}
