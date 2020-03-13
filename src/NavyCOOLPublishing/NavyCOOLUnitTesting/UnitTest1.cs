using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using COOLTool.Services.Models.Input;
using ThisEntity = COOLTool.Services.Models.Input.Agency;
using InputRequest = COOLTool.Services.Models.Input.OrganizationRequest;
using COOLTool.Services;
using Newtonsoft.Json;

namespace NavyCOOLUnitTesting
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public bool PublishCredential()
		{
			bool isValid = true;
			string publisherApiKey = "157ce70f-2020-0216-8b02-ad549a332115";
			RequestHelper helper = new RequestHelper();
			var request = new CredentialRequest();
			//get and populate an agency DTO

			var input = SampleData.Get_AircraftDispatcher_As_ARTT();

			request.Credential = input;
			request.OwningOrganizationCTID = input.CTID;
			request.Community = "navy";
			helper.ApiKey = publisherApiKey;

			var payload = new CredentialServices().Publish( request, helper );


			return isValid;
		}

		[TestMethod]
		public bool PublishOrganization()
		{
			bool isValid = true;
			RequestHelper helper = new RequestHelper();
			string publisherApiKey = "157ce70f-2020-0216-8b02-ad549a332115";
			var request = new COOLTool.Services.Models.Input.OrganizationRequest();
			//get and populate an agency DTO

			ThisEntity input = SampleData.Get_FAA();

			request.Agency = input;
			request.OwningOrganizationCTID = input.CTID;
			request.Community = "navy";
			helper.ApiKey = publisherApiKey;

			var payload = new OrganizationServices().Publish( request, helper );


			return isValid;
		}

	}
}
