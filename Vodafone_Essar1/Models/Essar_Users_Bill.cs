using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Vodafone_Essar1.Models
{
    public class Essar_Users_Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sr_no { get; set; }
        [Column(Order=0)] 
        [ForeignKey("Employee")]
        public string SAP_ID { get; set; }
        [Column(Order=1)] 
        [ForeignKey("Employee")]
        public string Mobile_no { get; set; }
        public string Year { get; set; }
        public string Months { get; set; }
        [Required]
        public decimal Sub_total { get; set; }
        public decimal GST_18 { get; set; }
        public int    Limit { get; set; }
        [Required]
        public decimal Total { get; set; }
        public decimal Deduction { get; set; }
        [Required]
        public virtual Employee Employee { get; set; } 
    }
    
}