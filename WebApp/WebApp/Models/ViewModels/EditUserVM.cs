using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class EditUserVM
    {
        public EditUserVM()
        {
            Claims = new List<string>();
            Roles = new List<string>();
        }
        public string Id { get; set; }
        [Required(ErrorMessage = "ادخل البريد الالكتروني")]
        [Display(Name = "البريد الالكتروني")]
        [RegularExpression(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", ErrorMessage = " ادخل البريد الالكتروني بطريقه صحيحه")]
        public string UserName { get; set; }
        [Display(Name = "الاسم")]
        [Required(ErrorMessage = "ادخل الاسم ")]
        [StringLength(100, ErrorMessage = " النص المدخل لايزيد عن  100 حرف ")]
        [RegularExpression(@"[ء-ي]{2,20}\s[ء-ي]{1,20}[\s[ء-ي]{1,20}]*", ErrorMessage = " ادخل الاسم  باللغة العربية")]
        public string Name { get; set; }

        [Display(Name = "الجوال")]
        [StringLength(11, ErrorMessage = " النص المدخل لايزيد عن  11 حرف ")]
        [RegularExpression(@"01\d{9}", ErrorMessage = "ادخل جوال  بطريقة صحيحة")]
        [Required(ErrorMessage = "ادخل الجوال ")]
        public string Mobile { get; set; }

        [Display(Name = "نشط")]
        public bool EmailConfirmed { get; set; }

        public IList<string> Roles { get; set; }
        public List<string> Claims { get; set; }
    }
}
