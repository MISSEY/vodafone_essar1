using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vodafone_Essar1.Models;
using Vodafone_Essar1.ViewModels;
using System.Web.Script.Serialization;

namespace Vodafone_Essar1.Controllers
{
    public class Essar_EmployeeController : Controller
    {
        //
        // GET: /Essar_Employee/

        public ActionResult Index()
        {
            return View();
        }

        public void GetEmployee(string Mobile_no){
            try
            {

                EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
                List<UsersBills_View> empViewModels = new List<UsersBills_View>();

                List<Essar_Users_Bill> employees;
                EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();

                employees = empBal.GetUsersBill(Mobile_no);

                foreach (Essar_Users_Bill emp in employees)
                {
                    UsersBills_View empViewModel = new UsersBills_View();
                    empViewModel.Sr_no = emp.Sr_no;
                    empViewModel.Mobile_no = emp.Mobile_no;
                    empViewModel.Months = emp.Months;
                    empViewModel.Year = emp.Year;
                    empViewModel.Sub_total = emp.Sub_total;
                    empViewModel.GST_18 = emp.GST_18;
                    empViewModel.Limit = emp.Limit;
                    empViewModel.Total = emp.Total;
                    empViewModel.Deduction = emp.Deduction;
                    empViewModels.Add(empViewModel);
                }
                employeeListViewModel.Employees_Bills = empViewModels;
                var js = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue };
                Response.Write(js.Serialize(empViewModels));
            }
            catch (Exception e)
            {
            }
        }

    }
}
