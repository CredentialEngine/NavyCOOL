using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavyCOOLBox.Models.Publishing
{
	public class CredentialRequest : BaseRequest
	{
		public CredentialRequest()
		{
			Credential = new ARTTCredential();
		}

		/// <summary>
		/// Organization Input Class
		/// </summary>
		public ARTTCredential Credential { get; set; }

	}
}
