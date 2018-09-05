using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Vodafone_Essar1.Models
{
    public class Reports
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sr_no { get; set; }

        public DateTime Date { get; set; }
        public string Employees_Name { get; set; }
        public string SAP_ID { get; set; }
        public string Levels { get; set; }
        public string Mobile_no { get; set; }
        public string Sim_no { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public string Department { get; set; }

    }
}