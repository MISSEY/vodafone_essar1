using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;



namespace Vodafone_Essar1.Login_Business_Layer
{
    public class UserAccessBLL
    {
        /// <summary>
        /// Aunthenticated user id and password
        /// </summary>
        /// <param name="sUserID"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public static bool IsAuthenticated(string sUserID, string sPassword)
        {
            return UserAccessDAL.IsAuthenticated(sUserID, sPassword);
        }

        /// <summary>
        /// Get User Access Details
        /// </summary>
        /// <param name="sUserID"></param>
        /// <returns></returns>
        public static UserAccess GetUserAcessDetail(string sUserID)
        {
            return UserAccessDAL.GetUserAcessDetail(sUserID);
        }
    }
}
