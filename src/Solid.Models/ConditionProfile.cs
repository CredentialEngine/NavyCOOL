using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Solid.Models
{
	public class ConditionProfile
	{
		/// <summary>
		/// Name of this condition
		/// Required
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Condition description 
		/// Required
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// OptionalConditions - list of options, where there are two or more mutually exclusive paths to the credential
		/// </summary>
		public List<string> OptionalConditions { get; set; } = new List<string>();

		public string SubjectWebpage { get; set; } //URL

		public string DateEffective { get; set; }
		/// <summary>
		/// List of condtions, containing:
		/// A single condition or aspect of experience that refines the conditions under which the resource being described is applicable.
		/// </summary>
		public List<string> Condition { get; set; } = new List<string>();

		/// <summary>
		/// Aug. 2019 - changed to be list of URIs. Use SubmissionOfDescription for text values.
		/// </summary>
		public List<string> SubmissionOf { get; set; } = new List<string>();

		public string SubmissionOfDescription { get; set; }

		/// <summary>
		/// Organization that asserts this condition
		/// This should be single, but as CTDL defines as multi-value, need to handle a List
		/// </summary>
		public List<Agency> AssertedBy { get; set; } = new List<Agency>();

		public string Experience { get; set; }
		public int MinimumAge { get; set; }
		public decimal YearsOfExperience { get; set; }
		public decimal Weight { get; set; }
		//Credit Information
		//
		//public QuantitativeValue CreditValue { get; set; } = new QuantitativeValue();
		//
		public string CreditHourType { get; set; }
		public decimal CreditHourValue { get; set; }
		//public int CreditUnitTypeId { get; set; }

		/// <summary>
		/// Only one credit unit type is allowed for input
		/// </summary>
		public string CreditUnitType { get; set; }
		public string CreditUnitTypeDescription { get; set; }
		public decimal CreditUnitValue { get; set; }

		//external classes =====================================
		public List<CostProfile> EstimatedCost { get; set; } = new List<CostProfile>();
		public List<Jurisdiction> Jurisdiction { get; set; } = new List<Jurisdiction>();
		public List<Jurisdiction> ResidentOf { get; set; } = new List<Jurisdiction>();

		public List<EntityReference> TargetAssessment { get; set; }
		public List<EntityReference> TargetCredential { get; set; }
		public List<EntityReference> TargetLearningOpportunity { get; set; }
		public List<CredentialAlignmentObject> TargetCompetency { get; set; }
		public List<ConditionProfile> AlternativeCondition { get; set; }

	}
}
