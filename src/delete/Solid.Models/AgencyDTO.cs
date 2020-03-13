using System;
using System.Collections.Generic;
using System.Linq;


namespace Solid.Models
{
    [Serializable]
    public class AgencyDTO
	{
		public AgencyDTO()
		{
			ISQAOrganization = false;
			IsACredentialingOrg = true;

			//Industry = new Enumeration();
			//AlternativeIndustries = new List<TextValueProfile>();
		}
		//presume an  identifier of some sort
		public int CA_AgencyID { get; set; }
		/// <summary>
		/// CTID will be required for publishing.
		/// We considered whether a lookup could be done by name and subject webpage.
		/// However, we will need the org CTID when publishing a credential.
		/// NOTE: is there a uniqueidentifier already part of the agency table? - if so could set to lowercase and add a prefix of 'ce-'
		/// </summary>
		public string CTID { get; set; }

		public string CA_AgencyName { get; set; }
		public string Description { get; set; }
		public string CA_AgencyStreetAddress1 { get; set; }
		public string CA_AgencyCity { get; set; }
		public string CA_AgencyState { get; set; }
		public string CA_AgencyZip { get; set; }
		public string CA_AgencyCountry { get; set; }
		public string CA_AgencyPhonePrimary { get; set; }

		//note: this should be the home page. 
		//For FAA it is a support page: Submit a question to our support team
		public string CA_AgencyHomePageURL { get; set; }


		//Agency.CA_AgencyContact (email or webpage)
		//need to know which. Already have a subject webpage
		public string CA_AgencyContact { get; set; }
		//TODO - should require an email where the organization will be automatically added the CE accounts site.

		public List<string> AgentType { get; set; } = new List<string>();
		/*
		 * Private For-Profit  
		 * Private Not-For-Profit  
		 * Public Institution 
		 * 
		 */
		public string AgentSectorType { get; set; }

		//OrganizationType is saved as an OrganizationProperty
		//public Enumeration OrganizationType { get; set; }
		
		public bool IsThirdPartyOrganization { get; set; }
		public bool IsACredentialingOrg { get; set; }

		public bool ISQAOrganization { get; set; }
		public string AgentPurpose { get; set; }
		public string AgentPurposeDescription { get; set; }
		public string MissionAndGoalsStatement { get; set; }
		public string MissionAndGoalsStatementDescription { get; set; }


		//public string Duns { get; set; }
		//public string Fein { get; set; }
		//public string IpedsId { get; set; }
		//public string OpeId { get; set; }
		//public string LEICode { get; set; }
		/// <summary>
		/// ISIC Revision 4 Code
		/// The International Standard of Industrial Classification of All Economic Activities (ISIC), Revision 4 code for a particular organization, business person, or place.
		/// </summary>
		//public string ISICV4 { get; set; }
		/// <summary>
		/// Identifier comprised of a 12 digit code issued by the National Center for Education Statistics (NCES) for educational institutions where the first 7 digits are the NCES District ID.
		/// </summary>
		//public string NcesID { get; set; }

		private static string GetListSpaced(string input)
		{
			return string.IsNullOrWhiteSpace( input ) ? "" : input + " ";
		}
		//public Enumeration Industry { get; set; }
		/// <summary>
		/// Concat Industry and OtherIndustry
		/// </summary>
		//public Enumeration IndustryType
		//{
		//	get
		//	{
		//		return new Enumeration()
		//		{
		//			Items = new List<EnumeratedItem>()
		//			.Concat( Industry.Items )
		//			.Concat( AlternativeIndustries.ConvertAll( m => new EnumeratedItem() { Name = m.TextTitle, Description = m.TextValue } ) ).ToList()
		//		};
		//	}
		//	set { Industry = value; }
		//} //Used for publishing
		public List<TextValueProfile> AlternativeIndustries { get; set; }



    } //

}
