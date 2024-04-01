using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class UserManageDataVM
    {
        public string Name { get; set; }
        public string RoleName { get; set; }
        public string Active { get; set; }
        public List<UserRolesVM> Roles { get; set; }
        public List<UserManageVM> Users { get; set; }


    }
}
