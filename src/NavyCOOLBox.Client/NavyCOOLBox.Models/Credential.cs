
namespace NavyCOOLBox.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Credential : ARTTCredential
    {
        //public int CE_CertID { get; set; }
        //public int CA_AgencyID { get; set; }
        //public string CE_CertTitle { get; set; }
        //public string CE_CertAcronym { get; set; }
        //public string CE_CertURL { get; set; }
        //public int CE_CertStatus { get; set; }
        //public string CE_CertType { get; set; } // This indicates if National Credential, Federal License, or State License
        //public string CE_CertDescription { get; set; }
    
        //public Agency Agency { get; set; }

        ////additions
        //public string CTID { get; set; }
    }
    public class ARTTCredential
    {
        public string CTID { get; set; }

        public string CA_AgencyCTID { get; set; }

        public string CE_CertTitle { get; set; }

        public string CE_CertAcronym { get; set; }

        public string CE_CertURL { get; set; }

        public int CE_CertStatus { get; set; }  // does the CE schema have a field for credential status?
        /*
         *Credential Type CodeCredential Type Text
         * N    National Credential
         * F    Federal License
         * S    State License
         * U    Unknown
         * W    STCW 
         */
        public string CE_CertType { get; set; }

        public string CE_CertDescription { get; set; }

        public List<string> cred_Rating { get; set; } = new List<string>();  // as discussed, this will be a list of Rating CTIDs

        public List<string> cred_ONET { get; set; } = new List<string>();  // as discussed, this will be a list of actual O*NET Codes


    }
}
