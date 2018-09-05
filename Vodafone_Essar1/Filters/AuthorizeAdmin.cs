using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vodafone_Essar1.Filters
{
    public class AuthorizeAdmin:System.Web.Mvc.ActionFilterAttribute,System.Web.Mvc.IActionFilter

    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["UserID"]==null)
            {
            
            filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
            {
                {"Controller","Employee"},
                 {"Action","logout"}

            });
            }
            base.OnActionExecuting(filterContext);
        }

    }
}