using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOLTool.Services.Models.Input
{
	/// <summary>
	/// Coded Framework
	/// Examples
	/// SOC - occupations
	/// NAICS - industries
	/// CIP
	/// </summary>
	public class FrameworkItem
	{
		/// <summary>
		/// URL for framework
		/// </summary>
		public string Framework { get; set; }
		public string FrameworkName { get; set; }

		public string CodedNotation { get; set; }
		//targetNodeName
		public string Name { get; set; }

		//targetNodeDescription
		public string Description { get; set; }

		/// <summary>
		/// URI for the FrameworkItem
		/// </summary>
		public string TargetNode { get; set; }

	}
}
