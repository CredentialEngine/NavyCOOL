using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOLTool.Services.Models.Input
{
	/// <summary>
	/// API Publish Response
	/// </summary>
	public class ApiPublishResponse
	{
		public ApiPublishResponse()
		{
			Messages = new List<string>();
			Payload = "";
		}

		/// True if action was successfull, otherwise false
		public bool Successful { get; set; }
		/// <summary>
		/// List of error or warning messages
		/// </summary>
		public List<string> Messages { get; set; }

		/// <summary>
		/// URL for the registry envelope that contains the document just add/updated
		/// </summary>
		public string EnvelopeUrl { get; set; }
		/// <summary>
		/// URL for the graph endpoint for the document just add/updated
		/// </summary>
		public string GraphUrl { get; set; }

		/// <summary>
		/// Identifier for the registry envelope that contains the document just add/updated
		/// </summary>
		public string RegistryEnvelopeIdentifier { get; set; }

		/// <summary>
		/// Payload of request to registry, containing properties formatted as CTDL - JSON-LD
		/// </summary>
		public string Payload { get; set; }
	}

	public class ApiDeleteResponse
	{
		public ApiDeleteResponse()
		{
			Messages = new List<string>();
		}
		/// <summary>
		/// True if delete was successfull, otherwise false
		/// </summary>
		public bool Successful { get; set; }

		/// <summary>
		/// List of error or warning messages
		/// </summary>
		public List<string> Messages { get; set; }

	}
}
