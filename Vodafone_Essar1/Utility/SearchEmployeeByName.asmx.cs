using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Microsoft.VisualBasic;
namespace Vodafone_Essar1.Utility
{
    /// <summary>
    /// Summary description for SearchEmployeeByName
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SearchEmployeeByName : System.Web.Services.WebService
    {
 

        [WebMethod]

        public List<string> GetEmployeeDetails(string prefixText)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("select SAP_Code,User_Name,Email_Id,Department,Emp_Level from MST_OUTLOOK_USER where SAP_Code like @Name+'%' OR User_Name like @Name+'%' OR Email_Id like @Name+'%' OR Department like @Name+'%' OR Emp_Level like @Name+'%'", con);
            cmd.Parameters.AddWithValue("@Name", prefixText);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<string> Employee = new List<string>();



            foreach (DataRow dr in dt.Rows)
            {
                Employee.Add(dr["SAP_Code"] + "|" + dr["User_Name"] + "|" + dr["Email_Id"] + "|" + dr["Department"] + "|" + dr["Emp_Level"]);

            }
            return Employee;
        }

     
    }
}
