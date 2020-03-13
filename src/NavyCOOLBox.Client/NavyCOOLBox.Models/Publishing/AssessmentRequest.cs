using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace NavyCOOLBox.Models.Publishing
{
	public class AssessmentRequest : BaseRequest
	{
		public AssessmentRequest()
		{
			Assessment = new Assessment();
		}

		public Assessment Assessment { get; set; }

	}
	//public class Assessment
	//{
	//	public Assessment()
	//	{
	//		Subject = new List<string>();
	//		Keyword = new List<string>();

	//		AssessmentMethodType = new List<string>();
	//		AudienceType = new List<string>();
	//		//CodedNotation = new List<string>();
	//		AssessmentUseType = new List<string>();

	//		AvailabilityListing = new List<string>();
	//		AvailableOnlineAt = new List<string>();

	//		DeliveryType = new List<string>();

	//		//EstimatedCost = new List<CostProfile>();
	//		//EstimatedDuration = new List<DurationProfile>();
	//		//
	//		ScoringMethodType = new List<string>();


	//		Corequisite = new List<ConditionProfile>();
	//		Recommends = new List<ConditionProfile>();
	//		Requires = new List<ConditionProfile>();
	//		EntryCondition = new List<ConditionProfile>();

	//		AvailableAt = new List<Address>();

	//		ExternalResearch = new List<string>();
	//		InLanguage = new List<string>();
	//		CommonConditions = new List<string>();
	//		CommonCosts = new List<string>();

	//	}



	//	#region *** Required Properties ***
	//	public string Name { get; set; }

	//	/// <summary>
	//	/// Assessment Description 
	//	/// Required
	//	/// </summary>
	//	public string Description { get; set; }


	//	public string Ctid { get; set; }
	//	public string SubjectWebpage { get; set; } //URL

	//	#region at least one of

	//	/// <summary>
	//	/// Organization that owns this resource
	//	/// </summary>
	//	public List<OrganizationReference> OwnedBy { get; set; } = new List<OrganizationReference>();
	//	//OR
	//	/// <summary>
	//	/// Organization(s) that offer this resource
	//	/// </summary>
	//	public List<OrganizationReference> OfferedBy { get; set; }
	//	#endregion

	//	#region at least one of the following
	//	public List<string> AvailableOnlineAt { get; set; } //URL
	//	public List<string> AvailabilityListing { get; set; } //URL
	//	public List<Address> AvailableAt { get; set; }
	//	#endregion

	//	#endregion

	//	#region *** Required if available Properties ***
	//	//public List<CredentialAlignmentObject> Assesses { get; set; }

	//	public List<string> AssessmentMethodType { get; set; }
	//	public List<string> DeliveryType { get; set; }
	//	public string DeliveryTypeDescription { get; set; }

	//	#endregion

	//	#region *** Recommended Properties ***
	//	public string DateEffective { get; set; }

	//	//List of language codes. ex: en, es
	//	public List<string> InLanguage { get; set; }

	//	//public List<DurationProfile> EstimatedDuration { get; set; }
	//	public List<ConditionProfile> Requires { get; set; }


	//	#endregion

	//	//
	//	public List<string> Keyword { get; set; }

	//	public List<string> Subject { get; set; }


	//	public string AssessmentExample { get; set; }
	//	public List<string> AssessmentUseType { get; set; }



	//	public string CodedNotation { get; set; }

	//	public string AssessmentExampleDescription { get; set; }

	//	public string AssessmentOutput { get; set; }


	//	public string ProcessStandards { get; set; }

	//	public string ProcessStandardsDescription { get; set; }

	//	//
	//	public List<FrameworkItem> OccupationType { get; set; }
	//	public List<string> AlternativeOccupationType { get; set; } = new List<string>();
	//	//public LanguageMapList AlternativeOccupationType_Map { get; set; } = new LanguageMapList();

	//	public List<FrameworkItem> IndustryType { get; set; }
	//	public List<string> AlternativeIndustryType { get; set; } = new List<string>();

	//	public List<FrameworkItem> InstructionalProgramType { get; set; } = new List<FrameworkItem>();
	//	public List<string> AlternativeInstructionalProgramType { get; set; } = new List<string>();

	//	//
	//	public bool? IsProctored { get; set; }
	//	public bool? HasGroupEvaluation { get; set; }
	//	public bool? HasGroupParticipation { get; set; }


	//	//external classes

	//	public List<string> AudienceType { get; set; }
	//	public List<string> AudienceLevelType { get; set; } = new List<string>();
	//	public string ScoringMethodDescription { get; set; }
	//	public string ScoringMethodExample { get; set; }
	//	public string ScoringMethodExampleDescription { get; set; }
	//	public List<string> ScoringMethodType { get; set; }


	//	#region -- Quality Assurance BY --
	//	public List<OrganizationReference> AccreditedBy { get; set; }
	//	public List<OrganizationReference> ApprovedBy { get; set; }

	//	public List<OrganizationReference> RecognizedBy { get; set; }
	//	public List<OrganizationReference> RegulatedBy { get; set; }
	//	#endregion

	//	//conditions
	//	public List<ConditionProfile> Corequisite { get; set; }
	//	public List<ConditionProfile> Recommends { get; set; }
	//	public List<ConditionProfile> EntryCondition { get; set; }

	//	public List<string> ExternalResearch { get; set; }


	//	//required competencies are handled with condition profiles
	//	//public List<CredentialAlignmentObject> RequiresCompetency { get; set; }


	//	public List<string> CommonCosts { get; set; }
	//	public List<string> CommonConditions { get; set; }

	//	public List<FinancialAssistanceProfile> FinancialAssistance { get; set; } = new List<FinancialAssistanceProfile>();

	//}
}
