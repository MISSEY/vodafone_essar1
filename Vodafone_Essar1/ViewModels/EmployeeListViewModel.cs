using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vodafone_Essar1.ViewModels
{
    public class EmployeeListViewModel
    {
        public List<EmployeeViewModel>  Employees { get; set; }
        public List<MobileNumbers> MobilenumberList { get; set; }
        public List<UsersBills_View> Employees_Bills { get; set; }
    }
}