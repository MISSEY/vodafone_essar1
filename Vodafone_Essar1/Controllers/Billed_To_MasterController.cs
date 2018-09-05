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
    public class Billed_To_MasterController : Controller
    {
        [Filters.AuthorizeAdmin]

        public ActionResult Index()
        {
            return View();
        }

        Training db = new Training();
        public JsonResult GetBilledToLists(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)  //Gets the todo Lists.
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var Billed_To_Lists = db.Billed_To_Lists.Select(
                    a => new
                    {
                        a.Billed_To
                       


                    });
            if (_search)
            {
                switch (searchField)
                {
                    case "Billed_To":
                        Billed_To_Lists = Billed_To_Lists.Where(t => t.Billed_To.Contains(searchString));
                        break;
                    
                }
            }  
            int totalRecords = Billed_To_Lists.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                Billed_To_Lists = Billed_To_Lists.OrderByDescending(s => s.Billed_To);
                Billed_To_Lists = Billed_To_Lists.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                Billed_To_Lists = Billed_To_Lists.OrderBy(s => s.Billed_To);
                Billed_To_Lists = Billed_To_Lists.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Billed_To_Lists
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // TODO:insert a new row to the grid logic here
        [HttpPost]
        public string Create(Billed_to objTodo)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Billed_To_Lists.Add(objTodo);
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
        public string Edit(Billed_to objTodo)
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
            Billed_to todolist = db.Billed_To_Lists.Find(ID);
            db.Billed_To_Lists.Remove(todolist);
            db.SaveChanges();
            return "Deleted successfully";
        }
    }


}
