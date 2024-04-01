using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class MobileVM
    {
        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"01\d{9}", ErrorMessage = "Error Mobile")]
        public string Mobile { get; set; }
    }
}
