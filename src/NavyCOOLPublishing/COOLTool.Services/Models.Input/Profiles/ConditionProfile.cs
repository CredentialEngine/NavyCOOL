using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOLTool.Services.Models.Input
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
		/// AlternativeCondition - list of options, where there are two or more mutually exclusive paths to the credential
		/// pipe delimited string of “name|description” Example:
		/// “Option 2| Candidate must provide a statement of graduation”  or “|Must be 21” – Note no name given
		/// 
		/// </summary>
		public List<string> AlternativeCondition { get; set; } = new List<string>();
		//public List<ConditionProfile> AlternativeCondition { get; set; }
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
		/// NOTE: will default to the owning Agent
		/// </summary>
		public List<OrganizationReference> AssertedBy { get; set; } = new List<OrganizationReference>();

		public string Experience { get; set; }
		public int MinimumAge { get; set; }
		public decimal YearsOfExperience { get; set; }
		public decimal Weight { get; set; }


		//Credit Information
		//
		//public QuantitativeValue CreditValue { get; set; } = new QuantitativeValue();
		//

		/// <summary>
		/// Only one credit unit type is allowed for input
		/// </summary>
		public string CreditUnitType { get; set; }
		public string CreditUnitTypeDescription { get; set; }
		public decimal CreditUnitValue { get; set; }

		//external classes =====================================

		public List<EntityReference> TargetAssessment { get; set; } = new List<EntityReference>();
		public List<EntityReference> TargetCredential { get; set; } = new List<EntityReference>();
		public List<EntityReference> TargetLearningOpportunity { get; set; } = new List<EntityReference>();


	}
}
