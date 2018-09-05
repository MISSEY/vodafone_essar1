using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Configuration;

namespace Vodafone_Essar1.Controllers
{
    public abstract class Configuration
    {
        public static string ConnectionString
        {
            get { return ConfigurationManager.AppSettings["ConnectionString"]; }
        }
      

        public static string FromMail
        {
            get { return ConfigurationManager.AppSettings["FromEmail"]; }
        }
        public static string UserName
        {
            get { return ConfigurationManager.AppSettings["UserName"]; }
        }
        public static string Password
        {
            get { return ConfigurationManager.AppSettings["Password"]; }
        }



      
        
    }
}

