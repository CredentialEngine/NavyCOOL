using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.Input;

namespace RA.Models.Navy.Input
{
	public class MaintenanceTaskFrameworkRequest : BaseRequest
	{
		public MaintenanceTaskFrameworkRequest()
		{
			MaintenanceTaskFramework = new MaintenanceTaskFramework();
		}

		public MaintenanceTaskFramework MaintenanceTaskFramework { get; set; }
		public List<MaintenanceTask> MaintenanceTasks { get; set; } = new List<MaintenanceTask>();
	}

	public class MaintenanceTaskFramework
	{
		public MaintenanceTaskFramework()
		{
		}
		/// <summary>
		/// Comment
		/// Supplemental text provided by the promulgating body that clarifies the nature, scope or use of this competency.
		/// </summary>
		public List<string> Comment { get; set; } = new List<string>();
		public LanguageMapList Comment_Map { get; set; } = new LanguageMapList();

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
		/// HasTopChild
		/// Indicates that this competency is at the top of some arbitrary hierarchy.
		/// URI to a Maintenance Task
		/// </summary>
		public List<string> HasTopChild { get; set; } = new List<string>();

		/// <summary>
		/// Name
		/// Name of the resource.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Alternately can provide a language map
		/// </summary>
		public LanguageMap Name_Map { get; set; } = new LanguageMap();


	}

	//public class MaintenanceTask : BaseTask
	//{
	//	public MaintenanceTask()
	//	{
	//	}

	//	/// <summary>
	//	/// HasSourceIdentifier
	//	/// A collection of identifiers (URIs) related to this resource.
	//	/// URI to a source identifier
	//	/// </summary>
	//	public List<string> HasSourceIdentifier { get; set; }


	//	/// <summary>
	//	/// RequiresRating
	//	/// Type of Rating(s) that is to perform this task or function.
	//	/// URI to a Rating
	//	/// </summary>
	//	public List<string> RequiresRating { get; set; } = new List<string>();

	//	/// <summary>
	//	/// TaskText
	//	/// Text describing the task to be performed.
	//	/// </summary>
	//	public string TaskText { get; set; }
	//	public LanguageMap TaskText_Map { get; set; }

	//}

}


