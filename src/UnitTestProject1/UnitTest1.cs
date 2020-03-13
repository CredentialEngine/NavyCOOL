using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using COOLTool.Services;
using COOLTool.Services.Models.Input;
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
			RequestHelper helper = new RequestHelper();
			var request = new COOLTool.Services.Models.Input.OrganizationRequest();

			var input = SampleData.Get_FAA();
			
			List<string> messages = new List<string>();
			string crEnvelopeId = "";
			//NOTE this should be set to empty before publishing to Github
			string publisherApiKey = UtilityManager.GetAppKeyValue( "coolOrgApiKey" );
			//157ce70f-2020-0216-8b02-ad549a332115
			string community = "navy";
			try
			{
				request.Agency = input;
				request.OwningOrganizationCTID = input.CTID;
				request.Community = community;
				helper.ApiKey = publisherApiKey;
				
				var payload = new OrganizationServices().Publish( request, helper );
				if ( helper.IsValidRequest )
				{

				}
				else
				{

				}
				//var result = new OrganizationServices().Publish( org, publisherApiKey, ref isValid, ref messages, community );
			} catch (Exception ex)
			{
				LoggingHelper.DoTrace( 1, "exception: " + ex.Message );
			}
		}

		[TestMethod]
		public void PublishAircraftDispatcher()
		{
			RequestHelper helper = new RequestHelper();
			var request = new COOLTool.Services.Models.Input.CredentialRequest();
			//var credential = SampleData.Get_AircraftDispatcher();
			var input = SampleData.Get_AircraftDispatcher_As_ARTT();
			bool isValid = true;
			List<string> messages = new List<string>();
			string crEnvelopeId = "";
			//NOTE this should be set to empty before publishing to Github
			string publisherApiKey = UtilityManager.GetAppKeyValue( "coolOrgApiKey" );
			string community = "navy";
			try
			{
				request.Credential = input;
				request.OwningOrganizationCTID = input.CA_AgencyCTID;
				request.Community = community;
				helper.ApiKey = publisherApiKey;

				var payload = new CredentialServices().Publish( request, helper );
				if ( helper.IsValidRequest )
				{

				}
				else
				{

				}
				//var result = new OrganizationServices().Publish( org, publisherApiKey, ref isValid, ref messages, community );
			}
			catch ( Exception ex )
			{
				LoggingHelper.DoTrace( 1, "exception: " + ex.Message );
			}
		}
	}
}
