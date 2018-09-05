using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vodafone_Essar1.Data_Access_Layer;
using Vodafone_Essar1.Models;
using System.Data;
using System.Data.Entity;
using System.Web.SessionState;


namespace Vodafone_Essar1.Controllers
{
     [Filters.NoCache]
    public class Essar_Users_MasterController : Controller
    {
         EmployeeBusinessLayer eb = new EmployeeBusinessLayer();

        [Filters.AuthorizeAdmin]

        public ActionResult Index()
        {
            return View();
        }

        Training db = new Training();
        public JsonResult GetEssarUsers(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)  //Gets the todo Lists.
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Essar_users_lists = db.Employees.Select(
                    a => new
                    {
                        a.Sr_no,
                        a.Employees_Name,
                        a.SAP_ID,
                        a.Mobile_no,
                        a.Sim_no,
                        a.email_id,
                        a.Levels, 
                        a.Limit,
                        a.Billed_To,
                        a.Department,
                        a.Scheme,
                        a.status
 

                    });
            if (_search)
            {
                switch (searchField)
                {
                    case "Employees_Name":
                        Essar_users_lists = Essar_users_lists.Where(t => t.Employees_Name.Contains(searchString));
                        break;

                    case "SAP_ID":
                        Essar_users_lists = Essar_users_lists.Where(t => t.SAP_ID.Contains(searchString));
                        break;

                    case "Mobile_no":
                        Essar_users_lists = Essar_users_lists.Where(t => t.Mobile_no.Contains(searchString));
                        break;

                    case "Sim_no":
                        Essar_users_lists = Essar_users_lists.Where(t => t.Sim_no.Contains(searchString));
                        break;

                    case "email_id":
                        Essar_users_lists = Essar_users_lists.Where(t => t.email_id.Contains(searchString));
                        break;

                    case "Levels":
                        Essar_users_lists = Essar_users_lists.Where(t => t.Levels.Contains(searchString));
                        break;

                    case "Billed_To":
                        Essar_users_lists = Essar_users_lists.Where(t => t.Billed_To.Contains(searchString));
                        break;

                    case "Department":
                        Essar_users_lists = Essar_users_lists.Where(t => t.Department.Contains(searchString));
                        break;

                    case "Scheme":
                        Essar_users_lists = Essar_users_lists.Where(t => t.Scheme.Contains(searchString));
                        break;

                    case "status":
                        Essar_users_lists = Essar_users_lists.Where(t => t.Scheme.Contains(searchString));
                        break;

                }
            } 
            int totalRecords = Essar_users_lists.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                Essar_users_lists = Essar_users_lists.OrderByDescending(s => s.Employees_Name);
                Essar_users_lists = Essar_users_lists.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                Essar_users_lists = Essar_users_lists.OrderBy(s => s.Employees_Name);
                Essar_users_lists = Essar_users_lists.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Essar_users_lists
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // TODO:insert a new row to the grid logic here
        [HttpPost]
        public string Create([Bind(Exclude = "Sr_no")]Employee objTodo)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Employees.Add(objTodo);
                    db.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfull";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;

            }
            return msg;
        }
        public string Edit(Employee objTodo)
        {
            string msg;
            EditEssarBusinessLayer eeb = new EditEssarBusinessLayer();
            try
            {
                if (ModelState.IsValid)
                {
                    string sapid = eb.SapID(objTodo.Sr_no);
                    if (!sapid.Equals(objTodo.SAP_ID))
                    {
                        if (eeb.UpdateSapId(sapid, objTodo))
                        {
                            msg = "Saved Successfully";
                        }
                        else
                        {
                            msg = "Unable to save changes";
                        }
                    }
                    else
                    {
                        db.Entry(objTodo).State = EntityState.Modified;
                        db.SaveChanges();
                        msg = "Saved Successfully";
                    }
                  
                   
                }
                else
                {
                    msg = "Validation data not successfull";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }
        public string Delete(int ID)
        {
            Employee todolist =new Employee();
            using (var ctx = new Training())
            {
                var employeelist = ctx.Database.ExecuteSqlCommand("Delete from Essar_users1 where Sr_no=" + ID + ";");
            }
           
            return "Deleted successfully";
        }
    }
}
