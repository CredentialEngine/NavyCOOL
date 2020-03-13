using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace COOLTool.Services.Models.Input
{
	/// <summary>
	/// Class used with an Credential format or publish request
	/// </summary>
	public class CredentialRequest : BaseRequest
	{
		public CredentialRequest()
		{
			Credential = new Credential();
		}

		/// <summary>
		/// Credential Input Class
		/// </summary>
		public Credential Credential { get; set; }
	}
	public class Credential : ARTTCredential
	{

		//candidates to add
		//TBD
		public string AvailableOnlineAt { get; set; }

		//jurisdiction 

		//accreditedBy info
		public List<OrganizationReference> AccreditedBy { get; set; } = new List<OrganizationReference>();

		//renewal frequency

		//keywords?
	}
	public class ARTTCredential
	{
		public string CTID { get; set; }

		public string CA_AgencyCTID { get; set; }

		public string CE_CertTitle { get; set; }

		public string CE_CertAcronym { get; set; }
		//URL to the NavyCOOL site
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
	//this class has to match the one used in the NavyCOOLBox.Client equivalent
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


		} //
	public class Cred_Requirements
	{

		public int Id { get; set; }
		public string Description { get; set; }


		//Indidates there are no requirements to obtain credential
		//This might suggest no condition profile, or just a description to indicate no requirements?
		//	what would be an example of this?
		public bool NoRequirements { get; set; }

		//is this data in one property or four?
		public string MinimumRequirements { get; set; }
		//Minimum education requirements to obtain credential  (AudienceLevelType)
		public string Education { get; set; }
		//Minimum experience requirements to obtain credential
		//map to conditionProfile.Experience
		public string MinimumExperience { get; set; }
		//Minimum training requirements to obtain credential
		public string Training { get; set; }

		//Prerequisite-Name
		//Any credentials that must be held as a prerequisite to obtain credential
		//probably would be an FK to an existing credential?
		public List<int> PreRequisiteCredential { get; set; } = new List<int>();

		//Indicates whether agency membership is a requirement to obtain credential
		public List<string> Membership_Required { get; set; } = new List<string>();

		/* includes data that could be split out:
			Minimum education requirements to obtain credential  (AudienceLevelType)
			Minimum experience requirements to obtain credential
			
			

		*/
		//CTDL equivalence
		public List<string> AudienceLevelType { get; set; } = new List<string>();

		/* Map to ConditionProfile.Conditions
		 * 
Credential Prerequisite
Experience: 2 years
Education: High School Diploma/GED
Training
Membership
Other
Fee		 
			 */
		public List<string> EligibilityRequirements { get; set; } = new List<string>();



	}
}
