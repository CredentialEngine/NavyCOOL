using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Models
{
	public class FinancialAssistanceProfile
	{

		public string Name { get; set; }
		public string Description { get; set; }
		/// <summary>
		/// SubjectWebpage - URI
		/// </summary>
		public string SubjectWebpage { get; set; }

		/// <summary>
		/// Financial Assistance type
		/// Need to use concepts from concept scheme:
		/// https://credreg.net/ctdl/terms/FinancialAssistance
		/// </summary>
		public List<string> FinancialAssistanceType { get; set; } = new List<string>();
	}
}
