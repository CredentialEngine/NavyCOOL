﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RA.Models.Input
{

	public class GraphRequest : BaseRequest
	{
		public GraphRequest()
		{
			GraphInput = new GraphInput();
			HasLanguageMaps = true;
		}

		//*** shouldn't use this!!!	
		public string CTID { get; set; } = "";

		/// <summary>
		/// Generate HasTopChild
		/// if true, the HasTopChild property is not included in the input document. The HasTopChild property in the JSON document will be generated from the Concept list.
		/// Should only be used where the structure is flat. That is there are no concepts have child concepts. SO that is: all concepts are top childs.
		/// Note: in some cases IsTopChild was provided and not HasTopChild. In this case: GenerateHasTopChild=true, and GenerateIsTopChild=false
		/// </summary>
		public bool GenerateHasTopChild { get; set; } = false;
		/// <summary>
		/// Generate HasTopChild where the child has property of: Top'Child'Of
		/// </summary>
		public bool GenerateHasTopChildFromIsTopChild { get; set; } = false;
		/// <summary>
		/// Generate IsTopChild
		/// if true, the IsTopChild property must not be included in the input document and the IsTopChild property in the JSON document will be generated for each concept in the list.
		/// Must only be used where the structure is flat. That is there are no concepts have child concepts.
		/// </summary>
		public bool GenerateIsTopChild { get; set; } = false;

		public bool HasLanguageMaps { get; set; }

		public GraphInput GraphInput { get; set; } 

	}
	/// <summary>
	/// The plain graph doesn't include language maps
	/// </summary>
	public class GraphInput
	{
		/// <summary>
		/// Main graph object
		/// </summary>
		[JsonProperty( "@graph" )]
		public object Graph { get; set; }

		[JsonProperty( "@context" )]
		public string Context { get; set; }
	}
}
