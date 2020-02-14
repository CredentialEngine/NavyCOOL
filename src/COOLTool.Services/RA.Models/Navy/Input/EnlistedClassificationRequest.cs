using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.Input;

namespace RA.Models.Navy.Input
{

	public class EnlistedClassificationRequest : BaseRequest
	{
		public EnlistedClassificationRequest()
		{
			EnlistedClassification = new EnlistedClassification();
		}

		public EnlistedClassification EnlistedClassification { get; set; }

	}
	public class EnlistedClassification 
	{
		public EnlistedClassification()
		{
			
		}
		
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
		/// <summary>
		/// Alternately can provide a language map
		/// </summary>
		public LanguageMapList Comment_Map { get; set; }

		/// <summary>
		/// CTID
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
		/// HasRating
		/// Rating related to this resource.
		/// </summary>
		public List<string> HasRating { get; set; } = new List<string>();

		public List<string> InLanguage { get; set; } = new List<string>();

		/// <summary>
		/// LegacyCodeNEC
		/// Alphanumeric code for this classification used for legacy purposes.
		/// </summary>
		public string LegacyCodeNEC { get; set; }

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
		/// CodeNEC
		/// Alphanumeric code for this classification.
		/// </summary>
		public string CodeNEC { get; set; }

		/// <summary>
		/// Version
		/// Version of this resource.
		/// </summary>
		public string Version { get; set; }


	}
}
