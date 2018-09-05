using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vodafone_Essar1.Data_Access_Layer;
using System.Web.SessionState; 

using Vodafone_Essar1.Models;
using Vodafone_Essar1.Login_Business_Layer;
namespace Vodafone_Essar1.Controllers
{
    [SessionState(SessionStateBehavior.Default)]   
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {

                return RedirectToAction("Index2", "Employee");
            }
            else
            {
                return View();
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserCredential objUser)
        {
          
            try
                {
            if (ModelState.IsValid)
            {
               
                 bool isValid = true;

                    using (Training db = new Training())
                    {
                        var obj = db.UserCredentials.Where(a => a.UserName.Equals(objUser.UserName) && a.password.Equals(objUser.password)).FirstOrDefault();
                        isValid = UserAccessBLL.IsAuthenticated(objUser.UserName, objUser.password);
                        if (obj != null)
                        {
                            Session["UserID"] = obj.UserId.ToString();
                            Session["UserName"] = obj.UserName.ToString();
                            return RedirectToAction("UserDashBoard");
                        }
                        else if(isValid){

                            int a=ValidateUser(objUser.UserName);
                            if (a == 1)
                            {
                                return RedirectToAction("UserDashBoard");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Essar_Employee");
                            }
                            

                        }
                        else
                        {
                            ViewBag.msg = "Invalid Credentials";
                        }
                    }
                }
               
            }
            catch (Exception e)
            {
                ViewBag.msg = "Connection Not Found";

            }
            return View();
        }

        int ValidateUser(string sUserID)
        {
            UserAccess objUserAccess = default(UserAccess);

            try
            {

                objUserAccess = UserAccessBLL.GetUserAcessDetail(sUserID);
                if (objUserAccess.RoleID != null)
                {


                    Session["UserID"] = objUserAccess.UserID;
                    Session["UserName"] = objUserAccess.UserName;
                    Session["RoleID"] = objUserAccess.RoleID;

                    return 1;

                }
                else
                {
                    return 0;
                }
             
            }
            catch (Exception objEx)
            {

            }

            return 0;
        }
        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Index2","Employee");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult logout()
        {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Index2","Employee");
      
        }

    }
}