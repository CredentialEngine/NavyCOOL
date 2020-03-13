using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NavyCOOLPublish.Models;
using NavyCOOLBox.Services;
using NavyCOOLBox.Factories;
using Utilities;

namespace NavyCOOLPublish
{
	public class ManageOrganizations : BaseServices
	{
		/// <summary>
		/// Handle publishing
		/// </summary>
		/// <param name="publisherApiKey">Typically retrieved from a database or as an appKey</param>
		public void Publish( string publisherApiKey )
		{
			RequestParameters parms = new RequestParameters( 2 );
			SetParameters( parms );
			var community = UtilityManager.GetAppKeyValue( "requestedCommunity" );

			//string payloadPrefix = GetPayloadFilePrefix();

			if ( !parms.DoingPublish && !parms.DoingGenerate )
			{
				DisplayMessages( "OrganizationPublishing - NO ACTION REQUESTS ENDING" );
				return;
			}

			string statusMessage = "";
			//AppUser user = GetDefaultUser();

			DisplayMessages( "Starting Organizations, with filter: \r\n" + parms.Filter );

			int pTotalRows = 0;
			List<string> messages = new List<string>();
			//do search with minimum results (autocomplete = true)
			var list = OrganizationManager.Search( parms.Filter, parms.OrderBy, parms.PageNumber, parms.PageSize, ref pTotalRows );
			if ( list != null && list.Count() > 0 )
			{
				int cntr = 0;
				foreach ( var item in list )
				{
					cntr++;
					messages = new List<string>();
					statusMessage = "";
					DisplayMessages( string.Format( " {0}.	Organization: {1} ({2})", cntr, item.CA_AgencyName, item.CA_AgencyID ) );
					if ( parms.DoingPublish )
					{
						//Note that will target the Navy community
						if ( !new OrganizationServices().Publish( item.CA_AgencyID, publisherApiKey, ref messages, community ) )
						{
							DisplayMessages( "                           - FAILED: " + statusMessage );
							LoggingHelper.LogError( string.Format( "{0} #:{1} Publish failed: {2} ", parms.EntityType, item.CA_AgencyID, statusMessage ), false );
						}

					}

					//payload is output in publish, so skip if doing publish
					//if( parms.DoingGenerate & !parms.DoingPublish )
					//	OrganizationFormat( item.Id, user, parms.PayloadPrefix );
				}
				if ( cntr > 0 )
				{
					DisplayMessages( string.Format( "_____________________ Processed {0} Organizations _____________________", cntr ) );
				}
			}


		}
	}
}
