using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.JsonV2;

namespace RA.Models.Navy.Json
{
	public class TrainingTask : BaseTask
	{
		public TrainingTask()
		{
			Type = "navy:TrainingTask";
		}
		/// <summary>
		///  type
		/// </summary>
		[JsonProperty( "@type" )]
		public string Type { get; set; }

		/// <summary>
		/// AbilityEmbodied
		/// Enduring attributes of the individual that influence performance are embodied either directly or indirectly in this resource.
		/// URI to a Competency
		/// </summary>
		[JsonProperty( "ceasn:abilityEmbodied" )]
		public List<string> AbilityEmbodied { get; set; }

		/// <summary>
		/// AffectiveLevel
		/// Affective level for this item.
		/// URI to a Concept
		/// 2020-01-27 removed
		/// </summary>
		//[JsonProperty( "navy:affectiveLevel" )]
		//public string AffectiveLevel { get; set; }

		/// <summary>
		/// AudienceType
		/// Audience for this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:audienceType" )]
		public List<string> AudienceType { get; set; }

		/// <summary>
		/// CognitiveLevelType
		/// Cognitive level for this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:cognitiveLevelType" )]
		public string CognitiveLevelType { get; set; }

		/// <summary>
		/// CollectiveTrainTask
		/// Indicates whether the item should be performed by an individual or collective (true?).
		/// </summary>
		[JsonProperty( "navy:collectiveTrainTask" )]
		public bool CollectiveTrainTask { get; set; }



		/// <summary>
		/// GroupType
		/// Type of group that is to perform this item.
		/// 2020-01-27 removed
		/// </summary>
		//[JsonProperty( "navy:groupType" )]
		//public List<string> GroupType { get; set; }



		/// <summary>
		/// HasLearningObjective
		/// Learning objective for this resource.
		/// URI to a Competency
		/// </summary>
		[JsonProperty( "navy:hasLearningObjective" )]
		public List<string> HasLearningObjective { get; set; }

		/// <summary>
		/// HasLearningOpportunity
		/// Any education or training related entity or data that results from this item.
		/// s1000d:LearnPlan
		/// URI to a LearnPlan
		/// 2020-01-27 removed
		/// </summary>
		//[JsonProperty( "navy:hasLearningOpportunity" )]
		//public List<string> HasLearningOpportunity { get; set; }

		/// <summary>
		/// HasMaintenanceTask
		/// Maintenance task related to this resource.
		/// URI to a Maintenance task
		/// </summary>
		[JsonProperty( "navy:hasMaintenanceTask" )]
		public List<string> HasMaintenanceTask { get; set; }

		/// <summary>
		/// HasOccupationalTask
		/// Occupational task related to this resource.
		/// URI to an Occupation Task
		/// </summary>
		[JsonProperty( "navy:hasOccupationalTask" )]
		public List<string> HasOccupationalTask { get; set; }

		/// <summary>
		/// HasSourceIdentifier
		/// A collection of identifiers related to this resource.
		/// URI to a SourceIdentifier
		/// </summary>
		[JsonProperty( "navy:hasSourceIdentifier" )]
		public List<string> HasSourceIdentifier { get; set; }



		/// <summary>
		/// KnowledgeEmbodied
		/// Body of information embodied either directly or indirectly in this resource.
		/// URI to a Competency
		/// </summary>
		[JsonProperty( "ceasn:knowledgeEmbodied" )]
		public List<string> KnowledgeEmbodied { get; set; }

		/// <summary>
		/// PerformanceStandard
		/// Standard to which this item is to be performed.
		/// 2020-01-27 removed
		/// </summary>
		//[JsonProperty( "navy:performanceStandard" )]
		//public LanguageMap PerformanceStandard { get; set; }

		/// <summary>
		/// PositionType
		/// Type of position that is to perform this item.
		/// URI to a Concept
		/// 2020-01-27 removed
		/// </summary>
		//[JsonProperty( "navy:positionType" )]
		//public List<string> PositionType { get; set; }

		/// <summary>
		/// PsychomotorLevelType
		/// Psychomotor level for this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:psychomotorLevelType" )]
		public string PsychomotorLevelType { get; set; }

		/// <summary>
		/// SkillEmbodied
		/// Ability to apply knowledge and use know-how to complete tasks and solve problems including types or categories of developed proficiency or dexterity in mental operations and physical processes is embodied either directly or indirectly in this resource.
		/// URI to a Competency
		/// </summary>
		[JsonProperty( "ceasn:skillEmbodied" )]
		public List<string> SkillEmbodied { get; set; }

		/// <summary>
		/// TaskDifficultyType
		/// Difficulty of performing this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:taskDifficultyType" )]
		public string TaskDifficultyType { get; set; }

		/// <summary>
		/// TaskFrequencyType
		/// Frequency with which this item is to be performed.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:taskFrequencyType" )]
		public string TaskFrequencyType { get; set; }

		/// <summary>
		/// TaskTrainingDifficulty
		/// Difficulty of training this item.
		/// URI to a Concept
		/// 2020-01-27 removed
		/// </summary>
		//[JsonProperty( "navy:taskTrainingDifficulty" )]
		//public List<string> TaskTrainingDifficulty { get; set; }

		/// <summary>
		/// TaskImportanceType
		/// Importance of training this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:taskImportanceType" )]
		public List<string> TaskImportanceType { get; set; }

		/// <summary>
		/// TaskTrainingLevelType
		/// Level to which this item is to be trained.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:taskTrainingLevelType" )]
		public List<string> TaskTrainingLevelType { get; set; }

		/// <summary>
		/// ToolType
		/// Type of tool used to perform this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:toolType" )]
		public List<string> ToolType { get; set; }


	}

