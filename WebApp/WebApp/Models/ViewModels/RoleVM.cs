using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class RoleVM
    {
        public RoleVM()
        {
            Users = new List<string>();
        }
        public string ID { set; get; }
        [Required(ErrorMessage = "enter role name")]
        [Display(Name = "مسمى الصلاحية")]
        [RegularExpression(@"[a-zA-Z]{2,20}", ErrorMessage = " حروف انجليزي من 2 الي 20 حرف بدون مسافات")]
        public string Name { set; get; }

        public List<string> Users { get; set; }
    }
}
