using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
namespace Vodafone_Essar1.Models
{
    public class Sim_Numbers
    {
        [Key]
        public string Sim_no { get; set; }
        public int status { get; set; }
    }
}