using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class UserRolesVM
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }
        public string RoleNameAr { get; set; }

        public bool IsSelected { get; set; }
    }
}
