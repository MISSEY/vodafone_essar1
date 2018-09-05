using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vodafone_Essar1.Models
{
    public class Billed_to
    {
         [Key]
        public string Billed_To { get; set; }
    }
}