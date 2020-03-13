using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.JsonV2;

namespace RA.Models.Navy.Json
{
	public class BaseTask 
	{

		/// <summary>
		/// CTID
		/// Globally unique Credential Transparency Identifier (CTID) by which the creator, owner or provider of a resource recognizes it in transactions with the external environment (e.g., in verifiable claims involving the resource).
		/// </summary>
		[JsonProperty( "ceterms:ctid" )]
		public string CTID { get; set; }

		[JsonProperty( "@id" )]
		public string CtdlId { get; set; }

		[JsonProperty( PropertyName = "ceasn:inLanguage" )]
		public List<string> InLanguage { get; set; } = new List<string>();


		/// <summary>
		/// CodedNotation
		/// An alphanumeric notation or ID code as defined by the promulgating body to identify this resource.
		/// </summary>
		[JsonProperty( "ceasn:codedNotation" )]
		public string CodedNotation { get; set; }

		/// <summary>
		/// Comment
		/// Supplemental text provided by the promulgating body that clarifies the nature, scope or use of this competency.
		/// </summary>
		[JsonProperty( "ceasn:comment" )]
		public LanguageMapList Comment { get; set; }


		/// <summary>
		/// HasChild
		/// The referenced resource is lower in some arbitrary hierarchy than this resource.
		/// URI to a child task 
		/// </summary>
		[JsonProperty( "ceasn:hasChild" )]
		public List<string> HasChild { get; set; }

		/// <summary>
		/// IsChildOf
		/// The referenced resource is higher in some arbitrary hierarchy than this resource.
		/// URI to a parent task
		/// </summary>
		[JsonProperty( "ceasn:isChildOf" )]
		public List<string> IsChildOf { get; set; } = new List<string>();


		/// <summary>
		/// IsPartOf
		/// Competency framework that this competency is a part of.
		/// URI to a task framework
		/// </summary>
		[JsonProperty( "ceasn:isPartOf" )]
		public string IsPartOf { get; set; }


		/// <summary>
		/// IsTopChildOf
		/// Indicates that this competency is at the top of some arbitrary hierarchy.
		/// URI to the parent framework
		/// </summary>
		[JsonProperty( "ceasn:isTopChildOf" )]
		public string IsTopChildOf { get; set; }


		/// <summary>
		/// List ID
		/// An alphanumeric string indicating the relative position of a resource in an ordered list of resources such as "A", "B", or "a", "b", or "I", "II", or "1", "2".
		/// </summary>
		[JsonProperty( "ceasn:listID" )]
		public string ListID { get; set; }

		/// <summary>
		/// TaskLabel
		/// Short identifying phrase or name applied to a task by the creator of that task.
		/// </summary>
		[JsonProperty( "navy:taskLabel" )]
		public LanguageMap TaskLabel{ get; set; }

		/// <summary>
		/// TaskText
		/// Text describing the task to be performed.
		/// </summary>
		[JsonProperty( "navy:taskText" )]
		public LanguageMap TaskText { get; set; }

	}

	public class BaseTaskPlain : BaseTask
	{

		/// <summary>
		/// CTID
		/// Globally unique Credential Transparency Identifier (CTID) by which the creator, owner or provider of a resource recognizes it in transactions with the external environment (e.g., in verifiable claims involving the resource).
		/// </summary>
		//[JsonProperty( "ceterms:ctid" )]
		//public string CTID { get; set; }

		//[JsonProperty( "@id" )]
		//public string CtdlId { get; set; }

		//[JsonProperty( PropertyName = "ceasn:inLanguage" )]
		//public List<string> InLanguage { get; set; } = new List<string>();


		/// <summary>
		/// CodedNotation
		/// An alphanumeric notation or ID code as defined by the promulgating body to identify this resource.
		/// </summary>
		//[JsonProperty( "ceasn:codedNotation" )]
		//public string CodedNotation { get; set; }

		/// <summary>
		/// Comment
		/// Supplemental text provided by the promulgating body that clarifies the nature, scope or use of this competency.
		/// </summary>
		[JsonProperty( "ceasn:comment" )]
		public new List<string> Comment { get; set; }


		/// <summary>
		/// HasChild
		/// The referenced resource is lower in some arbitrary hierarchy than this resource.
		/// URI to a child task 
		/// </summary>
		//[JsonProperty( "ceasn:hasChild" )]
		//public List<string> HasChild { get; set; }

		/// <summary>
		/// IsChildOf
		/// The referenced resource is higher in some arbitrary hierarchy than this resource.
		/// URI to a parent task
		/// </summary>
		//[JsonProperty( "ceasn:isChildOf" )]
		//public List<string> IsChildOf { get; set; } = new List<string>();


		/// <summary>
		/// IsPartOf
		/// Competency framework that this competency is a part of.
		/// URI to a task framework
		/// </summary>
		//[JsonProperty( "ceasn:isPartOf" )]
		//public string IsPartOf { get; set; }

		//[JsonProperty( "ceasn:listID" )]
		//public string ListID { get; set; }

		/// <summary>
		/// IsTopChildOf
		/// Indicates that this competency is at the top of some arbitrary hierarchy.
		/// URI to the parent framework
		/// </summary>
		//[JsonProperty( "ceasn:isTopChildOf" )]
		//public string IsTopChildOf { get; set; }


		/// <summary>
		/// TaskLabel
		/// Text describing the task to be performed.
		/// </summary>
		[JsonProperty( "navy:taskLabel" )]
		public new string TaskLabel{ get; set; }

		/// <summary>
		/// TaskText
		/// Text describing the task to be performed.
		/// </summary>
		[JsonProperty( "navy:taskText" )]
		public new string TaskText { get; set; }

	}
}
