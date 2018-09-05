using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vodafone_Essar1.Data_Access_Layer;
using Vodafone_Essar1.Models;
using Vodafone_Essar1.ViewModels;
using System.Web.SessionState;
using System.Web.Script.Serialization;
namespace Vodafone_Essar1.Controllers
{
    [Filters.NoCache]
    [Filters.AuthorizeAdmin]
    public class ReportsController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult New_Sim_Activation()
        {
            ViewBag.Title = "New Sim Activation Lists";
            return View("Index");
        }

        public ActionResult Deactivation()
        {
            ViewBag.Title = "Deactivation Lists";
            return View("Index");
        }

        public ActionResult Sim_Replacement()
        {
            ViewBag.Title = "Sim Replacement Lists";
            return View("Index");
        }

        public ActionResult Reactivation_numbers()
        {
            ViewBag.Title = "Reactivation number Lists";
            return View("Index");
        }
        public ActionResult Plan_Change()
        {
            ViewBag.Title = "Plan Change Lists";
            return View("Index");
        }

       
         public void GetReports(string from, string to,string Type)
        {

                string to1 = to+" 23:59:59";

                EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
                List<Reports_ViewModels> empViewModels = new List<Reports_ViewModels>();
                List<Reports> employees;


                employees = empBal.GetReports(from, to1,Type);
                foreach (Reports emp in employees)
                {
                    Reports_ViewModels empViewModel = new Reports_ViewModels();
                    empViewModel.Sr_no = emp.Sr_no;
                    empViewModel.Date = emp.Date.ToString();
                    empViewModel.Employees_Name = emp.Employees_Name;
                    empViewModel.SAP_ID = emp.SAP_ID;
                    empViewModel.Levels = emp.Levels;
                    empViewModel.Mobile_no = emp.Mobile_no;
                    empViewModel.Sim_no = emp.Sim_no;
                    empViewModel.Department = emp.Department;
                    empViewModel.Reason = emp.Reason;
                    empViewModels.Add(empViewModel);
                }
             
                var js = new JavaScriptSerializer();
                Response.Write(js.Serialize(empViewModels));
            

            }
        }

    
}
