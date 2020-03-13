using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.Input;

namespace RA.Models.Navy.Input
{
	public class WorkRoleRequest : BaseRequest
	{
		public WorkRoleRequest()
		{
			WorkRole = new WorkRole();
		}

		public WorkRole WorkRole { get; set; }
		//public List<OccupationalTask> OccupationalTasks { get; set; } = new List<OccupationalTask>();
	}

	public class WorkRole 
	{
		public WorkRole()
		{
		}

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
		/// <summary>
		/// Alternately can provide a language map
		/// </summary>
		public LanguageMapList Comment_Map { get; set; }


		/// <summary>
		/// Ctid
		/// Globally unique Credential Transparency Identifier (CTID) by which the creator, owner or provider of a resource recognizes it in transactions with the external environment (e.g., in verifiable claims involving the resource).
		/// </summary>
		public string CTID { get; set; }

		public List<string> inLanguage { get; set; } = new List<string>();

		/// <summary>
		/// Description
		/// Description of the resource.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Alternately can provide a language map
		/// </summary>
		public LanguageMap Description_Map { get; set; } = new LanguageMap();

		/// <summary>
		/// Name
		/// Name of the resource.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Alternately can provide a language map
		/// </summary>
		public LanguageMap Name_Map { get; set; } = new LanguageMap();

		/// <summary>
		/// Version
		/// Version of this resource.
		/// </summary>
		public string Version { get; set; }


	}


}


