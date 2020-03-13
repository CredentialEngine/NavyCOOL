using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Solid.Models
{
	public class CredentialDTO
	{
		public CredentialDTO()
		{

		}
		/*
		 * 

cred_ResourceIcons.Resource_Type
cred_ResourceIcons.Resource_Type
cred_ResourceIcons.Resource_Type
moc_credential.MOC_ID
cred_Requirements.REQ_Description
cred_RenewalRequirements.Renewal_ValueNum
cred_MinimumRequirements
cred_MinimumRequirements
cred_MinimumRequirements
cred_MinimumRequirements
cred_Requirements.REQ_Description
cred_MinimumRequirements
cred_MinimumRequirements
cred_RenewalRequirements
cred_Requirements.REQ_Description
cred_RenewalRequirements
cred_ExamPrep
MOC.MOC_ServiceCode
MOC.MOC_PersonnelCategory
moc_credential.MOCCE_LinkedAs
		 * 
		 */
		#region Basic Information 
		public int CE_CertID { get; set; }
		public string CTID { get; set; }
		//Title of the credential offered
		public string CE_CertTitle { get; set; }
		//Official acronym of credential
		public string CE_CertAcronym { get; set; }
		//Website for information about the credential
		//this will be on the NavyCOOL site. 
		//Use AvailableOnlineAt for the provide URL
		public string CE_CertURL { get; set; }

		//Description
		public string CE_CertDescription { get; set; }

		/// <summary>
		/// This indicates if National Credential, Federal License, or State License
		/// NOTE: may need conversion for state license and federal license. The type would be licence, and for State, may suggest the need for a Jurisdiction Profile
		/// </summary>
		public string CE_CertType { get; set; }

		/// <summary>
		/// The status of the Credential.
		/// Required, although API will default to Active if not provided. 
		/// </summary>
		public string CredentialStatusType { get; set; } = "Active";

		//what is the FK to the owning organization
		//could get org ctid by reading agency
		public string OwnedByOrganizationCTID { get; set; }
		//Certificate, License
		public string CredentialType { get; set; }

		public int CA_AgencyID { get; set; }

		//provider
		//public AgencyDTO Agency { get; set; } = new AgencyDTO();

		//public string ProviderName
		//{
		//	get
		//	{
		//		if( Agency != null && !string.IsNullOrWhiteSpace( Agency.CA_AgencyName ) )
		//			return Agency.CA_AgencyName;
		//		else
		//			return "MISSING";
		//	}
		//}
		//need the provider CTID for the ownedBy and/or offeredBy properties
		//public string ProviderCTID
		//{
		//	get
		//	{
		//		if( Agency != null && !string.IsNullOrWhiteSpace( Agency.CTID ) )
		//			return Agency.CTID;
		//		else
		//			return "";
		//	}
		//}



		//Url to the provider site
		public string AvailableOnlineAt { get; set; }

		//??
		public string ImageUrl { get; set; }

		/// <summary>
		/// HasRating
		/// Rating related to this resource.
		/// URI to a Rating
		/// NOTE: API will accept just the CTID and properly format the URI based on the target environment
		/// </summary>
		public List<string> HasRating { get; set; } = new List<string>();

		/// <summary>
		/// List of valid O*Net codes. See:
		/// https://www.onetonline.org/find/
		/// </summary>
		public List<string> ONET_Codes { get; set; } = new List<string>();

		/// <summary>
		/// AlternativeOccupations
		/// If there are occupations that are not part of ONet, these can be provided in AlternativeOccupations
		/// </summary>
		public List<string> AlternativeOccupations { get; set; } = new List<string>();
		#endregion

		//Language - will there be alternate languages, or always Englist?
		//The API will add the default of en-US if no languages are provided.
		public List<string> InLanguage { get; set; } 
		#region Conditions
		//condition profile
		public Cred_Requirements Requirements { get; set; } = new Cred_Requirements();

		public List<ConditionProfile> Requires { get; set; } = new List<ConditionProfile>();
		public List<ConditionProfile> Recommends { get; set; } = new List<ConditionProfile>();

		#endregion

		public List<FinancialAssistanceProfile> FinancialAssistance { get; set; } = new List<FinancialAssistanceProfile>();

		public List<JurisdictionProfile> Jurisdiction { get; set; }

		public bool HasGIBillReimbursement { get; set; }
		public bool IsInDemand { get; set; }

		#region Quality Assurance
		public List<OrganizationReference> AccreditedBy { get; set; } = new List<OrganizationReference>();
		public List<OrganizationReference> ApprovedBy { get; set; } = new List<OrganizationReference>();

		#endregion

		//list of MOC_IDs
		public List<int> MOCList { get; set; }

		//public List<Cred_ResourceIcons> Resources { get; set; } = new List<Cred_ResourceIcons>();



		//The number of years a credential is valid before renewal is required
		//Is this just an integer (i.e. always full years)
		public int RenewalPeriod { get; set; }

		/* one or more of
			Continuing Education
			Exam
			Continuing Education OR Exam
			Fee
			Other		 
			 
		*/

		public List<string> RenewalConditions = new List<string>();

		public static string DurationAsSchemaDuration(int years)
		{
			string duration = "P";

			if ( years > 0 )
			{
				duration += years.ToString() + "Y";
			}
			

			return duration;
		}

		public List<string> Keywords { get; set; } = new List<string>();

		#region Properties to review and remove if not relevent
		public List<Address> Addresses { get; set; } = new List<Address>();

		public string ISICV4 { get; set; }
		public string Version { get; set; }
		public string LatestVersionUrl { get; set; }
		public string PreviousVersion { get; set; }
		#endregion

		#region Optional information related to publishing

		//could optionally store the identifier from the credential registry, also known as the envelopeId
		public string CredentialRegistryId { get; set; }
		//publish information??
		public string LastPublishDate { get; set; } = "";
		public bool IsPublished { get; set; }

		//approvals ???
		public bool IsApproved { get; set; }
		public int ContentApprovedById { get; set; }
		public string ContentApprovedBy { get; set; }
		public string LastApprovalDate { get; set; } = "";

		#endregion

	}
}
