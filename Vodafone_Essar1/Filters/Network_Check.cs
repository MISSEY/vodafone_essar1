using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vodafone_Essar1.Data_Access_Layer;

namespace Vodafone_Essar1.Filters
{
    public class Network_Check : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        bool networkUp=System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            if (!networkUp)
            {
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                    {
                {"Controller","Employee"},
                {"Action","NoInternet"}

              });

                
            }
            base.OnActionExecuting(filterContext);
        }
    }
}