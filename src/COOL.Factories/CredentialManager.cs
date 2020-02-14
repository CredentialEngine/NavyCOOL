using System;
using System.Collections.Generic;
using COOLTool.Models;

namespace COOL.Factories
{
	public class CredentialManager
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
		public static List<Credential> Search(string pFilter, string pOrderBy, int pageNumber, int pageSize, ref int pTotalRows )
		{

			var results = new List<Credential>();

			var credential = new Credential() { CA_AgencyID = 1, CE_CertID = 1, CE_CertTitle = "Test Credential" };
			results.Add( credential );

			return results;
		}
	}
}
