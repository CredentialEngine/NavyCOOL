using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RA.Models.JsonV2;

namespace RA.Models.Navy.Json
{


	public class SourceIdentifier : JsonLDDocument
	{
		public SourceIdentifier()
		{
			Type = "navy:SourceIdentifier";
		}
		/// <summary>
		///  type
		/// </summary>
		[JsonProperty( "@type" )]
		public string Type { get; set; }

		/// <summary>
		/// AllowedProductConfigurationIdentifier
		/// String of characters that are unique to the issuing organization which is used to designate an AllowedProductConfiguration and to differentiate it from other AllowedProductConfigurations.
		/// </summary>
		[JsonProperty( "navy:allowedProductConfigurationIdentifier" )]
		public string AllowedProductConfigurationIdentifier { get; set; }

		/// <summary>
		/// Alternate_logistics_support_analysis_control_number_code
		/// Code used to allow documentation of multiple models of a system/equipment, or alternate design considerations of an item, using the same LCN breakdown.
		/// </summary>
		[JsonProperty( "navy:alternate_logistics_support_analysis_control_number_code" )]
		public string Alternate_logistics_support_analysis_control_number_code { get; set; }

		/// <summary>
		/// BreakdownElementIdentifier
		/// String of characters used to uniquely identify a BreakdownElement and to differentiate it from other BreakdownElements that comprise a product.
		/// </summary>
		[JsonProperty( "navy:breakdownElementIdentifier" )]
		public string BreakdownElementIdentifier { get; set; }

		/// <summary>
		/// BreakdownElementRevisionIdentifier
		/// String of characters used to uniquely identify a BreakdownElementRevision and to differentiate it from other BreakdownElementRevisions.
		/// </summary>
		[JsonProperty( "navy:breakdownElementRevisionIdentifier" )]
		public string BreakdownElementRevisionIdentifier { get; set; }

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
		/// End_item_acronym_code
		/// Authoritatively-assigned code that uniquely identifies the system or equipment end item and is constant throughout the item's life cycle.
		/// </summary>
		[JsonProperty( "navy:end_item_acronym_code" )]
		public string End_item_acronym_code { get; set; }

		/// <summary>
		/// Logistics_support_analysis_control_number
		/// Code that represents a functional or hardware generation breakdown/disassembly sequence of system/equipment hardware including SE, training equipment, and installation (connecting) hardware.
		/// </summary>
		[JsonProperty( "navy:logistics_support_analysis_control_number" )]
		public string Logistics_support_analysis_control_number { get; set; }

		/// <summary>
		/// Logistics_support_analysis_control_number_indenture_code
		/// Code that reflects the relationship of the item to the total system.
		/// </summary>
		[JsonProperty( "navy:logistics_support_analysis_control_number_indenture_code" )]
		public string Logistics_support_analysis_control_number_indenture_code { get; set; }

		/// <summary>
		/// Logistics_support_analysis_control_number_type
		/// Code indicating whether the LCN is representative of either a physical or functional breakdown.
		/// </summary>
		[JsonProperty( "navy:logistics_support_analysis_control_number_type" )]
		public string Logistics_support_analysis_control_number_type { get; set; }

		/// <summary>
		/// Logistics Support Analysis Control Number Structure
		/// A number signifying the number of indenture levels represented by the LCN when the LCNs are assigned using the classical or modified classical assignment method. The first digit of the LCN structure is the number of digits used in the LCN to identify the first indenture level. The second digit is the number of digits used to identify the second indenture level, etc.
		/// </summary>
		[JsonProperty( "navy:logistics_support_analysis_control_number_structure" )]
		public string Logistics_support_analysis_control_number_structure { get; set; }

		/// <summary>
		/// PartIdentifier
		/// String of characters that are unique to the issuing organization which is used to designate a PartAsDesigned and to differentiate it from other designed parts.
		/// </summary>
		[JsonProperty( "navy:partIdentifier" )]
		public string PartIdentifier { get; set; }

		/// <summary>
		/// System_end_item_identifier
		/// Code that signifies whether the LCN represents a system, end item, or not a system/end item.
		/// </summary>
		[JsonProperty( "navy:system_end_item_identifier" )]
		public string System_end_item_identifier { get; set; }


	}




}


