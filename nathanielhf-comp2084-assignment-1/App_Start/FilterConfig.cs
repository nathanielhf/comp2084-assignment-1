using System.Web;
using System.Web.Mvc;

namespace nathanielhf_comp2084_assignment_1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
