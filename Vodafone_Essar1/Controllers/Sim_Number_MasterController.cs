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
    public class Sim_Number_MasterController : Controller
    {
        [Filters.AuthorizeAdmin]

        public ActionResult Index()
        {
            return View();
        }

        Training db = new Training();
        public JsonResult GetSimNumberLists(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)  //Gets the todo Lists.
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Sim_no_lists = db.SimNumberLists.Select(
                    a => new
                    {
                        a.Sim_no,
                        a.status


                    });
            if (_search)
            {
                switch (searchField)
                {
                    case "Sim_no":
                        Sim_no_lists = Sim_no_lists.Where(t => t.Sim_no.Contains(searchString));
                        break;

                    case "status":
                        Sim_no_lists = Sim_no_lists.Where(t => t.status.ToString().Contains(searchString));
                        break;

                }
            } 
            int totalRecords = Sim_no_lists.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                Sim_no_lists = Sim_no_lists.OrderByDescending(s => s.Sim_no);
                Sim_no_lists = Sim_no_lists.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                Sim_no_lists = Sim_no_lists.OrderBy(s => s.Sim_no);
                Sim_no_lists = Sim_no_lists.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Sim_no_lists
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // TODO:insert a new row to the grid logic here
        [HttpPost]
        public string Create(Sim_Numbers objTodo)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.SimNumberLists.Add(objTodo);
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
        public string Edit(Sim_Numbers objTodo)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(objTodo).State = EntityState.Modified;
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
        public string Delete(string ID)
        {
            Sim_Numbers todolist = db.SimNumberLists.Find(ID);
            db.SimNumberLists.Remove(todolist);
            db.SaveChanges();
            return "Deleted successfully";
        }
    }


}

