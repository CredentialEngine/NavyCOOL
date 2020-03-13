using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Utilities;

namespace COOLTool.Services
{
	public class HelperServices
	{

		public static bool IsAuthTokenValid( ref string apiToken, ref string message, bool isTokenRequired= true)
		{
			bool isValid = true;
			//need to handle both ways. So if a token, and ctid are provided, then use them!
			try
			{
				HttpContext httpContext = HttpContext.Current;
				string authHeader = httpContext.Request.Headers[ "Authorization" ];
				//registry API uses ApiToken rather than Basic
				if ( !string.IsNullOrWhiteSpace( authHeader ) )
				{
					LoggingHelper.DoTrace( 6, "$$$$$$$$ Found an authorization header." + authHeader );
					if ( authHeader.ToLower().StartsWith( "apitoken " ) && authHeader.Length > 36 )
					{
						//Extract credentials
						authHeader = authHeader.ToLower();
						apiToken = authHeader.Substring( "apitoken ".Length ).Trim();
					}
				}
			}
			catch ( Exception ex )
			{
				if ( isTokenRequired )
				{
					LoggingHelper.LogError( ex, "Exception encountered attempting to get API key from request header. " );
					isValid = false;
				}
			}

			if ( isTokenRequired && string.IsNullOrWhiteSpace( apiToken ) )
			{
				message = "Error a valid API key must be provided in the header";
				isValid = false;
			}

			return isValid;
		}

		public static string LogInputFile(object request, string ctid, string entityType, string endpoint, int appLevel = 6)
		{
			string jsoninput = JsonConvert.SerializeObject( request, GetJsonSettings() );
			LoggingHelper.WriteLogFile( appLevel, string.Format( "{0}_{1}_{2}_raInput.json", entityType, endpoint, ctid ), jsoninput, "", false );
			return jsoninput;
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
}
