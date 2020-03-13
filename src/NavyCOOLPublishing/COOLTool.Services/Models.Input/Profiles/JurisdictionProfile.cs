using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RAPlace = RA.Models.Input.Place;
namespace COOLTool.Services.Models.Input
{
	public class JurisdictionProfile
	{
		public JurisdictionProfile()
		{
			JurisdictionException = new List<Address>();
		}
		public bool GlobalJurisdiction { get; set; }
		public string Description { get; set; }

		/// <summary>
		/// TBD - does it make sense to offer providing the full GeoCoordinates.
		/// Will be useful where the request can be populated programatically.
		/// </summary>
		public Address MainJurisdiction { get; set; } = new Address();

		public List<Address> JurisdictionException { get; set; }
	}
}
