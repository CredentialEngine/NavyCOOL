using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RAResponse = NavyCOOLBox.Services.RegistryAssistantResponse;

using Utilities;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NavyCOOLBox.Services
{
	public class PublishingServices
	{
		string thisClassName = "NavyCOOLBox.Services.PublishingServices";
		public string environment = UtilityManager.GetAppKeyValue( "envType" );

		public bool PublishRequest(AssistantRequestHelper request)
		{
			//"https://credentialengine.org/raSandbox/"
			string serviceUri = UtilityManager.GetAppKeyValue( "navyAssistantApi" );
			if ( DateTime.Now.Day == 6 && UtilityManager.GetAppKeyValue( "envType" ) == "development" )
			{
				//serviceUri = "https://localhost:44304/";
			}
			request.EndpointUrl = serviceUri + string.Format( "{0}/{1}", request.EndpointType, request.RequestType );

			return PostRequest( request );
		}
		public bool PostRequest(AssistantRequestHelper request)
		{
			RAResponse response = new RAResponse();
			string apiPublisherIdentifier = UtilityManager.GetAppKeyValue( "apiPublisherIdentifier" );

			LoggingHelper.DoTrace( 6, string.Format( thisClassName + ".PostRequest, RequestType: {0}, CTID: {1}, payloadLen: {2}, starts: '{3}' ....", request.RequestType, request.CTID, ( request.InputPayload ?? "" ).Length, request.InputPayload.Substring( 0, request.InputPayload.Length > 200 ? 200 : request.InputPayload.Length ) ) );
			string responseContents = "";
			try
			{
				using ( var client = new HttpClient() )
				{
					client.DefaultRequestHeaders.
						Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );
					//

					if ( !string.IsNullOrWhiteSpace( request.OrganizationApiKey ) )
					{
						client.DefaultRequestHeaders.Add( "Authorization", "ApiToken " + request.OrganizationApiKey );
					}
					else
					{
						if ( environment == "production" || environment == "staging" )
						{
							request.Messages.Add( "Error - an apiKey was not found for the owning organization. The owning organization must be approved in the Credential Engine Accounts site before being able to publish data." );
							return false;
						}
					}
					if ( UtilityManager.GetAppKeyValue( "envType" ) == "development" )
					{
						client.Timeout = new TimeSpan( 0, 30, 0 );
					}
					else if ( request.InputPayload.Length > 2000000 )
					{
						client.Timeout = new TimeSpan( 0, 30, 0 );
					}

					LoggingHelper.DoTrace( 6, "Publisher.PostRequest: doing PostAsync to: " + request.EndpointUrl );
					var task = client.PostAsync( request.EndpointUrl,
						new StringContent( request.InputPayload, Encoding.UTF8, "application/json" ) );

					task.Wait();
					var result = task.Result;
					LoggingHelper.DoTrace( 6, "Publisher.PostRequest: reading task.Result.Content" );
					responseContents = task.Result.Content.ReadAsStringAsync().Result;

					if ( result.IsSuccessStatusCode == false )
					{
						LoggingHelper.DoTrace( 6, "Publisher.PostRequest: result.IsSuccessStatusCode == false" );
						response = JsonConvert.DeserializeObject<RAResponse>( responseContents );
						//logging???
						//string queryString = GetRequestContext();
						string status = string.Join( ",", response.Messages.ToArray() );
						request.FormattedPayload = response.Payload ?? "";
						request.Messages.AddRange( response.Messages );

						LoggingHelper.DoTrace( 4, thisClassName + string.Format( ".PostRequest() {0} {1} failed: {2}", request.EndpointType, request.RequestType, status ) );
						LoggingHelper.LogError( thisClassName + string.Format( ".PostRequest()  {0} {1}. Failed\n\rMessages: {2}" + "\r\nResponse: " + response + "\n\r" + responseContents + ". payload: " + response.Payload, request.EndpointType, request.RequestType, status ) );
						return false;
					}
					else
					{
						response = JsonConvert.DeserializeObject<RAResponse>( responseContents );
						//
						if ( response.Successful )
						{
							LoggingHelper.DoTrace( 7, thisClassName + " PostRequest. envelopeId: " + response.RegistryEnvelopeIdentifier );
							LoggingHelper.WriteLogFile( 5, request.Identifier + "_payload_Successful.json", response.Payload, "", false );

							request.FormattedPayload = response.Payload;
							request.EnvelopeIdentifier = response.RegistryEnvelopeIdentifier;
							//may have some warnings to display
							request.Messages.AddRange( response.Messages );
						}
						else
						{
							string status = string.Join( ",", response.Messages.ToArray() );
							LoggingHelper.DoTrace( 5, thisClassName + " PostRequest FAILED. result: " + status );
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
				LoggingHelper.LogError( exc, string.Format( "PostRequest. RequestType:{0}, Identifier: {1}. /n/r responseContents: {2}", request.RequestType, request.Identifier, ( responseContents ?? "empty" ) ) );
				string message = LoggingHelper.FormatExceptions( exc );
				request.Messages.Add( message );
				return false;

			}
			//return false;
			//return valid;
		}
		#region Newtonsoft.Json.Serialization helpers
		//JsonSerializer
		//for serializing
		public static JsonSerializerSettings GetJsonSettings()
		{
			var settings = new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				ContractResolver = new EmptyNullResolver(),
				Formatting = Formatting.Indented,
				DateParseHandling = DateParseHandling.None,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			};

			return settings;
		}
		//for serializing
		public static JsonSerializerSettings GetJsonFlatSettings()
		{
			var settings = new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				ContractResolver = new EmptyNullResolver(),
				Formatting = Formatting.None,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			};

			return settings;
		}
		//for deserializing
		public static Newtonsoft.Json.JsonSerializer GetJsonSerializerSettings()
		{
			var settings = new Newtonsoft.Json.JsonSerializer()
			{
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				ContractResolver = new EmptyNullResolver(),
				Formatting = Formatting.Indented,
				DateParseHandling = DateParseHandling.DateTimeOffset,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore

			};
			//
			return settings;
		}

		/// <summary>
		/// NOTE: previously inherited from AlphaNumericContractResolver. the latter would sort by property name, which we don't want - must be @context, @id, and @graph
		/// </summary>
		public class EmptyNullResolver : DefaultContractResolver
		{
			protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
			{
				var property = base.CreateProperty( member, memberSerialization );
				var isDefaultValueIgnored = ( ( property.DefaultValueHandling ?? DefaultValueHandling.Ignore ) & DefaultValueHandling.Ignore ) != 0;

				if ( isDefaultValueIgnored )
					if ( !typeof( string ).IsAssignableFrom( property.PropertyType ) && typeof( IEnumerable ).IsAssignableFrom( property.PropertyType ) )
					{
						Predicate<object> newShouldSerialize = obj =>
						{
							var collection = property.ValueProvider.GetValue( obj ) as ICollection;
							return collection == null || collection.Count != 0;
						};
						Predicate<object> oldShouldSerialize = property.ShouldSerialize;
						property.ShouldSerialize = oldShouldSerialize != null ? o => oldShouldSerialize( oldShouldSerialize ) && newShouldSerialize( oldShouldSerialize ) : newShouldSerialize;
					}
					else if ( typeof( string ).IsAssignableFrom( property.PropertyType ) )
					{
						Predicate<object> newShouldSerialize = obj =>
						{
							var value = property.ValueProvider.GetValue( obj ) as string;
							return !string.IsNullOrEmpty( value );
						};

						Predicate<object> oldShouldSerialize = property.ShouldSerialize;
						property.ShouldSerialize = oldShouldSerialize != null ? o => oldShouldSerialize( oldShouldSerialize ) && newShouldSerialize( oldShouldSerialize ) : newShouldSerialize;
					}
				return property;
			}
		}

		public class DecimalFormatConverter : Newtonsoft.Json.JsonConverter
		{
			public override bool CanConvert(Type objectType)
			{
				return objectType == typeof( decimal );
			}

			public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
			{
				writer.WriteRawValue( $"{value:0.00}" );
			}

			public override bool CanRead => false;

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
			{
				throw new NotImplementedException();
			}
		}
		#endregion
	}
	public class AssistantRequestHelper
	{
		public AssistantRequestHelper()
		{
			//response = new RAResponse();
			Messages = new List<string>();
			OrganizationApiKey = "";
		}
		//input
		public string RequestType { get; set; }

		//when we are ready to really use the apiKey, change to an alias for AuthorizationToken
		public string OrganizationApiKey { get; set; }
		public string CTID { get; set; } //used for deletes
		public string InputPayload { get; set; }
		public string EndpointType { get; set; }
		public string EndpointUrl { get; set; }

		public string Identifier { get; set; }
		public string EnvelopeIdentifier { get; set; }

		public string FormattedPayload { get; set; }

		//public string Status { get; set; }
		public List<string> Messages { get; set; }
	}

	public class RegistryAssistantResponse
	{
		public RegistryAssistantResponse()
		{
			Messages = new List<string>();
			Payload = "";
		}

		/// True if action was successfull, otherwise false
		public bool Successful { get; set; }
		/// <summary>
		/// List of error or warning messages
		/// </summary>
		public List<string> Messages { get; set; }

		public string CTID { get; set; }
		/// <summary>
		/// URL for the registry envelope that contains the document just add/updated
		/// </summary>
		public string EnvelopeUrl { get; set; }
		/// <summary>
		/// URL for the graph endpoint for the document just add/updated
		/// </summary>
		public string GraphUrl { get; set; }
		/// <summary>
		/// Credential Finder Detail Page URL for the document just published (within 30 minutes of publishing)
		/// </summary>
		public string CredentialFinderUrl { get; set; }
		/// <summary>
		/// Identifier for the registry envelope that contains the document just add/updated
		/// </summary>
		public string RegistryEnvelopeIdentifier { get; set; }

		/// <summary>
		/// Payload of request to registry, containing properties formatted as CTDL - JSON-LD
		/// </summary>
		public string Payload { get; set; }
	}
}
