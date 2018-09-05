using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using LDAPClassLibrary4V1.ESSAR;
using System.Text;
using System.Data.SqlClient;

using System.Web;
namespace Vodafone_Essar1.Login_Business_Layer
{
    public class UserAccessDAL : Configuration
    {
        /// <summary>
        /// Check weather is authenticated or not
        /// </summary>
        /// <param name="sUserID"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public static bool IsAuthenticated(string sUserID, string sPassword)
        {
            srActiveDirectory ADL = new srActiveDirectory();
           
            string cmStr = null;
            String[] isExist =null;
            bool isValid = false;
            try
            {
                cmStr = ADL.IsAuthenticated(sUserID, sPassword);
                    isExist = cmStr.Split('|');
                    if (isExist[0] == "True")
                    {
                        isValid = true;
                    }
                    else
                    {
                        isValid = false;
                    }
                    return isValid;
                
            }
            catch (Exception objEx)
            {

                return false;
            }
            finally
            {
                ADL = null;
            }

        }


        public static UserAccess GetUserAcessDetail(string sUserID)
        {
            SqlDataReader drUserAccess = null;
            UserAccess objUserAccess = new UserAccess();
            SqlParameter[] arrParam = null;
            try
            {
                arrParam = new SqlParameter[1];
                arrParam[0] = new SqlParameter("@User_Id", SqlDbType.VarChar, 50);
                arrParam[0].Value = sUserID;
                

                drUserAccess = SqlHelper.ExecuteReader(ConnectionString, "SP_Get_Outlook_User_Access", arrParam);

                if (drUserAccess.HasRows)
                {
                    while (drUserAccess.Read())
                    {
                        if (!(System.Convert.IsDBNull(drUserAccess["USER_ID"])))
                        {
                            objUserAccess.UserID = drUserAccess["USER_ID"].ToString();
                        }

                        if (!(System.Convert.IsDBNull(drUserAccess["USER_NAME"])))
                        {
                            objUserAccess.UserName = drUserAccess["USER_NAME"].ToString();
                        }

                        if (!(System.Convert.IsDBNull(drUserAccess["Role_ID"])))
                        {
                           
                                objUserAccess.RoleID = drUserAccess["Role_ID"].ToString();
                        }
                        
                    }
                    
                }
                return objUserAccess;
            }
            catch (Exception objEx)
            {
               
                return null;
            }
            finally
            {
                arrParam = null;
                objUserAccess = null;
                if (((drUserAccess != null)))
                    drUserAccess.Close();

            }

        }

    }
}
