using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vodafone_Essar1.ViewModels
{
   
        public class EmployeeViewModel
        {
            public int Sr_no { get; set; }
            public string Employees_Name { get; set; }
            public string SAP_ID { get; set; }
            public string Levels { get; set; }
            public string Mobile_no { get; set; }
            public string Sim_no { get; set; }
            public string Year { get; set; }
            public string email_id { get; set; }
            public string Months { get; set; }
            public decimal Sub_total { get; set; }
            public decimal GST_18 { get; set; }
            public int Limit { get; set; }
            public decimal Total { get; set; }
            public decimal Deduction { get; set; }
            public string Billed_To { get; set; }
            public string Department { get; set; }
            public string Scheme { get; set; }
            public int status { get; set; }
            public List<MobileNumbers> MobilenumberList { get; set; }
            public List<SimNumbers> Sim_number_list { get; set; }
            public List<Billed_To_View_Model> Billed_To_Lists { get; set; }
            public List<Plans_View_Model> Plans_List_View { get; set; }
        }
    
}