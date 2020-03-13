using System;
using System.Collections.Generic;

namespace NavyCOOLBox.Models
{    
    public partial class Agency
    {
        public Agency()
        {
            this.CA_AgencyCredentialType = "N";
            this.CA_AgencyCountry = "USA";
            this.CA_AgencyStatus = 1;
            this.Credentials = new HashSet<Credential>();
        }

        public string CTDL_Type { get; set; }

        public int CA_AgencyID { get; set; }

        public string CA_AgencyName { get; set; }
        //use for AlternateName
        public string CA_AgencyAcronym { get; set; }
        public string CA_AgencyHomePageURL { get; set; }
        
        /// <summary>
        /// If has @, treat as email
        /// </summary>
        public string CA_AgencyContact { get; set; }  // This could be a URL to their "Contact Us" page or an email address, need to look at the actual data to know which
        
        //how is this used. Are all credentials for a provider of the same type
        public string CA_AgencyCredentialType { get; set; }  // This indicates if National Credential, Federal License, or State License agency
        public string CA_AgencyStreetAddress1 { get; set; }
        public string CA_AgencyStreetAddress2 { get; set; }
        public string CA_AgencyCity { get; set; }
        public string CA_AgencyState { get; set; }
        public string CA_AgencyZip { get; set; }
        public string CA_AgencyCountry { get; set; }
        public string CA_AgencyPhonePrimary { get; set; }
        public Nullable<int> CA_AgencyPhonePrimaryExtension { get; set; }
        public string CA_AgencyPhoneSecondary { get; set; }
        public string CA_AgencyFax { get; set; }
        public int CA_AgencyStatus { get; set; }
    
        public virtual ICollection<Credential> Credentials { get; set; }

        //additions
        public string CTID { get; set; }
        //TODO - should require an email where the organization will be automatically added the CE accounts site.

        /// <summary>
        /// Apparantly a description is available somewhere?
        /// </summary>
        public string Description { get; set; }
        public List<string> AgentType { get; set; } = new List<string>();
        /*
		 * Private For-Profit  
		 * Private Not-For-Profit  
		 * Public Institution 
		 * 
		 */
        public string AgentSectorType { get; set; }
    }
}
