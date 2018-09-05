using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Web.SessionState;
using Vodafone_Essar1.ViewModels;
using Vodafone_Essar1.Data_Access_Layer;
using System.Text.RegularExpressions;

namespace Vodafone_Essar1.Controllers
{
    [Filters.NoCache]
    [SessionState(SessionStateBehavior.ReadOnly)] 
    [Filters.AuthorizeAdmin]
    public class FileController : Controller
    {
        SqlConnection con = new SqlConnection("Server=150.0.150.17;Database=Training;User Id=TRAINEE;Password=TRAINEE");
        OleDbConnection Econ;
        DataSet ds = new DataSet();
        DataTable dt;

        int counter = 0;
        string msg = null;

         [Filters.AuthorizeAdmin]
        private void ExcelConn(string filepath)
        {
            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0;HDR=YES;IMEX=1""", filepath);

            Econ = new OleDbConnection(constr);
        }
        public void Template_download()
        {
          
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("content-disposition", "filename=Template.xlsx");
            Response.TransmitFile(Server.MapPath("~/Template/Template.xlsx"));
            Response.End();
          
        }
        public void Template_download1()
        {

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("content-disposition", "filename=Template_Sim.xlsx");
            Response.TransmitFile(Server.MapPath("~/Template/Template_Sim.xlsx"));
            Response.End();

        }
        public void Template_download2()
        {

            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("content-disposition", "filename=Template_mob.xlsx");
            Response.TransmitFile(Server.MapPath("~/Template/Template_mob.xlsx"));
            Response.End();

        }


        
        public ActionResult Index()
        {
            return View();
        }
        
         public Excelsheet_List_viewmodel SheetList(string file)
         {
             ExcelConn(file);
             Econ.Open();
             DataTable dt = Econ.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
             Econ.Close();
             List<Excel_sheetcs> excellist=new List<Excel_sheetcs>();
             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 Excel_sheetcs excellist1 = new Excel_sheetcs();
                 String sheet_name = dt.Rows[i]["TABLE_NAME"].ToString();
                 sheet_name = sheet_name.Substring(0, sheet_name.Length - 1);
                 excellist1.Name = sheet_name;
                 excellist1.full_path=file;
                 excellist.Add(excellist1);
             }
             Excelsheet_List_viewmodel s=new Excelsheet_List_viewmodel();
             s.Excelsheetlist = excellist;
             return s;
           

         }

    
         [HttpPost]
         public ActionResult filesubmit(HttpPostedFileBase file)
         {
             Excelsheet_List_viewmodel s = new Excelsheet_List_viewmodel();
             try
             {

                 string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);

                 string filepath = "/excelfolder/" + filename;



                 file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));

                 string fullpath = Server.MapPath("/excelfolder/") + filename;
                    s= SheetList(fullpath);
             }
             catch (Exception e)
             {
             }
            
                 return View("Index",s);
             
         }
       
       

        
         [HttpPost]
         public ActionResult Index1(string Sheet_name,string fullpath)
         {
             try
             {
                 string[] m = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                 ExcelConn(fullpath);
                 string query = string.Format("Select [SAP ID],[Mobile No],[Month],[Sub Total],[18% GST],[Total],[limit],[Deduction] from [{0}$]", Sheet_name);

                 OleDbCommand Ecom = new OleDbCommand(query, Econ);

                 Econ.Open();





                 OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);

                 Econ.Close();

                 oda.Fill(ds);



                 dt = ds.Tables[0];
                 
                 dt.Columns.Add("Months", typeof(string));
                 dt.Columns.Add("Year", typeof(string));
                 dt.Columns.Add("SapID", typeof(string));

                 foreach (DataRow row in dt.Rows)
                 {

                     string s = row["Month"].ToString();
                     string[] s1 = s.Split('\'');
                   
                     int i = Array.IndexOf(m, s1[0].Trim());
                     row["Months"] = i.ToString();
                     row["Year"] = "20" + s1[1];
                     var s4 = row["SAP ID"];
                     if (s4.ToString().Equals("") || s4.ToString().Equals("Advisor") || s4.ToString().Contains("Adviser") || s4.ToString().Contains("Consult") || s4.ToString().Contains("Vendor") || s4.ToString().Contains("IT") || s4.ToString().Equals("Adv") || s4.ToString().Contains("adv") || s4.ToString().Contains("tem") || s4.ToString().Contains("Tem"))
                     {
                         using (var ctx = new Training())
                         {
                             string SAPID = ctx.Database.SqlQuery<string>("Select SAP_ID from Essar_users1 where Mobile_no='" + row["Mobile No"].ToString().Trim() + "';").FirstOrDefault();
                             row["SapID"] = Regex.Replace(SAPID, @"\s", "");
                         }
                     }
                     else
                     {
                         row["SapID"] = Regex.Replace(row["SAP ID"].ToString(), @"\s", "");
                     }
                     if (row["limit"].ToString().Equals(""))
                     {
                         row["limit"] = 0;
                     }
                     if (row["Deduction"].ToString().Equals(""))
                     {
                         row["Deduction"] = 0;
                     }

                 }
                 foreach (DataRow row1 in dt.Rows)
                 {
                     string mobile = row1["Mobile No"].ToString();
                     string limit = row1["limit"].ToString();
                     if (mobile.Equals("") || mobile.Contains(" "))
                     {

                         counter = 1;
                         break;

                     }
                     if (limit.Equals("") || limit.Contains(" "))
                     {
                         counter = 2;
                         break;
                     }




                 }


                 if (counter == 0)
                 {

                     using (SqlBulkCopy objbulk = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.CheckConstraints))
                     {
                        

                             objbulk.DestinationTableName = "Essar_users_Bill";

                             objbulk.ColumnMappings.Add("Mobile No", "Mobile_no");

                             objbulk.ColumnMappings.Add("Sub Total", "Sub_total");

                             objbulk.ColumnMappings.Add("18% GST", "GST_18");

                             objbulk.ColumnMappings.Add("Total", "Total");

                             objbulk.ColumnMappings.Add("limit", "Limit");

                             objbulk.ColumnMappings.Add("Deduction", "Deduction");

                             objbulk.ColumnMappings.Add("Months", "Months");

                             objbulk.ColumnMappings.Add("Year", "Year");

                             objbulk.ColumnMappings.Add("SapID", "SAP_ID");


                             con.Open();

                             objbulk.WriteToServer(dt);
                        
                       


                         con.Close();
                     }
                     if (msg == null)
                     {
                         ViewBag.Error = "Successfully Uploaded";
                     }

                 }
                 else if (counter == 1)
                 {
                     ViewBag.Error = "Mobile Number Should be trimed";
                 }
                 else if (counter == 2)
                 {
                     ViewBag.Error = "Limit should be Integer Type";
                 }
                 else
                 {
                 }
                 
             }
             catch (System.Data.SqlClient.SqlException e)
             {
                 msg = e.GetBaseException().ToString();
                 if (msg.Contains("FOREIGN KEY"))
                 {

                     ViewBag.Error = "Miss match of SAP ID and Mobile number, Please Enter Valid SAP ID against Valid Mobile Number";
                 }
             }
             catch (System.Data.OleDb.OleDbException e)
             {
                 msg = e.Message.ToString();
                 if (msg.Contains("No value given for one or more required parameters"))
                 {

                     ViewBag.Error = "Please Upload Correct File";
                 }
             }

             return View("Index");
         }


    }
}
