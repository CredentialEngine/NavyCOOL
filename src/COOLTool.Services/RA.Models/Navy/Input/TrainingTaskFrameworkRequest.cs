using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.Input;

namespace RA.Models.Navy.Input
{
	public class TrainingTaskFrameworkRequest : BaseRequest
	{
		public TrainingTaskFrameworkRequest()
		{
			TrainingTaskFramework = new TrainingTaskFramework();
		}

		public TrainingTaskFramework TrainingTaskFramework { get; set; }

		//TrainingTask for include TaskFramework
		public List<TrainingTask> TrainingTasks { get; set; } = new List<TrainingTask>();
	}

	public class TrainingTaskFramework 
	{
		public TrainingTaskFramework()
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
		/// URI to a Training Task
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




}


