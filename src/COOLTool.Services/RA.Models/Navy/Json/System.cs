using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.JsonV2;

namespace RA.Models.Navy.Json
{
	public class System : JsonLDDocument
	{
		public System()
		{
			Type = "navy:System";
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
		public LanguageMapList Comment { get; set; }

		/// <summary>
		/// Ctid
		/// Globally unique Credential Transparency Identifier (CTID) by which the creator, owner or provider of a resource recognizes it in transactions with the external environment (e.g., in verifiable claims involving the resource).
		/// </summary>
		[JsonProperty( "ceterms:ctid" )]
		public string CTID { get; set; }

		/// <summary>
		/// Description
		/// Description of the resource.
		/// </summary>
		[JsonProperty( "schema:description" )]
		public LanguageMap Description { get; set; }

		/// <summary>
		/// HasChild
		/// System related to this resource.
		/// URI for a Systen
		/// </summary>
		[JsonProperty( "ceasn:hasChild" )]
		public List<string> HasChild{ get; set; }

		/// <summary>
		/// HasMaintenanceTask
		/// Maintenance task related to this resource.
		/// URI for a MaintenanceTask
		/// </summary>
		[JsonProperty( "navy:hasMaintenanceTask" )]
		public List<string> HasMaintenanceTask { get; set; }

		/// <summary>
		/// HasPart
		/// System that is part of this system.
		/// URI for a System
		/// </summary>
		[JsonProperty( "ceasn:hasPart" )]
		public List<string> HasPart { get; set; }

		/// <summary>
		/// HasSourceIdentifier
		/// A collection of identifiers related to this resource.
		/// URI for a SourceIdentifier
		/// </summary>
		[JsonProperty( "navy:hasSourceIdentifier" )]
		public List<string> HasSourceIdentifier { get; set; }

		[JsonProperty( "ceasn:isChildOf" )]
		public List<string> IsChildOf { get; set; }

		//[JsonProperty( "navy:hasComponent" )]
		//public List<string> HasComponent { get; set; } = new List<string>();

		//[JsonProperty( "navy:isComponentOf" )]
		//public List<string> IsComponentOf { get; set; } = new List<string>();

		/// <summary>
		/// IsPartOf
		/// System that this system is a part of.
		/// URI for a System
		/// </summary>
		[JsonProperty( "ceasn:isPartOf" )]
		public string IsPartOf { get; set; }

		/// <summary>
		/// ManagedBy
		/// Organization that manages this resource.
		/// URI for an Organization
		/// </summary>
		[JsonProperty( "navy:managedBy" )]
		public string ManagedBy { get; set; }

		/// <summary>
		/// MainEntityOfPage
		/// Webpage, document, or other resource that primarily describes this resource.
		/// </summary>
		[JsonProperty( "schema:mainEntityOfPage" )]
		public string MainEntityOfPage { get; set; }

		/// <summary>
		/// Name
		/// Name of the resource.
		/// </summary>
		[JsonProperty( "schema:name" )]
		public LanguageMap Name { get; set; }

		/// <summary>
		/// Version
		/// Version of this resource.
		/// </summary>
		[JsonProperty( "schema:version" )]
		public string Version { get; set; }


	}

	public class SystemPlain : System
	{

		[JsonProperty( "ceasn:comment" )]
		public new List<string> Comment { get; set; }

		[JsonProperty( "schema:description" )]
		public new string Description { get; set; }

		[JsonProperty( "schema:name" )]
		public new string Name { get; set; }
	}
}


