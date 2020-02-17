using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOLTool.Models
{
	public class Assessment
	{
        public int CE_ID { get; set; }
        public int CA_AgencyID { get; set; }
        public string CE_Title { get; set; }
        public string CE_Description { get; set; }
        public string CE_Acronym { get; set; }  //codedNotation
        public string CE_URL { get; set; } //subjectWebpage
        public int CE_Status { get; set; }

        public Agency Agency { get; set; }  //use for ownedBy

        //additions
        public string CTID { get; set; }
        /*publishing requires at least one of:
         * AvailabilityListing
         * AvailableOnlineAt
         * AvailableAt (address)
         */
        public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
