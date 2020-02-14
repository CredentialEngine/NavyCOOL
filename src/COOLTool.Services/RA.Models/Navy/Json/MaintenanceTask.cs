using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.JsonV2;

namespace RA.Models.Navy.Json
{
	public class MaintenanceTask : BaseTask
	{
		public MaintenanceTask()
		{
			Type = "navy:MaintenanceTask";
		}
		/// <summary>
		///  type
		/// </summary>
		[JsonProperty( "@type" )]
		public string Type { get; set; }

		//[JsonProperty( "@id" )]
		//public string CtdlId { get; set; }

		//[JsonProperty( PropertyName = "ceasn:inLanguage" )]
		//public List<string> InLanguage { get; set; } = new List<string>();

		[JsonProperty( "navy:hasPerformanceObjective" )]
		public List<string> HasPerformanceObjective { get; set; }

		/// <summary>
		/// HasSourceIdentifier
		/// A collection of identifiers (URIs) related to this resource.
		/// URI to a source identifier
		/// </summary>
		[JsonProperty( "navy:hasSourceIdentifier" )]
		public List<string> HasSourceIdentifier { get; set; }


		/// <summary>
		/// RequiresRating
		/// Type of Rating(s) that is to perform this task or function.
		/// URI to a Rating
		/// </summary>
		[JsonProperty( "navy:hasRating" )]
		public List<string> HasRating { get; set; } = new List<string>();

		[JsonProperty( "navy:hasSystem" )]
		public List<string> HasSystem { get; set; } = new List<string>();

		/// <summary>
		/// HasTrainingTask
		/// Training task related to this resource.
		/// URI to a training task
		/// </summary>
		[JsonProperty( "navy:hasTrainingTask" )]
		public List<string> HasTrainingTask { get; set; }

		/// <summary>
		/// ToolType
		/// Type of tool used to perform this item.
		/// URI to a Concept
		/// </summary>
		[JsonProperty( "navy:toolType" )]
		public List<string> ToolType { get; set; } = new List<string>();
	}

	public class MaintenanceTaskPlain : BaseTaskPlain
	{
		public MaintenanceTaskPlain()
		{
			Type = "navy:MaintenanceTask";
		}
		/// <summary>
		///  type
		/// </summary>
		[JsonProperty( "@type" )]
		public string Type { get; set; }

		[JsonProperty( "navy:hasPerformanceObjective" )]
		public List<string> HasPerformanceObjective { get; set; }

		/// <summary>
		/// HasSourceIdentifier
		/// A collection of identifiers (URIs) related to this resource.
		/// URI to a source identifier
		/// </summary>
		[JsonProperty( "navy:hasSourceIdentifier" )]
		public List<string> HasSourceIdentifier { get; set; }


		/// <summary>
		/// RequiresRating
		/// Type of Rating(s) that is to perform this task or function.
		/// URI to a Rating
		/// </summary>
		[JsonProperty( "navy:hasRating" )]
		public List<string> HasRating { get; set; } = new List<string>();
		[JsonProperty( "navy:hasSystem" )]
		public List<string> HasSystem { get; set; } = new List<string>();

		/// <summary>
		/// HasTrainingTask
		/// Training task related to this resource.
		/// URI to a training task
		/// </summary>
		[JsonProperty( "navy:hasTrainingTask" )] 
		public List<string> HasTrainingTask { get; set; }

		[JsonProperty( "navy:toolType" )]
		public List<string> ToolType { get; set; } = new List<string>();
	}


}


