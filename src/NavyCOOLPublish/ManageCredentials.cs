using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NavyCOOLPublish.Models;
using COOLTool.Services;
using COOL.Factories;
using Utilities;

namespace NavyCOOLPublish
{
	public class ManageCredentials : BaseServices
	{
		public void Publish()
		{
			RequestParameters parms = new RequestParameters( 1 );
			SetParameters( parms );
			//RegistryServices mgr = new RegistryServices();
			//set to null to skip history method
			//List<SiteActivity> history = null; // new List<SiteActivity>();
			//RegistryPublishManager rpMgr = new RegistryPublishManager();

			//bool doingPublish = UtilityManager.GetAppKeyValue( "credDoingPublish", false );
			//bool doingGenerate = UtilityManager.GetAppKeyValue( "credDoingGenerate", false );


			//string pOrderBy = "newest";
			//string filter = "";

			//string payloadPrefix = GetPayloadFilePrefix();

			if( !parms.DoingPublish && !parms.DoingGenerate )
			{
				DisplayMessages( "CredentialPublishing - NO ACTION REQUESTS ENDING" );
				return;
			}

			string statusMessage = "";
			//AppUser user = GetDefaultUser();

			DisplayMessages( "Starting credentials, with filter: \r\n" + parms.Filter );

			string publisherApiKey = "";//TBD get from somewhere
			int pTotalRows = 0;
			List<string> messages = new List<string>();
			//do search with minimum results (autocomplete = true)
			var list = CredentialManager.Search( parms.Filter, parms.OrderBy, parms.PageNumber, parms.PageSize, ref pTotalRows );

			{
				int cntr = 0;
				foreach( var item in list )
				{
					cntr++;
					messages = new List<string>();
					statusMessage = "";
					DisplayMessages( string.Format( " {0}.	Credential: {1} ({2})", cntr, item.CE_CertTitle, item.CE_CertID ) );
					if( parms.DoingPublish )
					{
						if( parms.DoingDeleteBeforePublish )
						{
							//if( !mgr.Unregister_Credential( item.Id, user, ref statusMessage, ref history ) )
							//{
							//	DisplayMessages( "                           - UNREGISTER FAILED: " + statusMessage );
							//}
						}
						//Note that will target the Navy community
						if( ! new PublishCredential().Publish( item.CE_CertID, publisherApiKey, ref messages, "navy" ) )
						{
							DisplayMessages( "                           - FAILED: " + statusMessage );
							LoggingHelper.LogError( string.Format( "{0} #:{1} Publish failed: {2} ", parms.EntityType, item.CE_CertID, statusMessage ), false );
						}
				
					}

					//payload is output in publish, so skip if doing publish
					//if( parms.DoingGenerate & !parms.DoingPublish )
					//	CredentialFormat( item.Id, user, parms.PayloadPrefix );
				}
				if( cntr > 0 )
				{
					DisplayMessages( string.Format( "_____________________ Processed {0} Credentials _____________________", cntr ) );
				}
			}


		}
	}
}
