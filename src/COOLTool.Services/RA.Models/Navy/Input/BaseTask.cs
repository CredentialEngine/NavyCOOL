using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
//using RA.Models.JsonV2;
using RA.Models.Input;
namespace RA.Models.Navy.Input
{
	public class BaseTask 
	{

		/// <summary>
		/// CTID
		/// Globally unique Credential Transparency Identifier (CTID) by which the creator, owner or provider of a resource recognizes it in transactions with the external environment (e.g., in verifiable claims involving the resource).
		/// </summary>
		public string CTID { get; set; }

		public List<string> InLanguage { get; set; } = new List<string>();


		/// <summary>
		/// CodedNotation
		/// An alphanumeric notation or ID code as defined by the promulgating body to identify this resource.
		/// </summary>
		public string CodedNotation { get; set; }

		/// <summary>
		/// Comment
		/// Supplemental text provided by the promulgating body that clarifies the nature, scope or use of this competency.
		/// </summary>
		public List<string> Comment { get; set; } = new List<string>();
		public LanguageMapList Comment_Map { get; set; } = new LanguageMapList();


		/// <summary>
		/// HasChild
		/// The referenced resource is lower in some arbitrary hierarchy than this resource.
		/// URI to a child task 
		/// </summary>
		public List<string> HasChild { get; set; } = new List<string>();

		/// <summary>
		/// IsChildOf
		/// The referenced resource is higher in some arbitrary hierarchy than this resource.
		/// URI to a parent task
		/// </summary>
		public List<string> IsChildOf { get; set; } = new List<string>();


		/// <summary>
		/// IsPartOf
		/// Competency framework that this competency is a part of.
		/// URI to a task framework
		/// </summary>
		public string IsPartOf { get; set; }


		/// <summary>
		/// IsTopChildOf
		/// Indicates that this competency is at the top of some arbitrary hierarchy.
		/// URI to the parent framework
		/// </summary>
		public string IsTopChildOf { get; set; }

		/// <summary>
		/// List ID
		/// An alphanumeric string indicating the relative position of a resource in an ordered list of resources such as "A", "B", or "a", "b", or "I", "II", or "1", "2".
		/// </summary>
		public string ListID { get; set; }

		/// <summary>
		/// TaskText
		/// Text describing the task to be performed.
		/// </summary>
		public string TaskLabel { get; set; }
		public LanguageMap TaskLabel_Map { get; set; }

		/// <summary>
		/// TaskText
		/// Text describing the task to be performed.
		/// </summary>
		public string TaskText { get; set; }
		public LanguageMap TaskText_Map { get; set; }

	}
}
