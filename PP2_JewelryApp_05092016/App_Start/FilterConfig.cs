using System.Web;
using System.Web.Mvc;

namespace PP2_JewelryApp_05092016
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
