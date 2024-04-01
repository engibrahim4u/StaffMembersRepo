using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class UserVM
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^((([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", ErrorMessage = "Not Email Address")]
        [StringLength(50, ErrorMessage = "Text must not more than 50 Characters")]
        [Remote(action: "IsUserExsist", controller: "Account")]
        public string Email { get; set; }

        [Display(Name = "Password")]

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[a-zA-Z0-9@$_.%#&]{6,12}", ErrorMessage = "Password contain English Characters and digits and special Characters like @$_.%#&")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "  Password and Confirm Password not Same")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = " النص المدخل لايزيد عن  100 حرف ")]
        [RegularExpression(@"[ء-ي]{2,20}\s[ء-ي]{2,20}[\s[ء-ي]{1,20}]*", ErrorMessage = " ادخل الاسم  باللغة العربية")]

        public string Name { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"0\d{9,18}", ErrorMessage = "Input Error")]
        [Remote(action: "IsMobileExsist", controller: "Account")]

        public string Mobile { get; set; }
        [Display(Name = "الصلاحية")]
        public List<RoleVM> Roles { set; get; } = new List<RoleVM>();
        [Display(Name = "Role")]
        [Required(ErrorMessage = "Required")]
        public string SelectedRole { set; get; }
    }
}
