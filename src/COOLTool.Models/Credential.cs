
namespace COOLTool.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Credential
    {
        public int CE_CertID { get; set; }
        public int CA_AgencyID { get; set; }
        public string CE_CertTitle { get; set; }
        public string CE_CertAcronym { get; set; }
        public string CE_CertURL { get; set; }
        public int CE_CertStatus { get; set; }
        public string CE_CertType { get; set; } // This indicates if National Credential, Federal License, or State License
        public string CE_CertDescription { get; set; }
    
        public Agency Agency { get; set; }

        //additions
        public string CTID { get; set; }
    }
}
