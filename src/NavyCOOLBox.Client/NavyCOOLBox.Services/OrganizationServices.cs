using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NavyCOOLBox.Services;
using NavyCOOLBox.Models.Publishing;


namespace NavyCOOLBox.Services
{
	public class OrganizationServices
	{
		//test method
		public bool Publish(int agencyId, string publisherApiKey, ref List<string> messages, string community = "")
		{
			bool isValid = true;
		
			var request = new OrganizationRequest();
			//get and populate an agency DTO

			var input = SampleData.Get_FAA();

			request.Agency = input;
			request.OwningOrganizationCTID = input.CTID;
			request.Community = community;
			//serialize and call 
			string coolInput = JsonConvert.SerializeObject( input, PublishingServices.GetJsonSettings() );
			AssistantRequestHelper arh = new AssistantRequestHelper()
			{
				OrganizationApiKey = publisherApiKey,
				CTID = request.OwningOrganizationCTID,
				InputPayload = coolInput,
				EndpointType = "organization"
			};


			isValid = new PublishingServices().PublishRequest( arh );

			return isValid;
		}

	}
}
