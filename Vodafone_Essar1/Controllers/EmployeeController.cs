using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using Vodafone_Essar1.Models;
using Vodafone_Essar1.ViewModels;
using Vodafone_Essar1.Data_Access_Layer;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using Vodafone_Essar1.Utility;




namespace Vodafone_Essar1.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class EmployeeController : Controller
    {

        EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
        string[] m = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        SqlConnection con = new SqlConnection("Data Source=150.0.150.17;Initial Catalog=Training;Persist Security Info=True;User ID=TRAINEE;Password=TRAINEE");
        OleDbConnection Econ;
        DataSet ds = new DataSet();
        DataTable dt;
        string error_msg = null;
        int counter = 0;

        public ActionResult Index2()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index","Home");
            }
            
                return View();

        }


        private void ExcelConn(string filepath)
        {
            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", filepath);

            Econ = new OleDbConnection(constr);
        }


        [Filters.AuthorizeAdmin]
        [HttpPost]
        public ActionResult Index2(HttpPostedFileBase file,string upload_type)
        {
            try
            {
                string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);

                string filepath = "/excelfolder/" + filename;


                file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));

                InsertExceldata(filepath, filename, upload_type);
                
            }
            catch (Exception e)
            {
            }
            return View("Index2");
        }


        private void InsertExceldata(string fileepath, string filename,string upload_type)
        {
            try
            {
                string fullpath = Server.MapPath("/excelfolder/") + filename;

                ExcelConn(fullpath);





                SqlBulkCopy objbulk = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.CheckConstraints);

                if (upload_type.Equals("1"))
                {
                    Sim(objbulk);

                }
                else
                {
                    Mobile(objbulk);

                }

                if (counter == 0)
                {
                    con.Open();

                    objbulk.WriteToServer(dt);

                    con.Close();
                    if (error_msg == null)
                    {
                        ViewBag.Error_SimandMobile = "Successfully Uploaded";
                    }
                }

              
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                error_msg = e.Message.ToString();
                string[] e1 = error_msg.Split('.');

                if (error_msg.Contains("Violation of PRIMARY KEY constraint"))
                {

                    ViewBag.Error_SimandMobile = "Number Already Exist, Duplicate Enrty should be removed"+e1[3];
                }
            }
            catch (System.Data.OleDb.OleDbException e)
            {
                error_msg = e.Message.ToString();
                if (error_msg.Contains("No value given for one or more required parameters"))
                {

                    ViewBag.Error_SimandMobile = "Please Upload Correct File";
                }
            }
        }



        void Sim(SqlBulkCopy objbulk)
        {
            string query = string.Format("Select [Sim No] from [{0}]", "Sheet1$");

            OleDbCommand Ecom = new OleDbCommand(query, Econ);

            Econ.Open();

            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);

            Econ.Close();

            oda.Fill(ds);

            dt = ds.Tables[0];
            dt.Columns.Add("Status", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                row["Status"] = "0";
                string sim_no = row["Sim No"].ToString();

                if (sim_no.Equals("") || sim_no.Contains(" "))
                {

                    counter = 1;
                    ViewBag.Error_SimandMobile = "Sim Number Should be trimed";
                    break;

                }
            }
           
            objbulk.DestinationTableName = "Essar_users_Sim_Numbers";

            objbulk.ColumnMappings.Add("Sim No", "Sim_no");

            objbulk.ColumnMappings.Add("Status", "status");
        }

        void Mobile(SqlBulkCopy objbulk)
        {
            string query = string.Format("Select [Mobile No] from [{0}]", "Sheet1$");

            OleDbCommand Ecom = new OleDbCommand(query, Econ);

            Econ.Open();

            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);

            Econ.Close();

            oda.Fill(ds);
            dt = ds.Tables[0];
            dt.Columns.Add("Status", typeof(string));
            foreach (DataRow row in dt.Rows)
            {
                row["Status"] = "0";
                string mobile_no = row["Mobile No"].ToString();

                if (mobile_no.Equals("") || mobile_no.Contains(" "))
                {

                    counter = 1;
                    ViewBag.Error_SimandMobile = "Mobile Number Should be trimed";
                    break;

                }
            }

            objbulk.DestinationTableName = "Essar_users_Mobile_no";

            objbulk.ColumnMappings.Add("Mobile No", "Mobile_no");

            objbulk.ColumnMappings.Add("Status", "status");
        }




        [Filters.AuthorizeAdmin]
        public IEnumerable<MobileNumbers> GetMobile()
        {
         

            List<MobileNumbers> listItem = new List<MobileNumbers>();
            List<Mobile_numbers> Mobile_numbers = new List<Mobile_numbers>();

            EmployeeViewModel MobileListView = new EmployeeViewModel();


                try
                {

                    Mobile_numbers = empBal.Get_Mobile_Numbers();



                     foreach (Mobile_numbers m in Mobile_numbers)
                    {
                        MobileNumbers mobile_numbers = new MobileNumbers();
                        mobile_numbers.Mobile_no = m.Mobile_no;
                        listItem.Add(mobile_numbers);
                    }
                }
                catch (Exception e)
                {
                    var s = e.GetBaseException();
                }
                

                MobileListView.MobilenumberList = listItem;
                return listItem;
            
        }

        public IEnumerable<Plans_View_Model> GetScheme()
        {
            List<Plans_View_Model> listItem = new List<Plans_View_Model>();
            List<string> Scheme = new List<string>();
            EmployeeViewModel plan_list = new EmployeeViewModel();
            try
            {
 
                    Scheme = empBal.Get_Scheme(); 
                    foreach (var s in Scheme)
                    {
                        Plans_View_Model plans = new Plans_View_Model();
                        plans.Plan = s;
                        listItem.Add(plans);
                    }
                
            }
            catch (Exception e)
            {
                
            }
                plan_list.Plans_List_View = listItem;
                return listItem;
        }

        public IEnumerable<Plans_View_Model> GetLevel()
        {
            List<Plans_View_Model> listItem = new List<Plans_View_Model>();
            List<string> Level = new List<string>();
            EmployeeViewModel plan_list = new EmployeeViewModel();
            try
            {

                Level = empBal.Get_Level();
                foreach (var s in Level)
                {
                    Plans_View_Model plans = new Plans_View_Model();
                    plans.Level = s;
                    listItem.Add(plans);
                }

            }
            catch (Exception e)
            {

            }
            plan_list.Plans_List_View = listItem;
            return listItem;
        }

        [Filters.AuthorizeAdmin]
        public IEnumerable<SimNumbers> GetSimNumber()
        {
            List<SimNumbers> listItem = new List<SimNumbers>();
            List<Sim_Numbers> Sim_numbers = new List<Sim_Numbers>();
            EmployeeViewModel SimListView = new EmployeeViewModel();
            
                try
                {

                    Sim_numbers = empBal.GetSimNumber();
                    foreach (Sim_Numbers sim in Sim_numbers)
                    {
                        SimNumbers sim_numbers = new SimNumbers();
                        sim_numbers.Sim_no = sim.Sim_no;
                        listItem.Add(sim_numbers);
                    }
                }
                catch (Exception e)
                {
                }

                SimListView.Sim_number_list = listItem;
                return listItem;
            
        }

        [Filters.AuthorizeAdmin]
        public IEnumerable<Billed_To_View_Model> GetBilledTo()
        {
            List<Billed_To_View_Model> listItem = new List<Billed_To_View_Model>();
            List<Billed_to> billed_to_list = new List<Billed_to>();
            EmployeeViewModel Billed_To_Lists = new EmployeeViewModel();
             try
                {

                    billed_to_list = empBal.GetBilledTo();
                    foreach (Billed_to bill in billed_to_list)
                    {
                        Billed_To_View_Model billed_to = new Billed_To_View_Model();
                        billed_to.Billed_To = bill.Billed_To;
                        listItem.Add(billed_to);
                    }
                }
                catch(Exception e)
                {
                }

                Billed_To_Lists.Billed_To_Lists = listItem;
                return listItem;
            
        }

        public List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();


        public void EmployeeListData(List<Essar_Users_All> employees, List<EmployeeViewModel> empViewModels)
        {
               foreach (Essar_Users_All emp in employees)
                {
                    EmployeeViewModel empViewModel = new EmployeeViewModel();
                    empViewModel.Sr_no = emp.Sr_no;
                    empViewModel.Employees_Name = emp.Employees_Name;
                    empViewModel.SAP_ID = emp.SAP_ID;
                    empViewModel.Levels = emp.Levels;
                    empViewModel.Mobile_no = emp.Mobile_no;
                    empViewModel.email_id = emp.email_id;
                    empViewModel.Sim_no = emp.Sim_no;
                    empViewModel.Months = emp.Months;
                    empViewModel.Year = emp.Year;
                    empViewModel.Sub_total = emp.Sub_total;
                    empViewModel.GST_18 = emp.GST_18;
                    empViewModel.Limit = emp.Limit;
                    empViewModel.Total = emp.Total;
                    empViewModel.Deduction = emp.Deduction;
                    empViewModel.Billed_To = emp.Billed_To;
                    empViewModel.Department = emp.Department;
                    empViewModel.status = emp.status;
                    empViewModels.Add(empViewModel);
                }
            }
        
        

        [Filters.AuthorizeAdmin]
        public void GetEmployee(string from,string to,int status)
        {
            try
            {
            
            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
           
            List<Essar_Users_All> employees;
            string[] from1 = from.Split('/');
            string[] to1 = to.Split('/');
            int from2 = Int32.Parse(from1[1]);
            int to2 = Int32.Parse(to1[1]);
            string from_month = (from2-1).ToString();
            string to_month = (to2-1).ToString();
            int to_year = Int32.Parse(to1[2]);
            int from_year = Int32.Parse(from1[2]);
            if (from_year == to_year)
            {
                employees = empBal.GetEmployees(from_month, (from_year).ToString(), to_month, to_year.ToString(), status);
                EmployeeListData(employees, empViewModels);
            }
            else
            {

                //Middle month
                employees = empBal.GetEmployees("0", (from_year + 1).ToString(), "11", (to_year - 1).ToString(), status);
                EmployeeListData(employees, empViewModels);
                //first year
                employees = empBal.GetEmployees(from_month, (from_year).ToString(), "11", (from_year).ToString(), status);
                EmployeeListData(employees, empViewModels);
                //Last year
                employees = empBal.GetEmployees("0", (to_year).ToString(), to_month, (to_year).ToString(), status);
                EmployeeListData(employees, empViewModels);
            }
           
            
            
            employeeListViewModel.Employees = empViewModels;
            var js = new JavaScriptSerializer(){ MaxJsonLength = Int32.MaxValue };         
            Response.Write(js.Serialize(empViewModels));
            }
            catch (Exception e)
            {
            }
        }



        [Filters.AuthorizeAdmin]
        [HttpGet]
        public ActionResult Show_Graph(int Sr_no)
        {

            string sapid="", mobileno="";

            EmployeeListViewModel employeeListViewModel = new EmployeeListViewModel();
            EmployeeBusinessLayer empBal = new EmployeeBusinessLayer();
            List<Essar_Users_Bill> employees;
            List<Employee> employees1;
            employees1 = empBal.GetEmployees(Sr_no);
          
            List<UsersBills_View> empViewModels = new List<UsersBills_View>();
            List<EmployeeViewModel> empViewModels1 = new List<EmployeeViewModel>();
            foreach (Employee emp in employees1)
            {
                EmployeeViewModel empViewModel = new EmployeeViewModel();
                empViewModel.Sr_no = emp.Sr_no;

                sapid=empViewModel.SAP_ID = emp.SAP_ID;

                mobileno=empViewModel.Mobile_no = emp.Mobile_no;

                empViewModels1.Add(empViewModel);
            }
            employees = empBal.GetEmployees(sapid,mobileno);
            foreach (Essar_Users_Bill emp in employees)
            {
                UsersBills_View empViewModel = new UsersBills_View();
                empViewModel.Sr_no = emp.Sr_no;              
                empViewModel.SAP_ID = emp.SAP_ID;              
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
            var js = new JavaScriptSerializer();
            //Response.Write(js.Serialize(empViewModels));
            return PartialView(employeeListViewModel);

        }


        [Filters.AuthorizeAdmin]
        public ActionResult Add()
        {

            return PartialView();

        }


        [HttpPost]
        [Filters.AuthorizeAdmin]
        public JsonResult Add1(string Prefix)
        {
            SearchEmployeeByName s = new SearchEmployeeByName();
            var Employee_details = s.GetEmployeeDetails(Prefix);

            return Json(Employee_details, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [Filters.AuthorizeAdmin]
        public JsonResult Add2(string Prefix)
        {
           

                Plans_View_Model plans = new Plans_View_Model();
                List<Plans> scheme = new List<Plans>();
                scheme = empBal.Get_Scheme(Prefix);
                foreach (Plans p in scheme)
                {
                   
                    plans.Plan = p.Plan;
                    plans.Limit = p.Limit;
                }
                
               return Json(plans, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Filters.AuthorizeAdmin]
        public JsonResult Scheme(string Prefix)
        {

            using (var ctx = new Training())
            {
                Plans_View_Model plans = new Plans_View_Model();
                List<Plans> scheme = new List<Plans>();
                scheme = empBal.Get_Limit(Prefix);
                foreach (Plans p in scheme)
                {

                    plans.Limit = p.Limit;
                }
                return Json(plans.Limit, JsonRequestBehavior.AllowGet);
            }




        }

     
       [HttpPost]
       [Filters.AuthorizeAdmin]
        public JsonResult Add([Bind(Exclude = "Sr_no")] Employee objTodo, string Month,string Year)
        {
            string msg;
           
            Email_SendController sendmail = new Email_SendController();
            DateTime dt = DateTime.Now;
            string s = dt.ToString();
           
       
          
        
                if (ModelState.IsValid)
                {
                    
                  
                    using (var ctx = new Training())
                    {

                        using (DbContextTransaction transaction = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                ctx.Employees.Add(objTodo);
                                ctx.SaveChanges();
                                int noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Update Essar_users_Mobile_no set status=1 where Mobile_no=" + objTodo.Mobile_no + ";");
                                noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Insert into Essar_users_Bill values('" + objTodo.SAP_ID + "','" + objTodo.Mobile_no + "','" + Month + "','" + Year + "',0,0,0," + objTodo.Limit + ",0);");
                                noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Insert into Essar_users_Logs values('" + s + "','" + objTodo.Employees_Name + "','" + objTodo.Levels + "','" + objTodo.Mobile_no + "','" + objTodo.SAP_ID + "','" + objTodo.Sim_no + "','" + objTodo.Department + "','','New_ACT');");
                                noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Update Essar_users_Sim_Numbers set status=1 where Sim_no=" + objTodo.Sim_no + ";");
                                transaction.Commit();
                                

                                string type = "Reactivate_SIM";
                                string Reason_Sim_replacement = "No";
                                msg = sendmail.SendEmail(objTodo, type, Reason_Sim_replacement);
                                return Json(new { success = true });
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                msg = "Error occured: Internal Error Occured";

                            }
                        }
                    }
                } 

                else
                {
                    msg = "Validation data not successfull";
                }
                
          

       
            return Json(msg, JsonRequestBehavior.AllowGet); 

        }

       [Filters.AuthorizeAdmin]
       public JsonResult Edit(Employee objTodo, int Change_type, string Reason_Sim_replacement)
        {
            string msg;
           
            Email_SendController sendmail = new Email_SendController();
            DateTime dt = DateTime.Now;
            string s = dt.ToString();

            if (ModelState.IsValid)
            {

                // msg = "Fields Updated";

                using (var ctx = new Training())
                {
                    using (DbContextTransaction transaction = ctx.Database.BeginTransaction())
                    {
                        try
                        {
                            ctx.Entry(objTodo).State = EntityState.Modified;
                            ctx.SaveChanges();
                            if (Change_type == 1)
                            {
                                int noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Insert into Essar_users_Logs values('" + s + "','" + objTodo.Employees_Name + "','" + objTodo.Levels + "','" + objTodo.Mobile_no + "','" + objTodo.SAP_ID + "','" + objTodo.Sim_no + "','" + objTodo.Department + "','','Plan_Change');");
                                string type = "Change_Plan";
                                msg = sendmail.SendEmail(objTodo, type, Reason_Sim_replacement);
                               
                            }
                            else if (Change_type == 2)
                            {

                                int noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Insert into Essar_users_Logs values('" + s + "','" + objTodo.Employees_Name + "','" + objTodo.Levels + "','" + objTodo.Mobile_no + "','" + objTodo.SAP_ID + "','" + objTodo.Sim_no + "','" + objTodo.Department + "','" + Reason_Sim_replacement + "','Sim_Replace');");
                                int noOfRowUpdated1 = ctx.Database.ExecuteSqlCommand("Update Essar_users_Sim_Numbers set status=1 where Sim_no=" + objTodo.Sim_no + ";");
                                string type = "Sim_Replacement";
                                msg = sendmail.SendEmail(objTodo, type, Reason_Sim_replacement);
                               

                            }
                            else if (Change_type == 3)
                            {
                                int noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Insert into Essar_users_Logs values('" + s + "','" + objTodo.Employees_Name + "','" + objTodo.Levels + "','" + objTodo.Mobile_no + "','" + objTodo.SAP_ID + "','" + objTodo.Sim_no + "','" + objTodo.Department + "','','DACT');");
                                int noOfRowUpdated1 = ctx.Database.ExecuteSqlCommand("Update Essar_users_Mobile_no set status=1 where Mobile_no=" + objTodo.Mobile_no + ";");
                                string type = "Mobile_Number";
                                msg = "asd";
                                //msg = sendmail.SendEmail(objTodo.Sr_no,type);

                            }
                            else
                            {
                                msg = "";
                            }
                            transaction.Commit();
                            return Json(new { success = true });

                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            msg = "Error occured: Internal Error Occured";

                        }

                    }
                }
            }
            else
            {
                msg = "Validation data not successfull";
            }
           
                    
            return Json(msg, JsonRequestBehavior.AllowGet);            
        }

        [Filters.AuthorizeAdmin]
        public JsonResult Deactivate(Employee objTodo)
        {
            string msg;
            Email_SendController sendmail = new Email_SendController();
            DateTime dt = DateTime.Now;
            string s = dt.ToString();
          
                if (ModelState.IsValid)
                {
                    
                    using (var ctx = new Training())
                    {
                        using (DbContextTransaction transaction = ctx.Database.BeginTransaction())
                        {
                            try
                            {

                                    string type = "DACT SIM";

                                    int noOfRowUpdated2 = ctx.Database.ExecuteSqlCommand("Delete Essar_users_Mobile_no where Mobile_no='"+objTodo.Mobile_no+"';");
                                   
                                     noOfRowUpdated2 = ctx.Database.ExecuteSqlCommand("Update Essar_users1 set status=0 where Mobile_no=" + objTodo.Mobile_no + "and SAP_ID='"+ objTodo.SAP_ID+"';");
                                     int noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Insert into Essar_users_Logs values('" + s + "','" + objTodo.Employees_Name + "','" + objTodo.Levels + "','" + objTodo.Mobile_no + "','" + objTodo.SAP_ID + "','" + objTodo.Sim_no + "','" + objTodo.Department + "','','DACT');");
                                     transaction.Commit();
                                        string Reason_Sim_replacement = "No";
                                   
                                        msg = sendmail.SendEmail(objTodo, type, Reason_Sim_replacement);
                                         msg = "Deactivated";
                                            return Json(new { success = true });

                                 
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                msg = "Error occured: Internal Error Occured";

                            }
                        }

                    }
                }
                else
                {
                    msg = "Validation data not successfull";
                }
            
           
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [Filters.AuthorizeAdmin]
        public JsonResult Reactivate(Employee objTodo,string Months,string Year)
        {
            string msg;
            DateTime dt = DateTime.Now;
            string s = dt.ToString();
            Email_SendController sendmail = new Email_SendController();
          
                if (ModelState.IsValid)
                {

                    using (var ctx = new Training())
                    {
                        using (DbContextTransaction transaction = ctx.Database.BeginTransaction())
                        {
                            try
                            {

                                string type = "Reactivate_SIM";
                                int noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Insert into Essar_users_Mobile_no values('"+objTodo.Mobile_no+"','1');");
                                noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Update Essar_users_Sim_Numbers set status=1 where Sim_no='" + objTodo.Sim_no + "';");
                                noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Update Essar_users1 set status=1 where Mobile_no='" + objTodo.Mobile_no + "'and SAP_ID = '" + objTodo.SAP_ID + "' and Months = '" + Months + "' and Year = '" + Year + "';");
                                noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Update Essar_users1 set Sim_no='"+objTodo.Sim_no+"' where Mobile_no='" + objTodo.Mobile_no + "'and SAP_ID = '" + objTodo.SAP_ID + "'");
                                noOfRowUpdated = ctx.Database.ExecuteSqlCommand("Insert into Essar_users_Logs values('" + s + "','" + objTodo.Employees_Name + "','" + objTodo.Levels + "','" + objTodo.Mobile_no + "','" + objTodo.SAP_ID + "','" + objTodo.Sim_no + "','" + objTodo.Department + "','','React');");

                                transaction.Commit();
                                string Reason_Sim_replacement = "No";
                               
                                    msg = sendmail.SendEmail(objTodo, type, Reason_Sim_replacement);
                                    msg = "Reactivated";
                                    return Json(new { success = true });


                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                msg = "Error occured: Internal Error Occured";

                            }
                        }
                    }
                }
                else
                {
                    msg = "Validation data not successfull";
                }
           
         
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

      
        public ActionResult logout()
        {
            return RedirectToAction("Logout","Home");
        }

        public ActionResult NoInternet()
        {
            return View();
        }
    }
}
