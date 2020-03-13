using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NavyCOOLPublish.Models;
using Utilities;

namespace NavyCOOLPublish
{
	public class BaseServices
	{
		static string latestFilter = "(StatusId <= 3 AND len(IsNull(CTID,'')) = 39 AND IsNull(CredentialRegistryId,'') = '') ";
		static string latestFilterPublished = "(StatusId <= 3 AND len([CredentialRegistryId]) = 36) ";
		static int pageNumber = 1;
		static int pageSize = 100;
		static int pTotalRows = 0;
		static bool isValid = true;
		static List<string> messages = new List<string>();


		public static void SetParameters(RequestParameters parms)
		{
			//
			var requestedCommunity = UtilityManager.GetAppKeyValue( "requestedCommunity", "" );
			if( !string.IsNullOrWhiteSpace( requestedCommunity ) && UtilityManager.GetAppKeyValue( "defaultCommunity", "" ) != requestedCommunity )
				parms.Community = requestedCommunity;
			//requestCommunity

			string publishSource = UtilityManager.GetAppKeyValue( parms.SetAppKey( "PublishSource" ), "latest" );
			parms.DoingPublish = UtilityManager.GetAppKeyValue( parms.SetAppKey( "DoingPublish" ), false );
			parms.DoingPublishSync = UtilityManager.GetAppKeyValue( parms.SetAppKey( "DoingPublishAsync" ), false, false );
			parms.DoingDeleteBeforePublish = UtilityManager.GetAppKeyValue( parms.SetAppKey( "DeleteBeforePublish" ), false );
			//
			parms.DoingGenerate = UtilityManager.GetAppKeyValue( parms.SetAppKey( "DoingGenerate" ), false );
			string defaultOrderBy = UtilityManager.GetAppKeyValue( "defaultOrderBy", "newest" );
			defaultOrderBy = string.IsNullOrWhiteSpace( defaultOrderBy ) ? "newest" : defaultOrderBy;

			parms.PageSize = UtilityManager.GetAppKeyValue( parms.SetAppKey( "ProcessCount" ), 50 );
			parms.OrderBy = UtilityManager.GetAppKeyValue( parms.SetAppKey( "OrderBy" ), defaultOrderBy );

			if( publishSource == "latest" )
			{
				parms.Filter = latestFilter;
				parms.OrderBy = "newest";
			}
			else if( publishSource == "custom" )
			{
				parms.Filter = UtilityManager.GetAppKeyValue( parms.SetAppKey( "Sql" ), "latest" );
				parms.OrderBy = parms.OrderBy == defaultOrderBy ? defaultOrderBy : parms.OrderBy;
			}
			else if( publishSource == "list" )
			{
				//this could just be sql as well
				string idList = UtilityManager.GetAppKeyValue( parms.SetAppKey( "IdList" ), "" );
				parms.Filter = string.Format( " (StatusId < 4 AND len(IsNull(base.CTID,'')) = 39 AND base.Id in ({0}) )", idList );
				parms.OrderBy = defaultOrderBy;
			}
			else
			{
				parms.Filter = latestFilter;
				parms.OrderBy = defaultOrderBy;
			}
			

			parms.PayloadPrefix = GetPayloadFilePrefix();
		}

		public static string GetPayloadFilePrefix()
		{
			string fixedPayloadPrefix = UtilityManager.GetAppKeyValue( "payloadFilePrefix" );
			if( string.IsNullOrWhiteSpace( fixedPayloadPrefix ) )
				return "";
			else
				return fixedPayloadPrefix;
		}

		public static string DisplayMessages(string message, bool loggingError = false)
		{
			LoggingHelper.DoTrace( 1, message );
			//Console.WriteLine( message );

			return message;
		}
	}
}
