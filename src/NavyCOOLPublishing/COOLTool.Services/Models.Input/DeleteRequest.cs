using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOLTool.Services.Models.Input
{
	public class DeleteRequest
	{
		/// <summary>
		/// CTID of document to be deleted
		/// </summary>
		public string CTID { get; set; }

		/// <summary>
		/// Identifier for Organization which Owns the data being published
		/// 2017-12-13 - this will be the CTID for the owning org.
		/// </summary>
		public string PublishForOrganizationIdentifier { get; set; }

		/// <summary>
		/// Leave blank for default
		/// </summary>
		public string Community { get; set; }
	}
}
