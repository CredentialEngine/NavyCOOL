using System;
using System.Collections.Generic;
using System.Text;

using COOLTool.Models;
namespace COOL.Factories
{
	public class OrganizationManager
	{
		/// <summary>
		/// Add method to retrieve the required dataset
		/// </summary>
		/// <param name="pFilter"></param>
		/// <param name="pOrderBy"></param>
		/// <param name="pageNumber"></param>
		/// <param name="pageSize"></param>
		/// <param name="pTotalRows"></param>
		/// <returns></returns>
		public static List<Agency> Search(string pFilter, string pOrderBy, int pageNumber, int pageSize, ref int pTotalRows)
		{

			var results = new List<Agency>();

			var agency = Get_FAA();
			//var agency = new Agency()
			//{
			//	CA_AgencyID = 1,
			//	CA_AgencyName = "Test Organization",
			//	CA_AgencyPhonePrimary="800-555-1212",
			//	CA_AgencyContact="email#email.com",
			//	CA_AgencyHomePageURL ="http://example.com",
			//	CA_AgencyStreetAddress1 = "",
			//	CA_AgencyStreetAddress2 = "",
			//	CA_AgencyCity = "",
			//	CA_AgencyZip = "",
			//	CA_AgencyState = "",
			//	CA_AgencyCountry = "",
			//}
			results.Add( agency );

			return results;
		}

		public static Agency Get_FAA()
		{
			string FAA_CTID = "ce-0c07ce76-e285-4dad-9b99-d5962e14ea88";

			Agency agency = new Agency();
			//
			agency.CA_AgencyID = 100;
			agency.CTID = FAA_CTID;
			agency.CA_AgencyName = "Federal Aviation Administration (FAA)";
			agency.CA_AgencyStreetAddress1 = "800 Independence Avenue SW";
			agency.CA_AgencyCity = "Washington";
			agency.CA_AgencyState = "DC";
			agency.CA_AgencyZip = "20591";
			agency.CA_AgencyCountry = "USA";
			agency.CA_AgencyPhonePrimary = "(866) 835-5322";
			//note cool site has a link to a help page
			/// http://faa.custhelp.com/app/ask
			agency.CA_AgencyHomePageURL = "https://www.faa.gov/";
			//apparantly a description is available
			agency.Description = "Our continuing mission is to provide the safest, most efficient aerospace system in the world.";

			agency.AgentType.Add( "orgType:Government" );
			agency.AgentSectorType = "agentSector:Public";

			return agency;
		}
	}
}

