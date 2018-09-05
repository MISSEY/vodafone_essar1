using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Vodafone_Essar1.Models
{
    public class Email_Fromat
    {

        [Key]
        public string Email_code { get; set; }
        [Column(TypeName = "text")]
        public string Email_Sub { get; set; }
        [Column(TypeName = "text")]
        public string Email_Body { get; set; }
        public string Email_To { get; set; }
        public string Email_Cc { get; set; }
        public string Email_Bcc { get; set; }
       
    }
}