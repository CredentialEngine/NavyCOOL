using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RAPlace = RA.Models.Input.Place;
namespace RA.Models.Input
{
	public class Jurisdiction
	{
		public Jurisdiction()
		{
			JurisdictionException = new List<RAPlace>();
		}
		public bool? GlobalJurisdiction { get; set; }
		public string Description { get; set; }

		/// <summary>
		/// TBD - does it make sense to offer providing the full GeoCoordinates.
		/// Will be useful where the request can be populated programatically.
		/// </summary>
		public RAPlace MainJurisdiction { get; set; } = new RAPlace();


		public List<RAPlace> JurisdictionException { get; set; }
	}

	/// <summary>
	/// One or more Organizations that make a specific Quality Assurance assertion for a specific jurisdiction. 
	/// </summary>
	public class JurisdictionAssertion : Jurisdiction
	{
		/// <summary>
		/// List of Organizations that asserts this condition
		/// Required
		/// </summary>
		public List<OrganizationReference> AssertedBy { get; set; } = new List<OrganizationReference>();
	}


}
