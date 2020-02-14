using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.JsonV2;

namespace RA.Models.Navy.Json
{


	public class OccupationalTask : BaseTask
	{
		public OccupationalTask()
		{
			Type = "navy:OccupationalTask";
		}
		/// <summary>
		///  type
		/// </summary>
		[JsonProperty( "@type" )]
		public string Type { get; set; }

		/// <summary>
		/// AbilityEmbodied
		/// Enduring attributes of the individual that influence performance are embodied either directly or indirectly in this resource.
		/// URI to a competency
		/// </summary>
		[JsonProperty( "ceasn:abilityEmbodied" )]
		public List<string> AbilityEmbodied { get; set; }

		/// <summary>
		/// CodedNotation
		/// An alphanumeric notation or ID code as defined by the promulgating body to identify this resource.
		/// </summary>
		//[JsonProperty( "ceasn:codedNotation" )]
		//public string CodedNotation { get; set; }


		/// <summary>
		/// HasFunctionalGroup
		/// Functional Group referenced by this resource.
		/// URI to a concept
		/// </summary>
		[JsonProperty( "navy:hasFunctionalGroup" )]
		public List<string> HasFunctionalGroup { get; set; }

		/// <summary>
		/// HasPayGradeType
		/// Type of pay grade; select from an existing enumeration of such types.
		/// URI to a concept
		/// </summary>
		[JsonProperty( "navy:hasPayGradeType" )]
		public List<string> HasPayGradeType { get; set; }

		/// <summary>
		/// HasTaskFlagType - Single
		/// Type of task flag; select from an existing enumeration of such types.
		/// URI to a concept
		/// </summary>
		[JsonProperty( "navy:hasTaskFlagType" )]
		public string HasTaskFlagType { get; set; }

		/// <summary>
		/// HasWorkActivity
		/// Work Activity referenced by this resource.
		/// URI to a concept
		/// </summary>
		[JsonProperty( "navy:hasWorkActivity" )]
		public List<string> HasWorkActivity { get; set; }

		/// <summary>
		/// HasWorkRole
		/// Work role referenced by this resource.
		/// URI to a workRole
		/// </summary>
		[JsonProperty( "navy:hasWorkRole" )]
		public List<string> HasWorkRole { get; set; }


		/// <summary>
		/// IsCore
		/// Indicates whether this resource is considered core.
		/// </summary>
		[JsonProperty( "navy:isCore" )]
		public bool IsCore { get; set; }
		

		/// <summary>
		/// KnowledgeEmbodied
		/// Body of information embodied either directly or indirectly in this resource.
		/// URI to a competency
		/// </summary>
		[JsonProperty( "ceasn:knowledgeEmbodied" )]
		public List<string> KnowledgeEmbodied { get; set; }

		/// <summary>
		/// SkillEmbodied
		/// Ability to apply knowledge and use know-how to complete tasks and solve problems including types or categories of developed proficiency or dexterity in mental operations and physical processes is embodied either directly or indirectly in this resource.
		/// URI to a competency
		/// </summary>
		[JsonProperty( "ceasn:skillEmbodied" )]
		public List<string> SkillEmbodied { get; set; }


		/// <summary>
		/// Version
		/// Version of this resource.
		/// </summary>
		[JsonProperty( "schema:version" )]
		public string Version { get; set; }
	}

	public class OccupationalTaskPlain : BaseTaskPlain
	{
		public OccupationalTaskPlain()
		{
			Type = "navy:OccupationalTask";
		}
		/// <summary>
		///  type
		/// </summary>
		[JsonProperty( "@type" )]
		public string Type { get; set; }

		/// <summary>
		/// AbilityEmbodied
		/// Enduring attributes of the individual that influence performance are embodied either directly or indirectly in this resource.
		/// URI to a competency
		/// </summary>
		[JsonProperty( "ceasn:abilityEmbodied" )]
		public List<string> AbilityEmbodied { get; set; }


		/// <summary>
		/// HasFunctionalGroup
		/// Functional Group referenced by this resource.
		/// URI to a concept
		/// </summary>
		[JsonProperty( "navy:hasFunctionalGroup" )]
		public List<string> HasFunctionalGroup { get; set; }

		/// <summary>
		/// HasPayGradeType
		/// Type of pay grade; select from an existing enumeration of such types.
		/// URI to a concept
		/// </summary>
		[JsonProperty( "navy:hasPayGradeType" )]
		public List<string> HasPayGradeType { get; set; }

		/// <summary>
		/// HasTaskFlagType 
		/// Type of task flag; select from an existing enumeration of such types.
		/// URI to a concept
		/// </summary>
		[JsonProperty( "navy:hasTaskFlagType" )]
		public List<string> HasTaskFlagType { get; set; }

		/// <summary>
		/// HasWorkActivity
		/// Work Activity referenced by this resource.
		/// URI to a concept
		/// </summary>
		[JsonProperty( "navy:hasWorkActivity" )]
		public List<string> HasWorkActivity { get; set; }

		/// <summary>
		/// HasWorkRole
		/// Work role referenced by this resource.
		/// URI to a workRole
		/// </summary>
		[JsonProperty( "navy:hasWorkRole" )]
		public List<string> HasWorkRole { get; set; }


		/// <summary>
		/// IsCore
		/// Indicates whether this resource is considered core.
		/// </summary>
		[JsonProperty( "navy:isCore" )]
		public bool IsCore { get; set; }


		/// <summary>
		/// KnowledgeEmbodied
		/// Body of information embodied either directly or indirectly in this resource.
		/// URI to a competency
		/// </summary>
		[JsonProperty( "ceasn:knowledgeEmbodied" )]
		public List<string> KnowledgeEmbodied { get; set; }

		/// <summary>
		/// SkillEmbodied
		/// Ability to apply knowledge and use know-how to complete tasks and solve problems including types or categories of developed proficiency or dexterity in mental operations and physical processes is embodied either directly or indirectly in this resource.
		/// URI to a competency
		/// </summary>
		[JsonProperty( "ceasn:skillEmbodied" )]
		public List<string> SkillEmbodied { get; set; }


		/// <summary>
		/// Version
		/// Version of this resource.
		/// </summary>
		[JsonProperty( "schema:version" )]
		public string Version { get; set; }
	}
}


