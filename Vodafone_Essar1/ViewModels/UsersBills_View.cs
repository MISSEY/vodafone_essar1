using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vodafone_Essar1.ViewModels
{
    public class UsersBills_View
    {
        
        public int Sr_no { get; set; }    
        public string SAP_ID { get; set; }   
        public string Mobile_no { get; set; }
        public string Year { get; set; }
        public string Months { get; set; }
        public decimal Sub_total { get; set; }
        public decimal GST_18 { get; set; }
        public int Limit { get; set; }
        public decimal Total { get; set; }
        public decimal Deduction { get; set; }
    }
}