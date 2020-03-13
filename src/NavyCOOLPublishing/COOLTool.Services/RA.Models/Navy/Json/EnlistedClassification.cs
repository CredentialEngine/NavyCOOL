using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;


namespace RA.Models.Navy.Json
{

	public class EnlistedClassificationPlain 
	{
		public EnlistedClassificationPlain()
		{
			Type = "navy:EnlistedClassification";
		}
		/// <summary>
		///  type
		/// </summary>
		[JsonProperty( "@type" )]
		public string Type { get; set; }

		[JsonProperty( "@id" )]
		public string CtdlId { get; set; }

		[JsonProperty( PropertyName = "ceasn:inLanguage" )]
		public List<string> InLanguage { get; set; } = new List<string>();

		/// <summary>
		/// CodedNotation
		/// An alphanumeric notation or ID code as defined by the promulgating body to identify this resource.
		/// </summary>
		[JsonProperty( "ceasn:codedNotation" )]
		public string CodedNotation { get; set; }

		/// <summary>
		/// Comment
		/// Supplemental text provided by the promulgating body that clarifies the nature, scope or use of this competency.
		/// </summary>
		[JsonProperty( "ceasn:comment" )]
		public List<string> Comment { get; set; }

		/// <summary>
		/// CTID
		/// Globally unique Credential Transparency Identifier (CTID) by which the creator, owner or provider of a resource recognizes it in transactions with the external environment (e.g., in verifiable claims involving the resource).
		/// </summary>
		[JsonProperty( "ceterms:ctid" )]
		public string CTID { get; set; }

		/// <summary>
		/// Description
		/// Description of the resource.
		/// </summary>
		[JsonProperty( "schema:description" )]
		public string Description { get; set; }

		/// <summary>
		/// HasRating
		/// Rating related to this resource.
		/// URI to a Rating
		/// </summary>
		[JsonProperty( "navy:hasRating" )]
		public List<string> HasRating { get; set; }

		/// <summary>
		/// LegacyCodeNEC
		/// Alphanumeric code for this classification used for legacy purposes.
		/// </summary>
		[JsonProperty( "navy:legacyCodeNEC" )]
		public string LegacyCodeNEC { get; set; }

		/// <summary>
		/// Name
		/// Name of the resource.
		/// </summary>
		[JsonProperty( "schema:name" )]
		public string Name { get; set; }

		/// <summary>
		/// CodeNEC
		/// Alphanumeric code for this classification.
		/// </summary>
		[JsonProperty( "navy:codeNEC" )]
		public string CodeNEC { get; set; }

		/// <summary>
		/// Version
		/// Version of this resource.
		/// </summary>
		[JsonProperty( "schema:version" )]
		public string Version { get; set; }


	}

}


