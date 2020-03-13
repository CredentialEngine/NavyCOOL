using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.Input;

namespace RA.Models.Navy.Input
{
	public class SystemRequest : BaseRequest
	{
		public SystemRequest()
		{
			System = new System();
		}

		public System System { get; set; }
	}
	public class System 
	{
		public System()
		{
		}

		public List<string> InLanguage { get; set; } = new List<string>();


		/// <summary>
		/// CodedNotation
		/// An alphanumeric notation or ID code as defined by the promulgating body to identify this resource.
		/// </summary>
		public string CodedNotation { get; set; }

		/// <summary>
		/// Comment
		/// Supplemental text provided by the promulgating body that clarifies the nature, scope or use of this competency.
		/// </summary>
		public List<string> Comment { get; set; } = new List<string>();
		public LanguageMapList Comment_Map { get; set; } = new LanguageMapList();

		/// <summary>
		/// Ctid
		/// Globally unique Credential Transparency Identifier (CTID) by which the creator, owner or provider of a resource recognizes it in transactions with the external environment (e.g., in verifiable claims involving the resource).
		/// </summary>
		public string CTID { get; set; }

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
		/// HasChild
		/// System related to this resource.
		/// URI for a Systen
		/// </summary>
		public List<string> HasChild { get; set; } = new List<string>();

		/// <summary>
		/// HasMaintenanceTask
		/// Maintenance task related to this resource.
		/// URI for a MaintenanceTask
		/// </summary>
		public List<string> HasMaintenanceTask { get; set; }

		/// <summary>
		/// HasPart
		/// System that is part of this system.
		/// URI for a System
		/// </summary>
		public List<string> HasPart { get; set; }

		/// <summary>
		/// HasSourceIdentifier
		/// A collection of identifiers related to this resource.
		/// URI for a SourceIdentifier
		/// </summary>
		public List<string> HasSourceIdentifier { get; set; }

		public List<string> IsChildOf { get; set; }

		//public List<string> HasComponent { get; set; } = new List<string>();
		//public List<string> IsComponentOf { get; set; } = new List<string>();

		/// <summary>
		/// IsPartOf
		/// System that this system is a part of.
		/// URI for a System
		/// </summary>
		public string IsPartOf { get; set; }

		/// <summary>
		/// ManagedBy
		/// Organization that manages this resource.
		/// URI for an Organization
		/// </summary>
		public string ManagedBy { get; set; }

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
		/// Version
		/// Version of this resource.
		/// </summary>
		public string Version { get; set; }


	}
}


