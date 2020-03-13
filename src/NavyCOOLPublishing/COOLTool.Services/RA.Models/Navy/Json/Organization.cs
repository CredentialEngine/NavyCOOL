using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.JsonV2;

namespace RA.Models.Navy.Json
{


	public class Organization : JsonLDDocument
	{
		public Organization()
		{
			Type = "schema:Organization";
		}
		/// <summary>
		///  type
		/// </summary>
		[JsonProperty( "@type" )]
		public string Type { get; set; }
		/// <summary>
		/// Ctid
		/// Globally unique Credential Transparency Identifier (CTID) by which the creator, owner or provider of a resource recognizes it in transactions with the external environment (e.g., in verifiable claims involving the resource).
		/// </summary>
		[JsonProperty( "ceterms:ctid" )]
		public string CTID { get; set; }

		[JsonProperty( "@id" )]
		public string CtdlId { get; set; }

		[JsonProperty( PropertyName = "ceasn:inLanguage" )]
		public List<string> InLanguage { get; set; } = new List<string>();

		/// <summary>
		/// Description
		/// Description of the resource.
		/// </summary>
		[JsonProperty( "schema:description" )]
		public LanguageMap Description { get; set; }

		/// <summary>
		/// Name
		/// Name of the resource.
		/// </summary>
		[JsonProperty( "schema:name" )]
		public LanguageMap Name { get; set; }

		/// <summary>
		/// MainEntityOfPage
		/// Webpage, document, or other resource that primarily describes this resource.
		/// </summary>
		[JsonProperty( "schema:mainEntityOfPage" )]
		public string MainEntityOfPage { get; set; }

		/// <summary>
		/// ParentOrganization
		/// Larger organization exercising authority over the organization being described.
		/// URI to an Organization
		/// </summary>
		[JsonProperty( "schema:parentOrganization" )]
		public string ParentOrganization { get; set; }


	}




}


