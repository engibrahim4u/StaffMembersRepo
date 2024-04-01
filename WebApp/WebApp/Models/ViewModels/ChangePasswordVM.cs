using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class ChangePasswordVM
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"[a-zA-Z0-9@$_.%#&]{6,12}", ErrorMessage = "Password contain English Characters and digits and special Characters like @$_.%#&")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword",
        ErrorMessage = "Password and Confirm Password not Same.")]
        public string ConfirmPassword { get; set; }
    }
}
