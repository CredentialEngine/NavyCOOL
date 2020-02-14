using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.Input;

namespace RA.Models.Navy.Input
{

	public class OccupationalTaskRequest : BaseRequest
	{
		public OccupationalTaskRequest()
		{
			OccupationalTask = new OccupationalTask();
		}

		public OccupationalTask OccupationalTask { get; set; }

	}

	public class OccupationalTask : BaseTask
	{
		public OccupationalTask()
		{
		}

		/// <summary>
		/// AbilityEmbodied
		/// Enduring attributes of the individual that influence performance are embodied either directly or indirectly in this resource.
		/// URI to a competency
		/// </summary>
		public List<string> AbilityEmbodied { get; set; }



		/// <summary>
		/// HasFunctionalGroup
		/// Functional Group referenced by this resource.
		/// URI to a concept
		/// </summary>
		public List<string> HasFunctionalGroup { get; set; }

		/// <summary>
		/// HasPayGradeType
		/// Type of pay grade; select from an existing enumeration of such types.
		/// URI to a concept
		/// </summary>
		public List<string> HasPayGradeType { get; set; }

		/// <summary>
		/// HasTaskFlagType - Single
		/// Type of task flag; select from an existing enumeration of such types.
		/// URI to a concept
		/// </summary>
		public string HasTaskFlagType { get; set; }

		/// <summary>
		/// HasWorkActivity
		/// Work Activity referenced by this resource.
		/// URI to a concept
		/// </summary>
		public List<string> HasWorkActivity { get; set; }

		/// <summary>
		/// HasWorkRole
		/// Work role referenced by this resource.
		/// </summary>
		public List<string> HasWorkRole { get; set; }


		/// <summary>
		/// IsCore
		/// Indicates whether this resource is considered core.
		/// </summary>
		public bool IsCore { get; set; }
		

		/// <summary>
		/// KnowledgeEmbodied
		/// Body of information embodied either directly or indirectly in this resource.
		/// URI to a competency
		/// </summary>
		public List<string> KnowledgeEmbodied { get; set; }

		/// <summary>
		/// SkillEmbodied
		/// Ability to apply knowledge and use know-how to complete tasks and solve problems including types or categories of developed proficiency or dexterity in mental operations and physical processes is embodied either directly or indirectly in this resource.
		/// URI to a competency
		/// </summary>
		public List<string> SkillEmbodied { get; set; }


		/// <summary>
		/// Version
		/// Version of this resource.
		/// </summary>
		public string Version { get; set; }


	}

}


