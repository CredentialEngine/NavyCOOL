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

using SMI = COOLTool.Services.Models.Input;
using MIPlace = COOLTool.Services.Models.Input.Address;
using MOPlace = RA.Models.Input.Place;
using RMI = RA.Models.Input;
using System.Globalization;
using System.Net;

namespace COOLTool.Services
{
	public class MappingHelpers
	{
		public string thisClassName = "COOLTool.ServicesMappingHelpers";
		//set to true for scenarios like a deprecated credential, where need allow publishing even if urls are invalid!
		public bool WarnOnInvalidUrls = false;
		public List<string> warningMessages = new List<string>();
		public string CurrentEntityName = "";
		public string CurrentEntityType = "";
		public string CurrentCtid = "";

		#region Code validation
		/// <summary>
		/// Validate CTID
		/// TODO - should we generate if not found
		/// </summary>
		/// <param name="ctid"></param>
		/// <param name="messages"></param>
		/// <returns></returns>
		public bool IsCtidValid(string ctid, string property, ref List<string> messages, bool isRequired = true)
		{
			bool isValid = true;

			if ( string.IsNullOrWhiteSpace( ctid ) )
			{
				if ( isRequired )
					messages.Add( "Error - A CTID must be entered for " + property );
				return false;
			}
			//just in case, handle old formats
			//ctid = ctid.Replace( "urn:ctid:", "ce-" );
			if ( ctid.Length != 39 )
			{
				messages.Add( string.Format( "Error - Invalid CTID format for {0}. The proper format is ce-UUID. ex. ce-84365aea-57a5-4b5a-8c1c-eae95d7a8c9b (with all lowercase letters)", property ) );
				return false;
			}

			if ( !ctid.StartsWith( "ce-" ) )
			{
				//actually we could add this if missing - but maybe should NOT
				messages.Add( "Error - The CTID property must begin with ce-" );
				return false;
			}
			//now we have the proper length and format, the remainder must be a valid guid
			if ( !IsValidGuid( ctid.Substring( 3, 36 ) ) )
			{
				//actually we could add this if missing - but maybe should NOT
				messages.Add( string.Format( "Error - Invalid CTID format for {0}. The proper format is ce-UUID. ex. ce-84365aea-57a5-4b5a-8c1c-eae95d7a8c9b (with all lowercase letters)", property ) );
				return false;
			}

			return isValid;
		}
		#endregion

		#region Mapping addresses, juridictions, etc
		public static List<MOPlace> FormatAvailableAt(List<SMI.Address> input)
		{
			List<string> messages = new List<string>();
			return FormatAvailableAtList( input, ref messages );


		}

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
		public static List<RMI.Jurisdiction> MapJurisdictions(List<SMI.JurisdictionProfile> list, ref List<string> messages)
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


