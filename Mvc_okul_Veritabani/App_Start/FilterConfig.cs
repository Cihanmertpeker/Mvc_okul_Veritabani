using System.Web;
using System.Web.Mvc;

namespace Mvc_okul_Veritabani
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
