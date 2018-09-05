using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
namespace Vodafone_Essar1.Login_Business_Layer

{


    public class UserAccess
    {

        #region "Private Variable"
        private string _userId = null;
        private string _userName = null;
        private string _roleID = null;
        private string _locationID = null;
        private string _locationName = null;
        
        #endregion

        #region "Public Properties"
        /// <summary>
        /// Gets or Sets the UserId of the User/User
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string UserID
        {
            get { return _userId; }
            set { _userId = value; }
        }

        /// <summary>
        /// Gets or Sets the UserName of the User/User
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
     

        /// <summary>
        /// Gets or Sets the Role ID of the Admin
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string RoleID
        {
            get { return _roleID; }
            set { _roleID = value; }
        }
        /// <summary>
        /// Gets or Sets the Location ID of the Admin
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string LocationID
        {
            get { return _locationID; }
            set { _locationID = value; }
        }
        /// <summary>
        /// Gets or Sets the Location Name of the Admin
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public string LocationName
        {
            get { return _locationName; }
            set { _locationName = value; }
        }
      
        #endregion


    }
}
