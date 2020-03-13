using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.Input;

namespace RA.Models.Navy.Input
{
	public class TrainingTaskRequest : BaseRequest
	{
		public TrainingTaskRequest()
		{
			TrainingTask = new TrainingTask();
		}

		public TrainingTask TrainingTask { get; set; }

	}
	public class TrainingTask : BaseTask
	{
		public TrainingTask()
		{
		}


		/// <summary>
		/// AbilityEmbodied
		/// Enduring attributes of the individual that influence performance are embodied either directly or indirectly in this resource.
		/// URI/CTID to a Competency
		/// </summary>
		public List<string> AbilityEmbodied { get; set; } //ceasn:abilityEmbodied

		/// <summary>
		/// AffectiveLevel
		/// Affective level for this item.
		/// URI/CTID to a Concept
		/// </summary>
		//public string AffectiveLevel { get; set; }

		/// <summary>
		/// AudienceType
		/// Audience for this item.
		/// URI/CTID to a Concept
		/// </summary>
		public List<string> AudienceType { get; set; }

		/// <summary>
		/// CognitiveLevelType
		/// Cognitive level for this item.
		/// URI/CTID to a Concept
		/// </summary>
		public string CognitiveLevelType { get; set; }

		/// <summary>
		/// CollectiveTrainTask
		/// Indicates whether the item should be performed by an individual or collective (true?).
		/// </summary>
		public bool CollectiveTrainTask { get; set; }



		/// <summary>
		/// GroupType
		/// Type of group that is to perform this item.
		/// URI/CTID to a Concept
		/// </summary>
		//public List<string> GroupType { get; set; }


		/// <summary>
		/// HasLearningObjective
		/// Learning objective for this resource.
		/// URI/CTID to a Competency
		/// </summary>
		public List<string> HasLearningObjective { get; set; }

		/// <summary>
		/// HasLearningOpportunity
		/// Any education or training related entity or data that results from this item.
		/// s1000d:LearnPlan
		/// URI/CTID to a LearnPlan
		/// 2020-01-27 removed
		/// </summary>
		//public List<string> HasLearningOpportunity { get; set; }

		/// <summary>
		/// HasMaintenanceTask
		/// Maintenance task related to this resource.
		/// URI/CTID to a Maintenance task
		/// </summary>
		public List<string> HasMaintenanceTask { get; set; }

		/// <summary>
		/// HasOccupationalTask
		/// Occupational task related to this resource.
		/// URI/CTID to an Occupation Task
		/// </summary>
		public List<string> HasOccupationalTask { get; set; }


		/// <summary>
		/// HasSourceIdentifier
		/// A collection of identifiers related to this resource.
		/// URI/CTID to a SourceIdentifier
		/// </summary>
		public List<string> HasSourceIdentifier { get; set; }

		/// <summary>
		/// KnowledgeEmbodied
		/// Body of information embodied either directly or indirectly in this resource.
		/// URI/CTID to a Competency
		/// </summary>
		public List<string> KnowledgeEmbodied { get; set; } //??????????????

		/// <summary>
		/// PerformanceStandard
		/// Standard to which this item is to be performed.
		/// 2020-01-27 removed
		/// </summary>
		//public string PerformanceStandard { get; set; }
		//public LanguageMap PerformanceStandard_Map { get; set; }

		/// <summary>
		/// PositionType
		/// Type of position that is to perform this item.
		/// URI/CTID to a Concept
		/// 2020-01-27 removed
		/// </summary>
		//public List<string> PositionType { get; set; }

		/// <summary>
		/// PsychomotorLevelType
		/// Psychomotor level for this item.
		/// URI/CTID to a Concept
		/// </summary>
		public string PsychomotorLevelType { get; set; }

		/// <summary>
		/// SkillEmbodied
		/// Ability to apply knowledge and use know-how to complete tasks and solve problems including types or categories of developed proficiency or dexterity in mental operations and physical processes is embodied either directly or indirectly in this resource.
		/// URI/CTID to a Competency
		/// </summary>
		public List<string> SkillEmbodied { get; set; } //ceasn:skillEmbodied

		/// <summary>
		/// TaskDifficultyType
		/// Difficulty of performing this item.
		/// URI/CTID to a Concept
		/// </summary>
		public string TaskDifficultyType { get; set; }

		/// <summary>
		/// TaskFrequencyType
		/// Frequency with which this item is to be performed.
		/// URI/CTID to a Concept
		/// </summary>
		public string TaskFrequencyType { get; set; }

		/// <summary>
		/// TaskTrainingDifficulty
		/// Difficulty of training this item.
		/// URI/CTID to a Concept
		/// </summary>
		//public List<string> TaskTrainingDifficulty { get; set; }

		/// <summary>
		/// TaskImportanceType
		/// Importance of training this item.
		/// URI/CTID to a Concept
		/// </summary>
		public List<string> TaskImportanceType { get; set; }

		/// <summary>
		/// TaskTrainingLevelType
		/// Level to which this item is to be trained.
		/// URI/CTID to a Concept
		/// </summary>
		public List<string> TaskTrainingLevelType { get; set; }

		/// <summary>
		/// ToolType
		/// Type of tool used to perform this item.
		/// URI/CTID to a Concept
		/// </summary>
		public List<string> ToolType { get; set; }


	}
}


