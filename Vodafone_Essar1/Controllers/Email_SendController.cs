using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Vodafone_Essar1.com.srhouse.esshazsrv160;
using System.Configuration;
using Vodafone_Essar1.ViewModels;
using Vodafone_Essar1.Data_Access_Layer;
using Vodafone_Essar1.Models;
namespace Vodafone_Essar1.Controllers
{
     [Filters.NoCache]
    public class Email_SendController : Configuration
    {

        public string SendEmail(Employee ObjTodo, string type, string Reason_Sim_replacement)
        {
            string result = string.Empty;
            Mail objMail = null;
            objMail = new Mail(); 
            string sCC = null, sBCC = null;
           
              string Subject, EmailBody, To;
             List<EmployeeViewModel> empViewModels = new List<EmployeeViewModel>();
             Plans_View_Model empv=new Plans_View_Model();
             EmployeeListViewModel Emplist=new EmployeeListViewModel();
            using (var ctx = new Training())
            {

                //var EmployeeEdit = ctx.Employees.SqlQuery("Select * from Essar_users1 where Sr_no="+ObjTodo.Sr_no+";").ToList<Employee>();
                var EmailTemplate = ctx.Email_Format_Lists.SqlQuery("Select * from Essar_users_Email_Temp where Email_Code='"+type+"';").ToList<Email_Fromat>();

               // var Plan = ctx.Plans_Lists.SqlQuery("Select * from Essar_users_Plans where Level='" + ObjTodo.Levels + "';").ToList<Plans>();


                //foreach (var s in Plan)
                //{
           
                //    empv.Plan = s.Plan;
           
                //}
               
               
                foreach (var s in EmailTemplate)
                {

                    Subject = s.Email_Sub;
                    EmailBody = s.Email_Body;
                    To = s.Email_To;
                    sCC = s.Email_Cc;
                    //sBCC = s.Email_Bcc;
                    EmailBody = EmailBody.Replace("$(Employee_Name)", ObjTodo.Employees_Name);
                    EmailBody = EmailBody.Replace("$(Mobile_No)", ObjTodo.Mobile_no);
                    EmailBody = EmailBody.Replace("$(Sim_No)", ObjTodo.Sim_no);
                    EmailBody = EmailBody.Replace("$(Sap_Id)", ObjTodo.SAP_ID);
                    EmailBody = EmailBody.Replace("$(Plan)", ObjTodo.Scheme);
                    EmailBody = EmailBody.Replace("$(Complaint)", Reason_Sim_replacement);
                    EmailBody = EmailBody.Replace("$(Dept)", ObjTodo.Department);
                    EmailBody = EmailBody.Replace("$(Billed_To)", ObjTodo.Billed_To);
                    EmailBody = EmailBody.Replace("$(Limit)", ObjTodo.Limit.ToString());
                    EmailBody = EmailBody.Replace("$(Level)", ObjTodo.Levels.ToString());
                    result = objMail.SendEmailWithOutAttachments(Configuration.FromMail, Configuration.UserName, Configuration.Password, To, sCC, sBCC, Subject, EmailBody);
                }

                return result;
              
            }

        }

      

    }
}
