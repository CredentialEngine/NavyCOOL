using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace COOLTool.Services.Models.Input
{
	/// <summary>
	/// Class used with an Organization format or publish request
	/// </summary>
	public class OrganizationRequest : BaseRequest
	{
		public OrganizationRequest()
		{
			Agency = new Agency();
		}

		/// <summary>
		/// Organization Input Class
		/// </summary>
		public Agency Agency { get; set; }

	}

	/// <summary>
	/// Keep in sync with version in NavyCOOLBox.Models
	/// </summary>
	public class Agency
	{
		public Agency()
		{
			CTDL_Type = "CredentialOrganization";
			ISQAOrganization = false;
			IsACredentialingOrg = true;

			//Industry = new Enumeration();
			//AlternativeIndustries = new List<TextValueProfile>();
		}
		//presume an  identifier of some sort
		public int CA_AgencyID { get; set; }
		public string CTDL_Type { get; set; }
		/// <summary>
		/// CTID will be required for publishing.
		/// We considered whether a lookup could be done by name and subject webpage.
		/// However, we will need the org CTID when publishing a credential.
		/// NOTE: is there a uniqueidentifier already part of the agency table? - if so could set to lowercase and add a prefix of 'ce-'
		/// </summary>
		public string CTID { get; set; }

		public string CA_AgencyName { get; set; }
		//use for AlternateName
		public string CA_AgencyAcronym { get; set; }
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
		public bool IsACredentialingOrg { get; set; }

		public bool ISQAOrganization { get; set; }
		//public string AgentPurpose { get; set; }
		//public string AgentPurposeDescription { get; set; }
		//public string MissionAndGoalsStatement { get; set; }
		//public string MissionAndGoalsStatementDescription { get; set; }


		private static string GetListSpaced(string input)
		{
			return string.IsNullOrWhiteSpace( input ) ? "" : input + " ";
		}




	} //
}
