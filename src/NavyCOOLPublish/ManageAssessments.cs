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
	public class ManageAssessments : BaseServices
	{
		/// <summary>
		/// Handle publishing
		/// </summary>
		/// <param name="publisherApiKey">Typically retrieved from a database or as an appKey</param>
		public void Publish(string publisherApiKey)
		{
			RequestParameters parms = new RequestParameters( 3 );
			SetParameters( parms );
			var community = UtilityManager.GetAppKeyValue( "requestedCommunity" );

			//string payloadPrefix = GetPayloadFilePrefix();

			if ( !parms.DoingPublish && !parms.DoingGenerate )
			{
				DisplayMessages( "AssessmentPublishing - NO ACTION REQUESTS ENDING" );
				return;
			}

			string statusMessage = "";
			//AppUser user = GetDefaultUser();

			DisplayMessages( "Starting Assessments, with filter: \r\n" + parms.Filter );

			int pTotalRows = 0;
			List<string> messages = new List<string>();
			//do search with minimum results (autocomplete = true)
			var list = AssessmentManager.Search( parms.Filter, parms.OrderBy, parms.PageNumber, parms.PageSize, ref pTotalRows );
			if ( list != null && list.Count() > 0 )
			{
				int cntr = 0;
				foreach ( var item in list )
				{
					cntr++;
					messages = new List<string>();
					statusMessage = "";
					DisplayMessages( string.Format( " {0}.	Assessment: {1} ({2})", cntr, item.CE_Title, item.CE_ID ) );
					if ( parms.DoingPublish )
					{
						//Note that will target the Navy community
						if ( !new PublishAssessment().Publish( item.CE_ID, publisherApiKey, ref messages, community ) )
						{
							DisplayMessages( "                           - FAILED: " + statusMessage );
							LoggingHelper.LogError( string.Format( "{0} #:{1} Publish failed: {2} ", parms.EntityType, item.CA_AgencyID, statusMessage ), false );
						}

					}

					//payload is output in publish, so skip if doing publish
					//if( parms.DoingGenerate & !parms.DoingPublish )
					//	AssessmentFormat( item.Id, user, parms.PayloadPrefix );
				}
				if ( cntr > 0 )
				{
					DisplayMessages( string.Format( "_____________________ Processed {0} Assessments _____________________", cntr ) );
				}
			}


		}
	}
}
