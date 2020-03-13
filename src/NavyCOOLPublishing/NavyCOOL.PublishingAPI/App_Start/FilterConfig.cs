using System.Web;
using System.Web.Mvc;

namespace NavyCOOL.PublishingAPI
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add( new HandleErrorAttribute() );
		}
	}
}
