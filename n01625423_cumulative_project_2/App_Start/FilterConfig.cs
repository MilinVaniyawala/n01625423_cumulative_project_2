using System.Web;
using System.Web.Mvc;

namespace n01625423_cumulative_project_2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
