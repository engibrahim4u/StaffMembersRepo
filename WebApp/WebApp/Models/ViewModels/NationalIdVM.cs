using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class NationalIdVM
    {
        [Display(Name = "الرقم القومي")]
        [Required(ErrorMessage = "ادخل الرقم القومي المكون من 14 رقم")]
        [RegularExpression(@"[2-3]\d{13}", ErrorMessage = "الرقم القومى خطأ")]
        //[Remote(action: "IsNationalIdExsist", controller: "Student")]
        public string NationalId { get; set; }
    }
}
