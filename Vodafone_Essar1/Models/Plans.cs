using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Vodafone_Essar1.Models
{
    public class Plans
    {
        [Key]
       public string Level { get;set ;}
       public string Plan { get; set; }
       public int Limit { get; set; }
        [Column(TypeName = "text")]
       public string Descrp { get; set; }
    }
}