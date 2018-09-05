using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Vodafone_Essar1.Models
{
    public class Essar_Users_All
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
        public int status { get; set; }
        public string Billed_To { get; set; }
        public string Department { get; set; }
    }
}