using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.JsonV2;

namespace RA.Models.Navy.Json
{


	public class Job : JsonLDDocument
	{
		public Job()
		{
			Type = "navy:Job";
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
		public LanguageMap Description { get; set; }

		/// <summary>
		/// HasMaintenanceTask
		/// Maintenance task related to this resource.
		/// URI to a maintenance task
		/// </summary>
		//[JsonProperty( "navy:hasMaintenanceTask" )]
		//public List<string> HasMaintenanceTask { get; set; }

		/// <summary>
		/// HasOccupationalTask
		/// Occupational task related to this resource.
		/// URI to an occupational task
		/// </summary>
		[JsonProperty( "navy:hasOccupationalTask" )]
		public List<string> HasOccupationalTask { get; set; }

		/// <summary>
		/// HasRating
		/// Rating related to this resource.
		/// URI to a Rating
		/// </summary>
		[JsonProperty( "navy:hasRating" )]
		public List<string> HasRating { get; set; }

		/// <summary>
		/// HasTrainingTask
		/// Training task related to this resource.
		/// URI to a training task
		/// </summary>
		//[JsonProperty( "navy:hasTrainingTask" )]
		//public List<string> HasTrainingTask { get; set; }

		/// <summary>
		/// ManagedBy
		/// Organization that manages this resource.
		/// URI to an organization
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
		/// RequiresCredential
		/// Credential required to perform one or more of the tasks or functions identified by this resource.
		/// URI to a credential
		/// </summary>
		[JsonProperty( "navy:requiresCredential" )]
		public List<string> RequiresCredential { get; set; }

		/// <summary>
		/// ShortName14
		/// Shortened version of the item's name, with a maximum of 14 characters.
		/// </summary>
		[JsonProperty( "navy:shortName14" )]
		public string ShortName14 { get; set; }

		/// <summary>
		/// ShortName30
		/// Shortened version of the item's name, with a maximum of 30 characters.
		/// </summary>
		[JsonProperty( "navy:shortName30" )]
		public string ShortName30 { get; set; }

		/// <summary>
		/// Version
		/// Version of this resource.
		/// </summary>
		[JsonProperty( "schema:version" )]
		public string Version { get; set; }


	}


	public class JobPlain : Job
	{
		public JobPlain()
		{
			Type = "navy:Job";
		}
		/// <summary>
		///  type
		/// </summary>
		//[JsonProperty( "@type" )]
		//public string Type { get; set; }

		//[JsonProperty( "@id" )]
		//public string CtdlId { get; set; }

		//[JsonProperty( PropertyName = "ceasn:inLanguage" )]
		//public List<string> InLanguage { get; set; } = new List<string>();

		/// <summary>
		/// CodedNotation
		/// An alphanumeric notation or ID code as defined by the promulgating body to identify this resource.
		/// </summary>
		//[JsonProperty( "ceasn:codedNotation" )]
		//public string CodedNotation { get; set; }

		/// <summary>
		/// Comment
		/// Supplemental text provided by the promulgating body that clarifies the nature, scope or use of this competency.
		/// </summary>
		[JsonProperty( "ceasn:comment" )]
		public new List<string> Comment { get; set; }

		/// <summary>
		/// CTID		
		/// Globally unique Credential Transparency Identifier (CTID) by which the creator, owner or provider of a resource recognizes it in transactions with the external environment (e.g., in verifiable claims involving the resource).
		/// </summary>
		//[JsonProperty( "ceterms:ctid" )]
		//public string CTID { get; set; }

		/// <summary>
		/// Description
		/// Description of the resource.
		/// </summary>
		[JsonProperty( "schema:description" )]
		public new string Description { get; set; }

		/// <summary>
		/// HasMaintenanceTask
		/// Maintenance task related to this resource.
		/// URI to a maintenance task
		/// </summary>
		//[JsonProperty( "navy:hasMaintenanceTask" )]
		//public List<string> HasMaintenanceTask { get; set; }

		/// <summary>
		/// HasOccupationalTask
		/// Occupational task related to this resource.
		/// URI to an occupational task
		/// </summary>
		//[JsonProperty( "navy:hasOccupationalTask" )]
		//public List<string> HasOccupationalTask { get; set; }

		/// <summary>
		/// HasRating
		/// Rating related to this resource.
		/// URI to a Rating
		/// </summary>
		//[JsonProperty( "navy:hasRating" )]
		//public List<string> HasRating { get; set; }

		/// <summary>
		/// ManagedBy
		/// Organization that manages this resource.
		/// URI to an organization
		/// </summary>
		//[JsonProperty( "navy:managedBy" )]
		//public string ManagedBy { get; set; }

		/// <summary>
		/// Name
		/// Name of the resource.
		/// </summary>
		[JsonProperty( "schema:name" )]
		public new string Name { get; set; }

		/// <summary>
		/// RequiresCredential
		/// Credential required to perform one or more of the tasks or functions identified by this resource.
		/// URI to a credential
		/// </summary>
		//[JsonProperty( "navy:requiresCredential" )]
		//public List<string> RequiresCredential { get; set; }

		/// <summary>
		/// ShortName14
		/// Shortened version of the item's name, with a maximum of 14 characters.
		/// </summary>
		//[JsonProperty( "navy:shortName14" )]
		//public string ShortName14 { get; set; }

		/// <summary>
		/// ShortName30
		/// Shortened version of the item's name, with a maximum of 30 characters.
		/// </summary>
		//[JsonProperty( "navy:shortName30" )]
		//public string ShortName30 { get; set; }

		///// <summary>
		///// Version
		///// Version of this resource.
		///// </summary>
		//[JsonProperty( "schema:version" )]
		//public string Version { get; set; }
	}
}


