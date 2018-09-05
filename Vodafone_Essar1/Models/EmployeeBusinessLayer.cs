using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vodafone_Essar1.Data_Access_Layer;
using Vodafone_Essar1.ViewModels;

namespace Vodafone_Essar1.Models
{
    public class EmployeeBusinessLayer
    {
        public List<Essar_Users_All> GetEmployees(string from_month, string from_year, string to_month, string to_year, int status)
        {
            int s = status;

            List<Essar_Users_All> result = new List<Essar_Users_All>();
            Training salesDal = new Training();
            var query = (from em in salesDal.Employees
                         join b in salesDal.Essar_users_bill
                         on new{a=em.SAP_ID,c=em.Mobile_no} equals  new{a=b.SAP_ID,c=b.Mobile_no} into gj
                         from x in gj.DefaultIfEmpty()
                         where em.status==s
                         where x.Months.CompareTo(from_month) >= 0 && x.Months.CompareTo(to_month) <= 0
                         where x.Year.CompareTo(from_year) >= 0 && x.Year.CompareTo(to_year) <= 0
                         select new
                         {
                             Sr_no=em.Sr_no,
                             Employees_Name=em.Employees_Name,
                             SAP_ID = em.SAP_ID,
                             Levels = em.Levels,
                             email_id=em.email_id,
                             Mobile_no=em.Mobile_no,
                             Sim_no=em.Sim_no,
                             status=em.status,
                             Billed_To=em.Billed_To,
                             Department=em.Department,
                             Year = (x == null ? String.Empty : x.Year),
                             Months = (x == null ? String.Empty : x.Months),
                             Sub_total = (x == null ? 0 : x.Sub_total),
                             GST_18 = (x == null ? 0 : x.GST_18),
                             Total = (x == null ? 0: x.Total),
                             Deduction = (x == null ? 0 : x.Deduction),
                             Limit = (x == null ? 0: x.Limit),

                         }).ToList();
           
            foreach (var sa in query)
            {
                Essar_Users_All emp = new Essar_Users_All();
                emp.Sr_no=sa.Sr_no;
                emp.Employees_Name=sa.Employees_Name;
                emp.SAP_ID = sa.SAP_ID;
                emp.Levels = sa.Levels;
                emp.email_id=sa.email_id;
                emp.Mobile_no = sa.Mobile_no;
                emp.Sim_no = sa.Sim_no;
                emp.Billed_To = sa.Billed_To;
                emp.Department = sa.Department;
                emp.Year = sa.Year;
                emp.Months = sa.Months;
                emp.Sub_total = sa.Sub_total;
                emp.GST_18 = sa.GST_18;
                emp.Total = sa.Total;
                emp.Deduction = sa.Deduction;
                emp.Limit = sa.Limit;
                emp.status = sa.status;
                 
                           
                result.Add(emp);
                
            }
            return result;
        }
        //"Select * from Essar_users1 where status=1 and Months Between"+from_month+"AND"+to_month+"and Year Between"+from_year+"AND"+to_year+";"
        public List<Essar_Users_Bill> GetEmployees(string SAP_ID,string Mobile_no)
        {
            Training salesDal = new Training();
            return salesDal.Essar_users_bill.SqlQuery(" select * from Essar_Users_Bill where SAP_ID='" + SAP_ID + "' and Mobile_no='" + Mobile_no + "'and Year in (Select Year from Essar_Users_Bill where Mobile_no='" + Mobile_no + "' and SAP_ID='" + SAP_ID + "') ").ToList();
           

        }
        public List<Employee> GetEmployees(int Sr_no)
        {
            Training salesDal = new Training();
            return salesDal.Employees.SqlQuery(" select * from Essar_users1 where Sr_no=" + Sr_no + ";").ToList();


        }
        public List<UserCredential> GetUserCredential()
        {
            Training salesDal = new Training();
            return salesDal.UserCredentials.ToList();
        }
        public List<Reports> GetReports(string from, string to,string Type)
        {
     
            Training salesDal = new Training();
            return salesDal.Reports_Log.SqlQuery(" select * from Essar_users_Logs where Date between '"+ from +"' and '"+ to +"' and Type= '"+Type+"'; ").ToList();


        }
        public List<Mobile_numbers> Get_Mobile_Numbers()
        {

            Training salesDal = new Training();
            return salesDal.MobileNumberLists.SqlQuery("Select * from Essar_users_Mobile_no where status=0").ToList();


        }
        public List<string> Get_Scheme()
        {

            Training salesDal = new Training();
            return salesDal.Database.SqlQuery<string>("Select Distinct [Plan] from Essar_users_Plans").ToList();


        }
        public List<string> Get_Level()
        {

            Training salesDal = new Training();
            return salesDal.Database.SqlQuery<string>("Select [Level] from Essar_users_Plans").ToList();


        }
        public List<Plans> Get_Scheme(string Prefix)
        {

            Training salesDal = new Training();
            return salesDal.Plans_Lists.SqlQuery("Select * from Essar_users_Plans where Level='" + Prefix + "';").ToList<Plans>();


        }
        public List<Plans> Get_Limit(string Prefix)
        {

            Training salesDal = new Training();
            return salesDal.Plans_Lists.SqlQuery("Select * from Essar_users_Plans where [Plan]='" + Prefix + "';").ToList<Plans>();


        }
        public List<Sim_Numbers> GetSimNumber()
        {

            Training salesDal = new Training();
            return salesDal.SimNumberLists.SqlQuery("Select * from Essar_users_Sim_Numbers where status=0").ToList<Sim_Numbers>();


        }

        public List<Essar_Users_Bill> GetBill(string sapid)
        {

            Training salesDal = new Training();
            return salesDal.Essar_users_bill.SqlQuery("Select * from Essar_Users_Bill where SAP_ID='"+sapid+"';").ToList<Essar_Users_Bill>();


        }
        public List<Essar_Users_Bill> GetUsersBill(string Mobile_no)
        {

            Training salesDal = new Training();
            return salesDal.Essar_users_bill.SqlQuery("Select * from Essar_Users_Bill where Mobile_no='" + Mobile_no + "';").ToList<Essar_Users_Bill>();


        }


        public List<Billed_to> GetBilledTo()
        {

            Training salesDal = new Training();
            return salesDal.Billed_To_Lists.SqlQuery("Select * from Essar_users_Billing_Sheet").ToList<Billed_to>();


        }

        public string SapID(int Sr_no)
        {
            using (var ctx = new Training())
            {
                //Get student name of string type
                string Sapid = ctx.Database.SqlQuery<string>("Select SAP_ID from Essar_users1 where Sr_no="+Sr_no+";")
                                        .FirstOrDefault();
                return Sapid;
            }
            
        }




    }
}