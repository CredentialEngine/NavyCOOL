using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RA.Models.Input;

namespace RA.Models.Navy.Input
{
	public class JobRequest : BaseRequest
	{
		public JobRequest()
		{
			Job = new Job();
		}

		public Job Job { get; set; }
	}

	public class Job 
	{

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
		/// CTID		
		/// Globally unique Credential Transparency Identifier (CTID) by which the creator, owner or provider of a resource recognizes it in transactions with the external environment (e.g., in verifiable claims involving the resource).
		/// </summary>
		public string CTID { get; set; }

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
		/// HasMaintenanceTask
		/// Maintenance task related to this resource.
		/// URI to a maintenance task
		/// </summary>
		//public List<string> HasMaintenanceTask { get; set; }

		/// <summary>
		/// HasOccupationalTask
		/// Occupational task related to this resource.
		/// URI to an occupational task
		/// </summary>
		public List<string> HasOccupationalTask { get; set; }

		/// <summary>
		/// HasRating
		/// Rating related to this resource.
		/// URI to a Rating
		/// </summary>
		public List<string> HasRating { get; set; }

		/// <summary>
		/// HasTrainingTask
		/// Training task related to this resource.
		/// URI to a training task
		/// </summary>
		//public List<string> HasTrainingTask { get; set; }

		/// <summary>
		/// MainEntityOfPage
		/// Webpage, document, or other resource that primarily describes this resource.
		/// </summary>
		public string MainEntityOfPage { get; set; }

		/// <summary>
		/// ManagedBy
		/// Organization that manages this resource.
		/// URI to an organization
		/// </summary>
		public string ManagedBy { get; set; }

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
		/// RequiresCredential
		/// Credential required to perform one or more of the tasks or functions identified by this resource.
		/// URI to a credential
		/// </summary>
		public List<string> RequiresCredential { get; set; }

		/// <summary>
		/// ShortName14
		/// Shortened version of the item's name, with a maximum of 14 characters.
		/// </summary>
		public string ShortName14 { get; set; }

		/// <summary>
		/// ShortName30
		/// Shortened version of the item's name, with a maximum of 30 characters.
		/// </summary>
		public string ShortName30 { get; set; }

		/// <summary>
		/// Version
		/// Version of this resource.
		/// </summary>
		public string Version { get; set; }


	}

}


