using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Models
{
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
