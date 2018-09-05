using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Vodafone_Essar1.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class BaseController : Controller
    {


       public BaseController()
        {
            try
            {

                if (Session["UserID"] == null)
                {

                    Index();
                }
            }
            catch (System.NullReferenceException e)
            {
                
            }
           

         
        }
       public ActionResult Index()
       {
           return RedirectToAction("Index", "Home");
       }

    }
}
