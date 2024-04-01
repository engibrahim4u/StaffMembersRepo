using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class VolumeVM
    {
        public int Id { get; set; }
        [Display(Name = "الرقم")]
        [Required(ErrorMessage = "مطلوب")]
        public int Number { get; set; }
        [Display(Name = "الاسم")]
        [Required(ErrorMessage = "مطلوب")] 
        [StringLength(200, ErrorMessage = "200 حرف فقط")]
        public string Name { get; set; }
        [Display(Name = "الاسم باللغة الانجليزية")]
        [Required(ErrorMessage = "مطلوب")]
        [StringLength(200, ErrorMessage = "200 حرف فقط")]
        public string NameEn { get; set; }
        public string IdEncrypted { get; set; }

        public int JournalId { get; set; }

    }
}
