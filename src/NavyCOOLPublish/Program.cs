using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Utilities;

namespace NavyCOOLPublish
{
	public class Program
	{
		static void Main(string[] args)
		{
			//these can be appKeys eventually
			bool publishingCredentials = false;
			bool publishingOrganizations = false;
			bool publishingAssessments = false;

			if( publishingCredentials )
			{
				Console.WriteLine( "Calling PublishCredentials" );
				new ManageCredentials().Publish();
			}

			if( publishingOrganizations )
			{
				Console.WriteLine( "Calling PublishOrganization" );
				new ManageOrganizations().Publish();
			}

			//if( publishingAssessments )
			//{
			//	Console.WriteLine( "Calling PublishAssessments" );
			//	new PublishAssessments().Publish();
			//}


		}
	}
}
