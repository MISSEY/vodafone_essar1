using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Vodafone_Essar1.Login_Business_Layer
{
    public abstract class Configuration
    {
        public static string ConnectionString
        {
            get { return WebConfigurationManager.AppSettings["ConnectionString1"]; }
        }
        public static string ErrorLog
        {
            get { return WebConfigurationManager.AppSettings["ErrorLog"]; }
        }
        public static string SMTP
        {
            get { return WebConfigurationManager.AppSettings["SMTP"]; }
        }

        public static string FromEmail
        {
            get { return WebConfigurationManager.AppSettings["FromEmail"]; }
        }
        public static string UserName
        {
            get { return WebConfigurationManager.AppSettings["UserName"]; }
        }
        public static string Password
        {
            get { return WebConfigurationManager.AppSettings["Password"]; }
        }
      
        public static string VirtualDirectoryName
        {
            get { return WebConfigurationManager.AppSettings["VirtualDirectoryName"]; }
        }

        //For Excel Upload
        public static string TempExcelFile
        {
            get { return WebConfigurationManager.AppSettings["TempExcelFile"]; }
        }
        public static string xls
        {
            get { return WebConfigurationManager.AppSettings["xls"]; }
        }
        public static string xlsx
        {
            get { return WebConfigurationManager.AppSettings["xlsx"]; }
        }
        public static string ApprovalIP
        {
            get { return WebConfigurationManager.AppSettings["ApprovalIP"]; }
        }
        public static string DocumentFilepath
        {
            get { return WebConfigurationManager.AppSettings["DocumentFilepath"]; }
        }
        
    }
}
