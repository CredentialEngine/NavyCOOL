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
			//set the related appKeys to true to request the related publishing
			string publisherApiKey = UtilityManager.GetAppKeyValue( "ourApiKey" );
			if ( UtilityManager.GetAppKeyValue( "credDoingPublish", false ))
			{
				Console.WriteLine( "Calling Credentials Publish" );
				new ManageCredentials().Publish( publisherApiKey );
			}

			if ( UtilityManager.GetAppKeyValue( "orgDoingPublish", false ) )
			{
				Console.WriteLine( "Calling Organizations Publish" );
				new ManageOrganizations().Publish( publisherApiKey );
			}

			if ( UtilityManager.GetAppKeyValue( "asmtDoingPublish", false ) )
			{
				Console.WriteLine( "Calling Assessments Publish" );
				new ManageAssessments().Publish( publisherApiKey );
			}


		}
	}
}
