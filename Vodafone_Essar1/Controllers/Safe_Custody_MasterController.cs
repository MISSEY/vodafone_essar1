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
    public class Safe_Custody_MasterController : Controller
    {
        [Filters.AuthorizeAdmin]
      
        public ActionResult Index()
        {
            return View();
        }

        Training db = new Training();
        public JsonResult GetSafeCustodyLists(string sidx, string sord, int page, int rows, bool _search, string searchField, string searchOper, string searchString)  //Gets the todo Lists.
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            var  Mobile_no_Lists = db.MobileNumberLists.Select(
                    a => new
                    {
                        a.Mobile_no,
                        a.status
                        
                        
                    });
            if (_search)
            {
                switch (searchField)
                {
                    case "Mobile_no":
                        Mobile_no_Lists = Mobile_no_Lists.Where(t => t.Mobile_no.Contains(searchString));
                        break;

                    case "status":
                        Mobile_no_Lists = Mobile_no_Lists.Where(t => t.status.ToString().Contains(searchString));
                        break;

                }
            } 
            int totalRecords = Mobile_no_Lists.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
            if (sord.ToUpper() == "DESC")
            {
                Mobile_no_Lists = Mobile_no_Lists.OrderByDescending(s => s.Mobile_no);
                Mobile_no_Lists = Mobile_no_Lists.Skip(pageIndex * pageSize).Take(pageSize);
            }
            else
            {
                Mobile_no_Lists = Mobile_no_Lists.OrderBy(s => s.Mobile_no);
                Mobile_no_Lists = Mobile_no_Lists.Skip(pageIndex * pageSize).Take(pageSize);
            }
            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = Mobile_no_Lists
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // TODO:insert a new row to the grid logic here
        [HttpPost]
        public string Create( Mobile_numbers objTodo)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    db.MobileNumberLists.Add(objTodo);
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
        public string Edit(Mobile_numbers objTodo)
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
            Mobile_numbers todolist = db.MobileNumberLists.Find(ID);
            db.MobileNumberLists.Remove(todolist);
            db.SaveChanges();
            return "Deleted successfully";
        }
    }


}

