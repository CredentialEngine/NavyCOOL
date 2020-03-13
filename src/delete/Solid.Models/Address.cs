using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Models
{


    public class Address : BaseObject
    {
        public Address()
        {
        }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        //CTDL only has a single street address
        public string StreetAddress { get { return Address1 + (string.IsNullOrWhiteSpace( Address2 ) ? "" : " " + Address2); } set { Address1 = value; } } //Can't determine address1 vs address2
        public string PostOfficeBoxNumber { get; set; }

        public string City { get; set; }

        public string AddressRegion { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        /// <summary>
        /// URI to geonames location
        /// </summary>
        public string GeoURI { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string LooseDisplayAddress( string separator = ", " ) //For easier geocoding
        {
            return
                ( string.IsNullOrWhiteSpace( City ) ? "" : City + separator ) +
                ( string.IsNullOrWhiteSpace( AddressRegion ) ? "" : AddressRegion + separator ) +
                ( string.IsNullOrWhiteSpace( PostalCode ) ? "" : PostalCode + " " ) +
                ( string.IsNullOrWhiteSpace( Country ) ? "" : Country );
        }
        public bool HasAddress()
        {
            bool hasAddress = true;

            if ( string.IsNullOrWhiteSpace( Address1 )
            && string.IsNullOrWhiteSpace( Address2 )
            && string.IsNullOrWhiteSpace( City )
            && string.IsNullOrWhiteSpace( AddressRegion )
            && string.IsNullOrWhiteSpace( PostalCode )
                )
                hasAddress = false;

            return hasAddress;
        }
        public bool HasLatLng()
        {
            if ( Latitude != 0 && Longitude != 0 )
                return true;
            else
                return false;
        }


		public List<ContactPoint> ContactPoint { get; set; } = new List<ContactPoint>();

    }
    //
    public class ContactPoint
    {
        public ContactPoint()
        {
            PhoneNumbers = new List<string>();
            Emails = new List<string>();
            SocialMediaPages = new List<string>();
        }
        /// <summary>
        /// Name of the Contact Point 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Specification of the type of contact
        /// Example: Registration
        /// </summary>
        public string ContactType { get; set; }

        public List<string> PhoneNumbers { get; set; }
        /// <summary>
        /// List of email addresses
        /// </summary>
        public List<string> Emails { get; set; }
        /// <summary>
        /// List of URIs to social media pages
        /// </summary>
        public List<string> SocialMediaPages { get; set; }

    }
}
