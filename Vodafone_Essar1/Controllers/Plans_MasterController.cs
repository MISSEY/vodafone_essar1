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
    public class Plans_MasterController : Controller
    {
        //
        // GET: /Plans_Master/
        [Filters.AuthorizeAdmin]
        public ActionResult Index()
        {
            return View();
        }

        Training db = new Training();
        public JsonResult GetPlanLists(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)  //Gets the todo Lists.
        {

            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var  PlanListsResults = db.Plans_Lists.Select(
                    a => new
                    {
                        a.Level,
                        a.Plan,
                        a.Limit,
                        a.Descrp
                        
                    });
            if (_search)
            {
                switch (searchField)
                {
                    case "Level":
                        PlanListsResults = PlanListsResults.Where(t => t.Level.Contains(searchString));
                        break;
                    case "Plan":
                        PlanListsResults = PlanListsResults.Where(t => t.Plan.Contains(searchString));
                        break;
                    case "Limit":
                        PlanListsResults = PlanListsResults.Where(t => t.Limit.ToString().Contains(searchString));
                        break;
                    case "Descrp":
                        PlanListsResults = PlanListsResults.Where(t => t.Descrp.Contains(searchString));
                        break;
                }
            }  
            int totalRecords = PlanListsResults.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                PlanListsResults = PlanListsResults.OrderByDescending(s => s.Level);
                PlanListsResults = PlanListsResults.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                PlanListsResults = PlanListsResults.OrderBy(s => s.Level);
                PlanListsResults = PlanListsResults.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = PlanListsResults
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // TODO:insert a new row to the grid logic here
        [HttpPost]
        public string Create( Plans objTodo)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Plans_Lists.Add(objTodo);
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
        public string Edit(Plans objTodo)
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
            Plans todolist = db.Plans_Lists.Find(ID);
            db.Plans_Lists.Remove(todolist);
            db.SaveChanges();
            return "Deleted successfully";
        }
    }
}
