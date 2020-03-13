using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOLTool.Services.Models.Input
{
	public class BaseRequest
	{
		/// <summary>
		/// DefaultLanguage is used with Language maps where there is more than one entry for InLanguage, and the user doesn't want to have the first language in the list be the language used with language maps. 
		/// </summary>
		public string DefaultLanguage { get; set; } = "en-US";
		/// <summary>
		/// Identifier for Organization which Owns the data being published
		/// 2017-12-13 - this will be the CTID for the owning org, even if publisher is third party.
		/// </summary>
		public string OwningOrganizationCTID { get; set; }


		/// <summary>
		/// Leave blank for default
		/// </summary>
		public string Community { get; set; }
	}
}
