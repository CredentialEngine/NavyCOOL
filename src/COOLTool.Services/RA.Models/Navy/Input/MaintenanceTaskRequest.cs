using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.Input;

namespace RA.Models.Navy.Input
{

	public class MaintenanceTaskRequest : BaseRequest
	{
		public MaintenanceTaskRequest()
		{
			MaintenanceTask = new MaintenanceTask();
		}

		public MaintenanceTask MaintenanceTask { get; set; }

	}

	public class MaintenanceTask : BaseTask
	{
		public MaintenanceTask()
		{
		}

		/// <summary>
		/// Performance objective for this resource.
		/// URI to a competency
		/// </summary>
		public List<string> HasPerformanceObjective { get; set; }

		/// <summary>
		/// HasSourceIdentifier
		/// A collection of identifiers (URIs) related to this resource.
		/// URI to a source identifier
		/// </summary>
		public List<string> HasSourceIdentifier { get; set; }


		/// <summary>
		/// RequiresRating
		/// Type of Rating(s) that is to perform this task or function.
		/// URI to a Rating
		/// </summary>
		public List<string> HasRating { get; set; } = new List<string>();

		/// <summary>
		/// System related to this resource.
		/// </summary>
		public List<string> HasSystem{ get; set; } = new List<string>();

		public List<string> HasTrainingTask { get; set; } = new List<string>();

		public List<string> ToolType { get; set; } = new List<string>();

	}
}


