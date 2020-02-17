using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using COOL.Factories;
using COOLTool.Services;
using ThisEntity = Solid.Models.AgencyDTO;
using Utilities;
namespace UnitTestProject1
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void PublishFAA()
		{

			var org = SampleData.Get_FAA();
			bool isValid = true;
			List<string> messages = new List<string>();
			string crEnvelopeId = "";
			//NOTE this should be set to empty before publishing to Github
			string publisherApiKey = UtilityManager.GetAppKeyValue( "coolOrgApiKey" );
			string community = "navy";

			var result = new PublishOrganization().Publish( org, publisherApiKey, ref isValid, ref messages, community );
		}

		[TestMethod]
		public void PublishAircraftDispatcher()
		{

			//var credential = SampleData.Get_AircraftDispatcher();
			var credential = CredentialManager.Get_AircraftDispatcher();
			bool isValid = true;
			List<string> messages = new List<string>();
			string crEnvelopeId = "";
			//NOTE this should be set to empty before publishing to Github
			string publisherApiKey = UtilityManager.GetAppKeyValue( "coolOrgApiKey" );
			string community = "navy";

			var result = new PublishCredential().Publish( credential, publisherApiKey, ref isValid, ref messages, community );
		}
	}
}
