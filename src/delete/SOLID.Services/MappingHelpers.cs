using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Utilities;

using Solid.Models;
using MIPlace = Solid.Models.Address;

using MOPlace = RA.Models.Input.Place;
using RMI = RA.Models.Input;

namespace SOLID.Services
{
	public class MappingHelpers
	{

		#region Mapping addresses, juridictions, etc


		/// <summary>
		/// Format AvailableAt
		/// 17-10-20 - essentially an address now
		/// 17-11-02 - essentially a Place now
		/// </summary>
		/// <param name="input"></param>
		/// <param name="messages"></param>
		/// <returns></returns>
		public static List<MOPlace> FormatAvailableAtList(List<MIPlace> input, ref List<string> messages)
		{
			//Available At should require an address, not just contact points
			return FormatPlacesList( input, ref messages, true );
		}

		public static List<MOPlace> FormatPlacesList(List<MIPlace> input, ref List<string> messages, bool addressExpected = false)
		{
			List<MOPlace> list = new List<MOPlace>();
			if( input == null || input.Count == 0 )
				return list;
			MOPlace output = new MOPlace();

			foreach( var item in input )
			{
				output = new MOPlace();
				output = FormatPlace( item, addressExpected, ref messages );

				list.Add( output );
			}
			return list;
		}//

		public static MOPlace FormatPlace(MIPlace input, bool isAddressExpected, ref List<string> messages)
		{
			MOPlace output = new MOPlace();
			if( input == null || input.HasAddress() == false )
				return output;

			//need to handle null or incomplete
			RMI.ContactPoint cpo = new RMI.ContactPoint();

			output = (new MOPlace
			{
				Name = input.Name,
				Address1 = input.Address1,
				Address2 = input.Address2,
				City = input.City,
				Country = input.Country,
				AddressRegion = input.AddressRegion,
				PostalCode = input.PostalCode
			});

			//always include lat/lng
			output.Latitude = input.Latitude;
			output.Longitude = input.Longitude;

			//output.Description = input.Description;

			bool hasContactPoints = false;
			if( input.ContactPoint != null && input.ContactPoint.Count > 0 )
			{
				foreach( var cpi in input.ContactPoint )
				{
					cpo = new RMI.ContactPoint()
					{
						Name = cpi.Name,
						ContactType = cpi.ContactType
					};
					output.ContactPoint.Add( cpo );
					hasContactPoints = true;
				}
			}
			else
				output.ContactPoint = null;

			bool hasAddress = false;
			if( (string.IsNullOrWhiteSpace( output.Address1 )
					&& string.IsNullOrWhiteSpace( output.PostOfficeBoxNumber ))
					|| string.IsNullOrWhiteSpace( input.City )
					|| string.IsNullOrWhiteSpace( input.AddressRegion )
					|| string.IsNullOrWhiteSpace( input.PostalCode ) )
			{
				if( isAddressExpected )
				{
					messages.Add( "Error - A valid address expected. Please provide a proper address, along with any contact points." );
					return null;
				}
			}
			else
				hasAddress = true;

			//check for at an address or contact point
			if( !hasContactPoints && !hasAddress
			)
			{
				messages.Add( "Error - incomplete place/address. Please provide a proper address and/or one or more proper contact points." );
				output = null;
			}

			return output;
		}
		public static List<RMI.Jurisdiction> MapJurisdictions(List<JurisdictionProfile> list, ref List<string> messages)
		{
			List<RMI.Jurisdiction> output = new List<RMI.Jurisdiction>();
			if( list == null || list.Count == 0 )
				return null;
			RMI.Jurisdiction jp = new RMI.Jurisdiction();
			foreach( var j in list )
			{
				jp = new RMI.Jurisdiction();
				jp = MapToJurisdiction( j, ref messages );
				output.Add( jp );
			}
			return output;
		}


		public static RMI.Jurisdiction MapToJurisdiction(JurisdictionProfile profile, ref List<string> messages)
		{
			var entity = new RMI.Jurisdiction();

			entity.Description = profile.Description;
			if( profile.MainJurisdiction != null && profile.MainJurisdiction.HasAddress() )
			{
				//entity.MainJurisdiction = profile.MainJurisdiction.Name;
				entity.GlobalJurisdiction = false;
				entity.MainJurisdiction = FormatPlace( profile.MainJurisdiction, true, ref messages );
			}
			else
			{
				entity.GlobalJurisdiction = profile.GlobalJurisdiction;
			}

			//handle exceptions
			if( profile.JurisdictionException != null && profile.JurisdictionException.Count > 0 )
			{
				foreach( var item in profile.JurisdictionException )
				{
					if ( item.HasAddress() )
						entity.JurisdictionException.Add( FormatPlace( item, true, ref messages ) );
				}
			}


			return entity;
		}


		#endregion

		public static JsonSerializerSettings GetJsonSettings()
		{
			var settings = new JsonSerializerSettings()
			{
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				ContractResolver = new AlphaNumericContractResolver(),
				Formatting = Formatting.Indented,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
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
		/// <summary>
		/// NOTE: previously inherited from AlphaNumericContractResolver. the latter would sort by property name, which we don't want - must be @context, @id, and @graph
		/// </summary>
		public class EmptyNullResolver : DefaultContractResolver
		{
			protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
			{
				var property = base.CreateProperty( member, memberSerialization );
				var isDefaultValueIgnored = ((property.DefaultValueHandling ?? DefaultValueHandling.Ignore) & DefaultValueHandling.Ignore) != 0;

				if( isDefaultValueIgnored )
					if( !typeof( string ).IsAssignableFrom( property.PropertyType ) && typeof( IEnumerable ).IsAssignableFrom( property.PropertyType ) )
					{
						Predicate<object> newShouldSerialize = obj =>
						{
							var collection = property.ValueProvider.GetValue( obj ) as ICollection;
							return collection == null || collection.Count != 0;
						};
						Predicate<object> oldShouldSerialize = property.ShouldSerialize;
						property.ShouldSerialize = oldShouldSerialize != null ? o => oldShouldSerialize( oldShouldSerialize ) && newShouldSerialize( oldShouldSerialize ) : newShouldSerialize;
					}
					else if( typeof( string ).IsAssignableFrom( property.PropertyType ) )
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
		public static bool IsValidGuid(Guid field)
		{
			if( (field == null || field == Guid.Empty) )
				return false;
			else
				return true;
		}
		public static bool IsValidGuid(string field)
		{
			Guid guidOutput;
			if( (field == null || field.ToString() == Guid.Empty.ToString()) )
				return false;
			else if( !Guid.TryParse( field, out guidOutput ) )
				return false;
			else
				return true;
		}
	}
}
