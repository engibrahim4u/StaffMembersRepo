using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class UserManageVM
    {
        [Display(Name = "البريد الالكتروني")]
        public string Email { get; set; }
        [Display(Name = "الاسم")]
        public string Name { get; set; }

        [Display(Name = "الجوال")]
        public string Mobile { get; set; }
        [Display(Name = "الصلاحية")]
        public List<UserRolesVM> Roles { set; get; } = new List<UserRolesVM>();
        [Display(Name = "Active")]
        public bool Active { set; get; }

        [Display(Name = "الصلاحية")]
        public string Role { get; set; }
        public string RoleAr { get; set; }


        public string UserId { get; set; }
    }
}
