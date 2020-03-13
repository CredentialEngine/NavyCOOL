using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavyCOOLBox.Models.Publishing
{
	public class OrganizationRequest : BaseRequest
	{
		public OrganizationRequest()
		{
			Agency = new Agency();
		}

		/// <summary>
		/// Organization Input Class
		/// </summary>
		public Agency Agency { get; set; }

	}
}
