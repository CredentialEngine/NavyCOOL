using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Models
{
	public class AssessmentDTO
	{
		public AssessmentDTO()
		{

		}

		#region required properties
		/// <summary>
		/// name
		/// </summary>
		public string Name { get; set; }

		//minimum length of 25 characters
		public string Description { get; set; }

		public string CTID { get; set; }
		public string SubjectWebpage { get; set; }

		public string OwnedByOrganizationCTID { get; set; }
		//at least one of OwnedBy/OfferedBy
		public List<OrganizationReference> OwnedBy { get; set; } = new List<OrganizationReference>();
		public List<OrganizationReference> OferredBy { get; set; } = new List<OrganizationReference>();

		//at least one ofAddresses, AvailabilityListing, or AvailableOnlineAt
		public List<Address> Addresses { get; set; } = new List<Address>();
		public string AvailabilityListing { get; set; }
		public string AvailableOnlineAt { get; set; }
		
		#endregion

		//
		//possibly
		public List<CostProfile> EstimatedCost { get; set; }
		public List<FinancialAssistanceProfile> FinancialAssistance { get; set; } = new List<FinancialAssistanceProfile>();

		public List<DurationProfile> EstimatedDuration { get; set; } = new List<DurationProfile>();

		//
		#region Less likely - remove/comment if not likely to use at this time
		public string AssessmentExample { get; set; }
		public string AssessmentExampleDescription { get; set; }
		
		public Enumeration AudienceLevelType { get; set; } = new Enumeration();
		public Enumeration AudienceType { get; set; } = new Enumeration();

		public Enumeration AssessmentUseType { get; set; } = new Enumeration();
		public string CodedNotation { get; set; }
		public Enumeration DeliveryType { get; set; } = new Enumeration();
		public string DeliveryTypeDescription { get; set; }
		public string ExternalResearch { get; set; }
		public List<string> Keywords { get; set; } = new List<string>();
		public List<string> Subjects { get; set; } = new List<string>();

		#endregion
	}
}