		public static RMI.Jurisdiction MapToJurisdiction(SMI.JurisdictionProfile profile, ref List<string> messages)
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
					if( item.HasAddress() )
						entity.JurisdictionException.Add( FormatPlace( item, true, ref messages ) );
				}
			}


			return entity;
		}


		#endregion
		#region conditions, connections
		public List<RMI.ConditionProfile> MapConditionProfiles(List<SMI.ConditionProfile> input, ref List<string> messages)
		{
			var output = new List<RMI.ConditionProfile>();

			if( input == null || input.Count == 0 )
				return output;

			string status = "";
			bool isUrlPresent = true;

			foreach ( var item in input )
			{
				var cp = new RMI.ConditionProfile
				{
					Name = item.Name ?? "" 
				};
				if( string.IsNullOrWhiteSpace( item.Description ) )
				{
					messages.Add( string.Format( "A description must be provided for a condition profile. Name: {0}", cp.Name ?? "Unnamed condition" ) );
				}
				else
					cp.Description = item.Description;

				//
				cp.SubjectWebpage = AssignValidUrlAsString( item.SubjectWebpage, "Condition Profile Subject Webpage", ref messages, false );
				//alternative conditions are very likely - how will the input be formatted?
				//created a simple mapping method assuming only a description of the option is available
				//cp.AlternativeCondition = MapAlternativeConditionProfiles( item.AlternativeCondition, ref messages );
				cp.AlternativeCondition = MapAlternativeConditionProfiles( item, item.AlternativeCondition, ref messages );

				cp.DateEffective = item.DateEffective;
				//list of conditions - that are all applicable for this condition profile
				//Note alternativeConditions should be used when there is a list of mutually exclusive options
				cp.Condition = item.Condition;

				cp.SubmissionOf = item.SubmissionOf;
				cp.SubmissionOfDescription = item.SubmissionOfDescription;
				//typically the condition is asserted by NavyCOOL or the provider 
				//either a CTID could be provide, or an organization reference.
				//TODO - clarify input for orgReference
				cp.AssertedBy = MapToOrgReferences( item.AssertedBy );

				cp.Experience = item.Experience;
				cp.MinimumAge = item.MinimumAge;
				cp.YearsOfExperience = item.YearsOfExperience;

				//

				//costs
				//cp.EstimatedCost = MapToEstimatedCosts( item.EstimatedCost );

				//jurisdictions
				//foreach( SMI.JurisdictionProfile jp in item.Jurisdiction )
				//{
				//	cp.Jurisdiction.Add( MapToJurisdiction( jp, ref messages ) );
				//}

				//targets
				//foreach( var ta in item.TargetCredential )
				//{
				//	cp.TargetCredential.Add( MapToEntityRef( ta ) );
				//}
				foreach ( var ta in item.TargetAssessment )
				{
					if (ta != null )
						cp.TargetAssessment.Add( MapToAsmtToEntityRef( ta, ref messages ) );
				}
				//foreach( var ta in item.TargetLearningOpportunity )
				//{
				//	cp.TargetLearningOpportunity.Add( MapToEntityRef( ta ) );
				//}

				//foreach( var ta in item.TargetCompetency )
				//{
				//	cp.TargetCompetency.Add( MapCompetencyToCredentialAlignmentObject( ta ) );

				//}

				output.Add( cp );
			}
			return output;
		}

		/// <summary>
		/// Create an Alternative Condition Profile using:
		/// Name - maybe name from top condition profile - the name would have to indicate this is one of the options, or set to Option n
		///			- or code hard code the name from the calling line, if able to derive a 'good' name.
		/// Description is from the list of options
		/// pipe delimited string of “name|description” ie. “Option 2| Candidate must provide a statement of graduation”  or “|Must be 21” – Note no name given
		/// </summary>
		/// <param name="conditionProfile"></param>
		/// <param name="list"></param>
		/// <param name="messages"></param>
		/// <returns></returns>
		public static List<RMI.ConditionProfile> MapAlternativeConditionProfiles( SMI.ConditionProfile conditionProfile, List<string> optionsList, ref List<string> messages)
		{
			if ( optionsList == null || optionsList.Count == 0 )
				return null;

			if ( conditionProfile == null || string.IsNullOrWhiteSpace( conditionProfile.Description ) )
				return null;
			List<RMI.ConditionProfile> output = new List<RMI.ConditionProfile>();
			int cntr = 0;
			foreach (var item in optionsList )
			{
				cntr++;
				string[] parts = item.Split( '|' );
				if ( parts.Count() > 0 )
				{
					var name = "";
					var desc = "";
					if ( parts.Count() > 1 )
					{
						name = parts[ 0 ];
						desc = parts[ 1 ];
					}
					else
					{
						//any default for name?
						desc = parts[ 0 ];
					}

					RMI.ConditionProfile acp = new RMI.ConditionProfile()
					{
						Name = name,
						Description = desc
					};
					if (conditionProfile.AssertedBy != null && conditionProfile.AssertedBy.Count() > 0)
					{
						acp.AssertedBy = conditionProfile.AssertedBy;
					}
					output.Add( acp );
				}
			}

			return output;
		}
		#endregion

		#region Entity References 
		public static RMI.EntityReference MapToAsmtToEntityRef(SMI.EntityReference entity, ref List<string> messages )
		{
			RMI.EntityReference refOut = new RMI.EntityReference();
			//requires checking 
			if ( string.IsNullOrWhiteSpace( entity.CTID ) )
			{
				if ( string.IsNullOrWhiteSpace( entity.Name )
					&& string.IsNullOrWhiteSpace( entity.Description )
					&& string.IsNullOrWhiteSpace( entity.SubjectWebpage ) )
					return null;

				if ( string.IsNullOrWhiteSpace( entity.Name ))
					messages.Add( "Error: A name must be provided for an Assessment reference." );
				else 
					refOut.Name = entity.Name;
				//
				if ( string.IsNullOrWhiteSpace( entity.Description ) )
					messages.Add( "Error: A Description must be provided for an Assessment reference." );
				else
					refOut.Description = entity.Description;
				//
				if ( string.IsNullOrWhiteSpace( entity.SubjectWebpage ) )
					messages.Add( "Error: A SubjectWebpage must be provided for an Assessment reference." );
				else
					refOut.SubjectWebpage = entity.SubjectWebpage;
				//set the type. 
				refOut.Type = "ceterms:AssessmentProfile";
			}
			else
			{
				//just output CTID
				refOut.CTID = entity.CTID.ToLower();
			}
			return refOut;
		}

		#endregion

		#region Organization references
		public static List<RMI.OrganizationReference> MapToOrgReferences(string ctid, bool isQAReference)
		{
			List<RMI.OrganizationReference> output = new List<RMI.OrganizationReference>();
			RMI.OrganizationReference orgRef = new RMI.OrganizationReference();
			orgRef.CTID = ctid.ToLower();
			if ( isQAReference )   
				orgRef.Type = "QACredentialOrganization";
			else
				orgRef.Type = "CredentialOrganization";
			output.Add( orgRef );
			return output;
		}
		public static List<RMI.OrganizationReference> MapToOrgReferences( List<SMI.OrganizationReference> input )
		{
			List<RMI.OrganizationReference> output = new List<RMI.OrganizationReference>();
			RMI.OrganizationReference or = new RMI.OrganizationReference();
			foreach( var agency in input )
			{
				output.Add( MapToOrgReference( agency ) );
			}

			return output;
		}
		public static List<RMI.OrganizationReference> MapToOrgReferences(SMI.OrganizationReference org)
		{
			List<RMI.OrganizationReference> list = new List<RMI.OrganizationReference>();
			if( org == null )
				return list;
			RMI.OrganizationReference refOut = MapToOrgReference( org );
			list.Add( refOut );
			return list;

		}
		public static RMI.OrganizationReference MapToOrgReference(SMI.OrganizationReference org)
		{
			RMI.OrganizationReference refOut = new RMI.OrganizationReference();
			if( org == null )
				return refOut;

			//??these should just be set to @Id???
			if( string.IsNullOrWhiteSpace( org.CTID ) )
			{
				refOut.Name = org.Name;
				refOut.Description = org.Description;
				refOut.SubjectWebpage = org.SubjectWebpage;
				//set the type. 
				if( (org.Type ?? "").IndexOf("QA") > -1 )
					refOut.Type = "QACredentialOrganization";
				else
					refOut.Type = "CredentialOrganization";

				if ( org.SocialMedia != null && org.SocialMedia.Count > 0 )
				{
					//not sure we need to handle for test purposes
					refOut.SocialMedia = new List<string>();
					foreach ( var item in org.SocialMedia )
					{
						refOut.SocialMedia.Add( item );
					}
				}
			}
			else
			{
				//just pass CTID to allow formatting for community as needed
				refOut.CTID = org.CTID.ToLower();
				if ( ( org.Type ?? "" ).IndexOf( "QA" ) > -1 )//would need a means to indicate this
					refOut.Type = "QACredentialOrganization";
				else
					refOut.Type = "CredentialOrganization";

			}

			return refOut;
		}//

		#endregion

		public RMI.DurationProfile AssignDuration( string input, ref List<string> messages, bool isRequired = false, bool exactDurationOnly = false)
		{
			var profile = new RMI.DurationProfile();
			if ( string.IsNullOrWhiteSpace( input ) )
				return profile;

			int rowNbr = 1;//for now
			int amt = 0;
			string unit = "";
			//should only be two, but could allow a range:
			//99 xxxxx~99 yyyyy
			if ( input.IndexOf( "~" ) == -1 )
			{
				string[] parts = input.Split( ' ' );
				//expecting amt first
				if ( parts.Count() > 0 )
				{
					if ( !Int32.TryParse( parts[ 0 ], out amt ) )
					{
						messages.Add( string.Format( "Row: {2} Invalid amount (integer) value of {0} for column: {1}. Must be a format like 12 hours, or 2 years", parts[ 0 ], "Duration", rowNbr ) );
						return profile;
					}
					if ( parts.Count() > 1 )
					{
						unit = parts[ 1 ];
						if ( string.IsNullOrWhiteSpace( unit ) )
						{
							messages.Add( string.Format( "Row: {2} Invalid duration unit of {0} for column: {1}. Must be a format years, months, weeks, or hours.", parts[ 0 ], "Duration", rowNbr ) );
							return profile;
						}
					}
					profile.ExactDuration = PopulateDuration( rowNbr, unit, amt, ref messages );
				}
			}
			else
			{
				string[] ranges = input.Split( '~' );
				if ( ranges.Count() > 0 )
				{
					string[] parts = ranges[ 0 ].Split( ' ' );
					//expecting amt first
					if ( parts.Count() > 0 )
					{
						if ( !Int32.TryParse( parts[ 0 ], out amt ) )
						{
							messages.Add( string.Format( "Row: {2} Invalid amount (integer) value of {0} for column: {1}. Must be a format like 12 hours, or 2 years", parts[ 0 ], "Duration", rowNbr ) );
							return profile;
						}
						if ( parts.Count() > 1 )
						{
							unit = parts[ 1 ];
							if ( string.IsNullOrWhiteSpace( unit ) )
							{
								messages.Add( string.Format( "Row: {2} Invalid duration unit of {0} for column: {1}. Must be a format years, months, weeks, or hours.", parts[ 0 ], "Duration", rowNbr ) );
								return profile;
							}
						}
						profile.MinimumDuration = PopulateDuration( rowNbr, unit, amt, ref messages );
					}
				}
				if ( ranges.Count() > 1 )
				{
					string[] parts = ranges[ 1 ].Split( ' ' );
					//expecting amt first
					if ( parts.Count() > 0 )
					{
						if ( !Int32.TryParse( parts[ 0 ], out amt ) )
						{
							messages.Add( string.Format( "Row: {2} Invalid amount (integer) value of {0} for column: {1}. Must be a format like 12 hours, or 2 years", parts[ 0 ], "Duration", rowNbr ) );
							return profile;
						}
						if ( parts.Count() > 1 )
						{
							unit = parts[ 1 ];
							if ( string.IsNullOrWhiteSpace( unit ) )
							{
								messages.Add( string.Format( "Row: {2} Invalid duration unit of {0} for column: {1}. Must be a format years, months, weeks, or hours.", parts[ 0 ], "Duration", rowNbr ) );
								return profile;
							}
						}
						profile.MaximumDuration = PopulateDuration( rowNbr, unit, amt, ref messages );
					}
				}
			}

			return profile;
		}
		public RMI.DurationItem PopulateDuration(int rowNbr, string input, int amt, ref List<string> messages)
		{
			RMI.DurationItem profile = new RMI.DurationItem();
			string durationUnit = input.ToLower();
			switch ( durationUnit )
			{
				case "years":
				case "year":
					profile.Years = amt;
					break;
				case "months":
				case "month":
					profile.Months = amt;
					break;
				case "weeks":
				case "week":
					profile.Weeks = amt;
					break;
				case "days":
				case "day":
					profile.Days = amt;
					break;
				case "hours":
				case "hour":
					profile.Hours = amt;
					break;
				case "minutes":
				case "minute":
					profile.Minutes = amt;
					break;
				default:

					messages.Add( string.Format( "Error - Invalid unit was entered for duration. Row: {0}, Unit: {1}", rowNbr, input ) );
					break;
			}

			return profile;
		}

		public static List<string> MapStringToList(string input)
		{
			List<string> output = new List<string>();
			if ( string.IsNullOrWhiteSpace( input ) )
				return output;

			output.Add( input );
			return output;
		}

		#region Data for acronyms
		public static List<SMI.OrganizationReference> GetQAOrganizations()
		{
			//acronym is used for matching, but want to publish the name
			SMI.OrganizationReference refOut = new SMI.OrganizationReference();
			var list = new List<SMI.OrganizationReference>
			{
				new SMI.OrganizationReference()
				{
					Type = "QACredentialOrganization",
					Acronym = "NCCA",
					Name="Institute for Credentialing Excellence",
					Description = "The Institute for Credentialing Excellence, or ICE, is a professional membership association that provides education, networking, and other resources for organizations and individuals who work in and serve the credentialing industry.  ICE is a leading developer of standards for both certification and certificate programs and it is both a provider of and a clearing house for information on trends in certification, test development and delivery, assessment-based certificate programs, and other information relevant to the credentialing community.",
					SubjectWebpage = "https://www.credentialingexcellence.org/p/cm/ld/fid=121",
					CTID = ""       //if CTID is known, only supply it
				},
				new SMI.OrganizationReference()
				{
					Type = "QACredentialOrganization",
					Acronym = "ANSI",
					Name="ANSI National Accreditation Board",
					Description = "The American National Standards Institute (ANSI) is a private, not-for-profit organization dedicated to supporting the U.S.voluntary standards and conformity assessment system and strengthening its impact, both domestically and internationally.",
					SubjectWebpage = "https://anab.ansi.org/credentialing/personnel-certification/directory",
					CTID = ""       //if CTID is known, only supply it
				},
				new SMI.OrganizationReference()
				{
					Type = "QACredentialOrganization",
					Acronym = "ICAC",
					Name="International Certification Accreditation Council (ICAC)",
					Description = "The International Certification Accreditation Council (ICAC) is an alliance of organizations dedicated to assuring competency, professional management, and service to the public by encouraging and setting standards for licensing, certification, and credentialing programs.",
					SubjectWebpage = "https://www.icacnet.org/membershipprograms/accredited-programs/",
					CTID = ""       //if CTID is known, only supply it
				},
				new SMI.OrganizationReference()
				{
					Type = "QACredentialOrganization",
					Acronym = "IAS",
					Name="International Accreditation Service (IAS)",
					Description = "IAS is a nonprofit, public-benefit corporation that has been providing accreditation services since 1975. IAS accredits a wide range of companies and organizations including governmental entities, commercial businesses, and professional associations. IAS accreditation programs are based on recognized national and international standards that ensure domestic and/or global acceptance of its accreditations. IAS is a subsidiary of the International Code Council (ICC), a leader in building safety, and professional association that develops the International Codes.",
					SubjectWebpage = "https://www.iasonline.org/search-accredited-organizations-2/",
					CTID = ""       //if CTID is known, only supply it
				},
				new SMI.OrganizationReference()
				{
					Type = "QACredentialOrganization",
					Acronym = "ABSNC",
					Name="Accreditation Board for Specialty Nursing Certification (ABSNC)",
					Description = "The Accreditation Board for Specialty Nursing Certification (ABSNC), formerly the ABNS Accreditation Council, is the only accrediting body specifically for nursing certification. ABSNC accreditation is a peer-review mechanism that allows nursing certification organizations to obtain accreditation by demonstrating compliance with the highest quality standards available in the industry. Accreditation is available for both ABNS member and non-member organizations.",
					SubjectWebpage = "http://www.nursingcertification.org/absnc/accreditation",
					CTID = ""       //if CTID is known, only supply it
				}
			};



			return list;
		}//

		public static List<SMI.FinancialAssistanceProfile> GetFinancialAssistanceTypes()
		{
			//acronym is used for matching, but want to publish the name
			SMI.FinancialAssistanceProfile refOut = new SMI.FinancialAssistanceProfile();
			var list = new List<SMI.FinancialAssistanceProfile>
			{
				new SMI.FinancialAssistanceProfile()
				{
					Acronym = "GIBILL",
					Name="Funding through GI Bill.",
					Description = "",
					SubjectWebpage = "https://www.benefits.va.gov/gibill/licensing_certification.asp"
				},
				new SMI.FinancialAssistanceProfile()
				{
					Acronym = "COOL",
					Name="NavyCOOL Funding",
					Description = "",
					SubjectWebpage = "https://www.cool.navy.mil/usn/costs_and_funding/navy_cool_funding.htm"
				}
			};



			return list;
		}//

		#endregion

		#region Helpers and validaton
		/// <summary>
		/// Validate a URL and return standardized string.
		/// NOTE this method is not to be used with CTIDs - use AssignRegistryResourceURIAsString
		/// </summary>
		/// <param name="url"></param>
		/// <param name="propertyName"></param>
		/// <param name="messages"></param>
		/// <param name="isRequired"></param>
		/// <param name="doingExistanceCheck">Defaults to true. Set false for registry URIs that may not exists yet.</param>
		/// <returns></returns>
		public string AssignValidUrlAsString(string url, string propertyName, ref List<string> messages, bool isRequired, bool doingExistanceCheck = true)
		{
			string statusMessage = "";
			bool isUrlPresent = true;
			if ( string.IsNullOrWhiteSpace( url ) )
			{
				if ( isRequired )
					messages.Add( string.Format( "The {0} URL is a required property.", propertyName ) );
				return null;
			}
			List<string> temp = new List<string>();
			url = url.Trim();
			//NOTE this method is not to be used with CTIDs
			if ( IsCtidValid( url, propertyName, ref temp ) )
			{
				//NO: don't use this method with CTIDs
				//url = credRegistryGraphUrl + url.ToLower().Trim();
				//return url;
				messages.Add( string.Format( "A CTID is not valid to be provided for the '{0}' property.", propertyName ) );
				return null;
			}

			//note a registry URI may exist if part of the current upload.
			if ( !IsUrlValid( url, ref statusMessage, ref isUrlPresent, doingExistanceCheck ) )
			{
				if ( isUrlPresent )
				{
					if ( WarnOnInvalidUrls )
						warningMessages.Add( string.Format( "The {0} URL ({1}) is invalid: {2} (warning only).", propertyName, url, statusMessage ) );
					else
						messages.Add( string.Format( "The {0} URL ({1}) is invalid: {2}.", propertyName, url, statusMessage ) );
				}
				return null;
			}
			url = AssignUrl( url.TrimEnd( '/' ) );
			return url;
		} //
		  //force registry urls to lowercase
		public string AssignUrl(string url)
		{
			if ( url.ToLower().IndexOf( "/resources/ce-" ) > -1
				|| url.ToLower().IndexOf( "/graph/ce-" ) > -1 )
			{
				return url.ToLower();
			}
			else
				return url;
		} //
		public bool IsUrlValid(string url, ref string statusMessage, ref bool urlPresent, bool doingExistanceCheck = true)
		{
			statusMessage = "";
			urlPresent = true;
			if ( string.IsNullOrWhiteSpace( url ) )
			{
				urlPresent = false;
				return true;
			}
			url = url.Trim();
			if ( !Uri.IsWellFormedUriString( url, UriKind.Absolute ) )
			{
				statusMessage = "The URL is not in a proper format (for example, must begin with http or https).";
				return false;
			}

			//may need to allow ftp, and others - not likely for this context?
			if ( url.ToLower().StartsWith( "http" ) == false )
			{
				statusMessage = "A URL must begin with http or https";
				return false;
			}
			//hack for pattern like https://https://www.sscc.edu
			if ( url.LastIndexOf( "//" ) > url.IndexOf( "//" ) )
			{
				statusMessage = "Invalid format, contains multiple sets of '//'";
				return false;
			}
			if ( !doingExistanceCheck )
				return true;

			var isOk = DoesRemoteFileExists( url, ref statusMessage );
			//optionally try other methods, or again with GET
			if ( !isOk && statusMessage == "999" )
				return true;
			return isOk;
		} //


		/// <summary>
		/// Checks the file exists or not.
		/// </summary>
		/// <param name="url">The URL of the remote file.</param>
		/// <returns>True : If the file exits, False if file not exists</returns>
		public bool DoesRemoteFileExists(string url, ref string responseStatus)
		{
			//this is only a conveniece for testing, and is normally false
			//although will greatly slow down batch publishing
			if ( UtilityManager.GetAppKeyValue( "ra.SkippingLinkChecking", false ) )
				return true;

			bool treatingRemoteFileNotExistingAsError = true; // UtilityManager.GetAppKeyValue( "treatingRemoteFileNotExistingAsError", true );
			//consider stripping off https?
			//or if not found and https, try http
			try
			{
				if ( SkippingValidation( url ) )
					return true;
				//SafeBrowsing.Reputation rep = SafeBrowsing.CheckUrl( url );
				//if ( rep != SafeBrowsing.Reputation.None )
				//{
				//	responseStatus = string.Format( "Url ({0}) failed SafeBrowsing check.", url );
				//	return false;
				//}
				//Creating the HttpWebRequest
				HttpWebRequest request = WebRequest.Create( url ) as HttpWebRequest;
				//NOTE - do NOT use the HEAD option, as many sites reject that type of request
				//		GET seems to be cause false 404s
				//request.Method = "GET";
				//var agent = HttpContext.Current.Request.AcceptTypes;

				//the following also results in false 404s
				//request.ContentType = "text/html;charset=\"utf-8\";image/*";
				//testing
				request.AllowAutoRedirect = true;
				request.Timeout = 10000;  //10 seconds
				request.KeepAlive = false;
				request.Accept = "*/*";

				//UserAgent appears OK
				request.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1309.0 Safari/537.17";

				//users may be providing urls to sites that have invalid ssl certs installed.You can ignore those cert problems if you put this line in before you make the actual web request:
				ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback( AcceptAllCertifications );

				//Getting the Web Response.
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				//Returns TRUE if the Status code == 200
				response.Close();
				if ( response.StatusCode != HttpStatusCode.OK )
				{
					if ( url.ToLower().StartsWith( "https:" ) )
					{
						url = url.ToLower().Replace( "https:", "http:" );
						LoggingHelper.DoTrace( 5, string.Format( "_____________Failed for https, trying again using http: {0}", url ) );

						return DoesRemoteFileExists( url, ref responseStatus );
					}
					else
					{
						var urlStatusCode = ( int )response.StatusCode;
						if ( urlStatusCode == 301 )
						{
							//for ( int i = 0; i < response.Headers.Count; ++i )
							//	Console.WriteLine( "\nHeader Name:{0}, Value :{1}", response.Headers.Keys[ i ], response.Headers[ i ] );
							string location = response.Headers.GetValues( "Location" ).FirstOrDefault();
							if ( !string.IsNullOrWhiteSpace( location ) )
							{
								string clearUrl = url.Replace( "http://", "" ).Replace( "https://", "" ).Trim( '/' );
								string clearLoc = location.Replace( "http://", "" ).Replace( "https://", "" ).Trim( '/' );
								//L: http://www.tesu.edu/about/mission
								//U: http://www.tesu.edu/about/mission.cfm
								if ( location.Replace( "https", "http" ).Trim( '/' ) == url.Trim( '/' )
									|| location.ToLower().Trim( '/' ) == url.ToLower().Trim( '/' )
									|| url.ToLower().IndexOf( location.ToLower() ) == 0 //redirect just trims an extension
									|| clearLoc.ToLower().IndexOf( clearUrl.ToLower() ) > 0
									)
								{
									return true;
								}
								else if ( location.Replace( "mobile.twitter", "twitter" ).ToLower().Trim( '/' ) == url.ToLower().Trim( '/' )
									|| location == "https://www.linkedin.com/error_pages/unsupported-browser.html"
									)
								{
									//Redirect to: https://www.linkedin.com/error_pages/unsupported-browser.html
									return true;
								}

							}

						}
						LoggingHelper.DoTrace( 5, string.Format( "Url validation failed for: {0}, using method: GET, with status of: {1}", url, response.StatusCode ) );
					}
				}
				responseStatus = response.StatusCode.ToString();

				return ( response.StatusCode == HttpStatusCode.OK );
				//apparantly sites like Linked In have can be a  problem
				//http://stackoverflow.com/questions/27231113/999-error-code-on-head-request-to-linkedin
				//may add code to skip linked In?, or allow on fail - which the same.
				//or some update, refer to the latter link

				//
			}
			catch ( WebException wex )
			{
				responseStatus = wex.Message;
				//
				if ( wex.Message.IndexOf( "(404)" ) > 1 )
					return false;
				else if ( wex.Message.IndexOf( "Too many automatic redirections were attempted" ) > -1 )
					return false;
				else if ( wex.Message.IndexOf( "(999" ) > 1 )
					return true;
				else if ( wex.Message.IndexOf( "(400) Bad Request" ) > 1 )
					return true;
				else if ( wex.Message.IndexOf( "(401) Unauthorized" ) > 1 )
					return true;
				else if ( wex.Message.IndexOf( "(406) Not Acceptable" ) > 1 )
					return true;
				else if ( wex.Message.IndexOf( "(500) Internal Server Error" ) > 1 )
					return true;
				else if ( wex.Message.IndexOf( "Could not create SSL/TLS secure channel" ) > 1 )
				{
					//https://www.naahq.org/education-careers/credentials/certification-for-apartment-maintenance-technicians 
					return true;

				}
				else if ( wex.Message.IndexOf( "Could not establish trust relationship for the SSL/TLS secure channel" ) > -1 )
				{
					return true;
				}
				else if ( wex.Message.IndexOf( "The underlying connection was closed: An unexpected error occurred on a send" ) > -1 )
				{
					return true;
				}
				else if ( wex.Message.IndexOf( "Detail=CR must be followed by LF" ) > 1 )
				{
					return true;
				}

				//var pageContent = new StreamReader( wex.Response.GetResponseStream() )
				//		 .ReadToEnd();
				if ( !treatingRemoteFileNotExistingAsError )
				{
					LoggingHelper.LogError( string.Format( thisClassName + ".DoesRemoteFileExists. CurrentEntityName: {0},CurrentCtid: {1}, url: {2}. Exception Message:{3}; URL: {4}", CurrentEntityName, CurrentCtid, url, wex.Message, GetWebUrl() ), true, "SKIPPING - Exception on URL Checking" );

					return true;
				}

				LoggingHelper.LogError( string.Format( thisClassName + ".DoesRemoteFileExists. : EntityType: {0}, EntityName: '{1}', Ctid: {2}, url: {3}. Exception Message:{4}", CurrentEntityType, CurrentEntityName, CurrentCtid, url, wex.Message ), true, "Exception on URL Checking" );
				responseStatus = wex.Message;
				return false;
			}
			catch ( Exception ex )
			{

				if ( ex.Message.IndexOf( "(999" ) > -1 )
				{
					//linked in scenario
					responseStatus = "999";
				}
				else if ( ex.Message.IndexOf( "Could not create SSL/TLS secure channel" ) > 1 )
				{
					//https://www.naahq.org/education-careers/credentials/certification-for-apartment-maintenance-technicians 
					return true;

				}
				else if ( ex.Message.IndexOf( "(500) Internal Server Error" ) > 1 )
				{
					return true;
				}
				else if ( ex.Message.IndexOf( "(401) Unauthorized" ) > 1 )
				{
					return true;
				}
				else if ( ex.Message.IndexOf( "Could not establish trust relationship for the SSL/TLS secure channel" ) > 1 )
				{
					return true;
				}
				else if ( ex.Message.IndexOf( "Detail=CR must be followed by LF" ) > 1 )
				{
					return true;
				}

				if ( !treatingRemoteFileNotExistingAsError )
				{
					LoggingHelper.LogError( string.Format( thisClassName + ".DoesRemoteFileExists url: {0}. Exception Message:{1}", url, ex.Message ), true, "SKIPPING - Exception on URL Checking" );

					return true;
				}

				LoggingHelper.LogError( string.Format( thisClassName + ".DoesRemoteFileExists url: {0}. Exception Message:{1}", url, ex.Message ), true, "Exception on URL Checking" );
				//Any exception will returns false.
				responseStatus = ex.Message;
				return false;
			}
		}
		public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}
		private static bool SkippingValidation(string url)
		{
			Uri myUri = new Uri( url );
			string host = myUri.Host;

			string exceptions = UtilityManager.GetAppKeyValue( "urlExceptions" );
			//quick method to avoid loop
			if ( exceptions.IndexOf( host ) > -1 )
				return true;


			//string[] domains = exceptions.Split( ';' );
			//foreach ( string item in domains )
			//{
			//	if ( url.ToLower().IndexOf( item.Trim() ) > 5 )
			//		return true;
			//}

			return false;
		}
		/// <summary>
		/// Get the current url for reporting purposes
		/// </summary>
		/// <returns></returns>
		private static string GetWebUrl()
		{
			string queryString = "n/a";

			if ( HttpContext.Current != null && HttpContext.Current.Request != null )
				queryString = HttpContext.Current.Request.RawUrl.ToString();

			return queryString;
		}


		public int StringToInt(string value, int defaultValue)
		{
			int returnValue = defaultValue;
			if ( Int32.TryParse( value, out returnValue ) == true )
				return returnValue;
			else
				return defaultValue;
		}


		public bool StringToDate(string value, ref DateTime returnValue)
		{
			if ( System.DateTime.TryParse( value, out returnValue ) == true )
				return true;
			else
				return false;
		}

		/// <summary>
		/// IsInteger - test if passed string is an integer
		/// </summary>
		/// <param name="stringToTest"></param>
		/// <returns></returns>
		public bool IsInteger(string stringToTest)
		{
			int newVal;
			bool result = false;
			try
			{
				newVal = Int32.Parse( stringToTest );

				// If we get here, then number is an integer
				result = true;
			}
			catch
			{

				result = false;
			}
			return result;

		}


		/// <summary>
		/// IsDate - test if passed string is a valid date
		/// </summary>
		/// <param name="stringToTest"></param>
		/// <returns></returns>
		public bool IsDate(string stringToTest, bool doingReasonableCheck = true)
		{

			DateTime newDate;
			bool result = false;
			try
			{
				newDate = System.DateTime.Parse( stringToTest );
				result = true;
				//check if reasonable - may what a lower date, for older organizations
				if ( doingReasonableCheck && newDate < new DateTime( 1800, 1, 1 ) )
					result = false;
			}
			catch
			{
				result = false;
			}
			return result;

		} //end

		public string MapDate(string date, string dateName, ref List<string> messages, bool doingReasonableCheck = true)
		{
			if ( string.IsNullOrWhiteSpace( date ) )
				return null;

			DateTime newDate = new DateTime();

			if ( DateTime.TryParse( date, out newDate ) )
			{
				if ( doingReasonableCheck && newDate < new DateTime( 1800, 1, 1 ) )
					messages.Add( string.Format( "Error - {0} is out of range (prior to 1800-01-01) ", dateName ) );
				else
				{
					//check if input was just two parts: 1900 01. Also may want to accomodate December 2001?
					//NOTE: may make this configurable, so allowed for dateCopywrite, not dateCreated
					var parts = date.Trim().Split( ' ' );
					if ( parts.Count() == 2 )
					{
						if ( parts[ 0 ].IndexOf( "-" ) == -1 && parts[ 0 ].IndexOf( "/" ) == -1 && parts[ 1 ].IndexOf( ":" ) == -1 )
						{
							//just return input
							return date;
						}
					}
					else
					{
						var parts2 = date.Trim().Split( '-' );
						if ( parts2.Count() == 2 )
						{
							//just return input
							return date;
						}
					}

				}
			}
			else
			{
				messages.Add( string.Format( "Error - {0} is invalid: '{1}' ", dateName, date ) );
				return null;
			}
			return newDate.ToString( "yyyy-MM-dd" );

		} //end

		public string MapDateTime(string date, string dateName, ref List<string> messages, bool doingReasonableCheck = true)
		{
			if ( string.IsNullOrWhiteSpace( date ) )
				return null;

			DateTime newDate = new DateTime();
			CultureInfo provider = CultureInfo.InvariantCulture;
			//if valid, maybe should leave as is, if can determine format

			if ( DateTime.TryParse( date, out newDate ) )
			{
				if ( doingReasonableCheck && newDate < new DateTime( 1800, 1, 1 ) )
					messages.Add( string.Format( "Error - {0} is out of range (prior to 1800-01-01) ", dateName ) );

				var outputDate = newDate.ToString( "u" );
				if ( dateName == "Rating.UploadDate" )
				{
					//what is the needed fix
				}
				var outputDate1 = newDate.ToString( "yyyy-MM-dd hh:mm:ss" );
				var outputDate2 = newDate.ToString( "yyyy-MM-ddThh:mm:ssZ" );
			}
			else
			{
				messages.Add( string.Format( "Error - {0} of {1} is invalid ", dateName, date ) );
				return null;
			}

			//if ( DateTime.TryParse( date, provider, DateTimeStyles.None, out newDate ) )
			//{

			//}
			//may want to leave date as is, if already in proper format?
			return newDate.ToString( "yyyy-MM-dd hh:mm:ssZ" );
			//return newDate.ToUniversalTime().ToString(); 

		} //end
		/// <summary>
		/// Only check for valid year
		/// </summary>
		/// <param name="date"></param>
		/// <param name="dateName"></param>
		/// <param name="messages"></param>
		/// <param name="doingReasonableCheck"></param>
		/// <returns></returns>
		public string MapYear(string date, string dateName, ref List<string> messages, bool doingReasonableCheck = true)
		{
			if ( string.IsNullOrWhiteSpace( date ) )
				return null;

			var year = 0;
			if ( Int32.TryParse( date, out year ) )
			{
				if ( year < 1800 || year > DateTime.Now.Year )
				{
					messages.Add( string.Format( "Error - {0} is out of range (prior to 1800 or greater than the current year) ", dateName ) );
					return null;
				}
			}
			else
			{
				messages.Add( string.Format( "Error - {0} is not a valid year.", dateName ) );
				return null;
			}
			return date;
		} //end

		public bool IsValidGuid(Guid field)
		{
			if ( ( field == null || field == Guid.Empty ) )
				return false;
			else
				return true;
		}
		public bool IsValidGuid(string field)
		{
			Guid guidOutput;
			if ( ( field == null || field.ToString() == Guid.Empty.ToString() ) )
				return false;
			else if ( !Guid.TryParse( field, out guidOutput ) )
				return false;
			else
				return true;
		}
		

		/// <summary>
		/// Convert a comma-separated list (as a string) to a list of integers
		/// </summary>
		/// <param name="csl">A comma-separated list of integers</param>
		/// <returns>A List of integers. Returns an empty list on error.</returns>
		public List<int> CommaSeparatedListToIntegerList(string csl)
		{
			try
			{
				return CommaSeparatedListToStringList( csl ).Select( int.Parse ).ToList();
			}
			catch
			{
				return new List<int>();
			}

		}

		/// <summary>
		/// Convert a comma-separated list (as a string) to a list of strings
		/// </summary>
		/// <param name="csl">A comma-separated list of strings</param>
		/// <returns>A List of strings. Returns an empty list on error.</returns>
		public List<string> CommaSeparatedListToStringList(string csl)
		{
			try
			{
				return csl.Trim().Split( new string[] { "," }, StringSplitOptions.RemoveEmptyEntries ).ToList();
			}
			catch
			{
				return new List<string>();
			}
		}

		#endregion

		#region Helpers for Json, validation, etc.
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

		#endregion
	}
}