	public class TrainingTaskPlain : BaseTaskPlain
	{
		public TrainingTaskPlain()
		{
			Type = "navy:TrainingTask";
		}
		/// <summary>
		///  type
		/// </summary>
		[JsonProperty( "@type" )]
		public string Type { get; set; }

		/// <summary>
		/// AbilityEmbodied
		/// Enduring attributes of the individual that influence performance are embodied either directly or indirectly in this resource.
		/// URI to a Competency
		/// </summary>
		[JsonProperty( "ceasn:abilityEmbodied" )]
		public List<string> AbilityEmbodied { get; set; }

		/// <summary>
		/// AffectiveLevel
		/// Affective level for this item.
		/// URI to a Concept
		/// </summary>
		//[JsonProperty( "navy:affectiveLevel" )]
		//public string AffectiveLevel { get; set; }

		/// <summary>
		/// AudienceType
		/// Audience for this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:audienceType" )]
		public List<string> AudienceType { get; set; }

		/// <summary>
		/// CognitiveLevelType
		/// Cognitive level for this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:cognitiveLevelType" )]
		public string CognitiveLevelType { get; set; }

		/// <summary>
		/// CollectiveTrainTask
		/// Indicates whether the item should be performed by an individual or collective (true?).
		/// </summary>
		[JsonProperty( "navy:collectiveTrainTask" )]
		public bool CollectiveTrainTask { get; set; }



		/// <summary>
		/// GroupType
		/// Type of group that is to perform this item.
		/// URI to a Concept
		/// 2020-01-27 removed
		/// </summary>
		//[JsonProperty( "navy:groupType" )]
		//public List<string> GroupType { get; set; }


		/// <summary>
		/// HasLearningObjective
		/// Learning objective for this resource.
		/// URI to a Competency
		/// </summary>
		[JsonProperty( "navy:hasLearningObjective" )]
		public List<string> HasLearningObjective { get; set; }

		/// <summary>
		/// HasLearningOpportunity
		/// Any education or training related entity or data that results from this item.
		/// s1000d:LearnPlan
		/// URI to a LearnPlan
		/// 2020-01-27 removed
		/// </summary>
		//[JsonProperty( "navy:hasLearningOpportunity" )]
		//public List<string> HasLearningOpportunity { get; set; }

		/// <summary>
		/// HasMaintenanceTask
		/// Maintenance task related to this resource.
		/// URI to a Maintenance task
		/// </summary>
		[JsonProperty( "navy:hasMaintenanceTask" )]
		public List<string> HasMaintenanceTask { get; set; }

		/// <summary>
		/// HasOccupationalTask
		/// Occupational task related to this resource.
		/// URI to an Occupation Task
		/// </summary>
		[JsonProperty( "navy:hasOccupationalTask" )]
		public List<string> HasOccupationalTask { get; set; }

		/// <summary>
		/// HasSourceIdentifier
		/// A collection of identifiers related to this resource.
		/// URI to a SourceIdentifier
		/// </summary>
		[JsonProperty( "navy:hasSourceIdentifier" )]
		public List<string> HasSourceIdentifier { get; set; }



		/// <summary>
		/// KnowledgeEmbodied
		/// Body of information embodied either directly or indirectly in this resource.
		/// URI to a Competency
		/// </summary>
		[JsonProperty( "ceasn:knowledgeEmbodied" )]
		public List<string> KnowledgeEmbodied { get; set; }

		/// <summary>
		/// PerformanceStandard
		/// Standard to which this item is to be performed.
		/// 2020-01-27 removed
		/// </summary>
		//[JsonProperty( "navy:performanceStandard" )]
		//public string PerformanceStandard { get; set; }

		/// <summary>
		/// PositionType
		/// Type of position that is to perform this item.
		/// URI to a Concept
		/// 2020-01-27 removed
		/// </summary>
		//[JsonProperty( "navy:positionType" )]
		//public List<string> PositionType { get; set; }

		/// <summary>
		/// PsychomotorLevelType
		/// Psychomotor level for this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:psychomotorLevelType" )]
		public string PsychomotorLevelType { get; set; }

		/// <summary>
		/// SkillEmbodied
		/// Ability to apply knowledge and use know-how to complete tasks and solve problems including types or categories of developed proficiency or dexterity in mental operations and physical processes is embodied either directly or indirectly in this resource.
		/// URI to a Competency
		/// </summary>
		[JsonProperty( "ceasn:skillEmbodied" )]
		public List<string> SkillEmbodied { get; set; }

		/// <summary>
		/// TaskDifficultyType
		/// Difficulty of performing this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:taskDifficultyType" )]
		public string TaskDifficultyType { get; set; }

		/// <summary>
		/// TaskFrequencyType
		/// Frequency with which this item is to be performed.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:taskFrequencyType" )]
		public string TaskFrequencyType { get; set; }

		/// <summary>
		/// TaskTrainingDifficulty
		/// Difficulty of training this item.
		/// URI to a Concept
		/// 2020-01-27 removed
		/// </summary>
		//[JsonProperty( "navy:taskTrainingDifficulty" )]
		//public List<string> TaskTrainingDifficulty { get; set; }

		/// <summary>
		/// TaskImportanceType
		/// Importance of training this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:taskImportanceType" )]
		public List<string> TaskImportanceType { get; set; }

		/// <summary>
		/// TaskTrainingLevelType
		/// Level to which this item is to be trained.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:taskTrainingLevelType" )]
		public List<string> TaskTrainingLevelType { get; set; }

		/// <summary>
		/// ToolType
		/// Type of tool used to perform this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:toolType" )]
		public List<string> ToolType { get; set; }


	}


}


