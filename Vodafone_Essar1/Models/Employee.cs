using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Vodafone_Essar1.Models
{
    public class Employee
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sr_no { get; set; }
        public string Employees_Name { get; set; }
        [Key, Column(Order = 0)]
        public string SAP_ID { get; set; }
        public string Levels{ get; set; }
        [Key, Column(Order = 1)]
        public string Mobile_no { get; set; }
        public string Sim_no { get; set; }
        public string email_id { get; set; }
        public int    Limit { get; set; }
        public string Billed_To { get; set; }
        public string Department {get; set;}
        public string Scheme { get; set; }
        public int status { get; set; }
        public ICollection<Essar_Users_Bill> Essar_users_bil { get; set; }
    }
}