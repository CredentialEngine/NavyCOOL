using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RA.Models.Input;

namespace RA.Models.Navy.Input
{
	public class OrganizationRequest : BaseRequest
	{
		public OrganizationRequest()
		{
			Organization = new Organization();
		}

		public Organization Organization { get; set; }
	}
	public class Organization
	{
		/// <summary>
		///  type
		/// </summary>
		public string Type { get; set; }
		/// <summary>
		/// Ctid
		/// Globally unique Credential Transparency Identifier (CTID) by which the creator, owner or provider of a resource recognizes it in transactions with the external environment (e.g., in verifiable claims involving the resource).
		/// </summary>
		public string CTID { get; set; }

		public List<string> InLanguage { get; set; } = new List<string>();

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
		/// MainEntityOfPage
		/// Webpage, document, or other resource that primarily describes this resource.
		/// </summary>
		public string MainEntityOfPage { get; set; }

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
		/// ParentOrganization
		/// Larger organization exercising authority over the organization being described.
		/// URI to an Organization
		/// </summary>
		public string ParentOrganization { get; set; }
	}
}
