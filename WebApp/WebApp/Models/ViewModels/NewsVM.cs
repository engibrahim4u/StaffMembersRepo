using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class NewsVM
    {
        public int Id { get; set; }
        [Range(minimum: 1, maximum: 1000, ErrorMessage = "رقم من 1 الى 1000")]
        [Display(Name = "ترتيب العرض")]
        public int Order { get; set; }
        [Display(Name = "عرض بالصفحة الرئيسية للموقع العربي")]
        public bool DisplayInHome { get; set; }
        [Display(Name = "عرض بالصفحة الرئيسية للموقع الانجليزي")]
        public bool DisplayInHomeEn { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "تاريخ الإضافة")]
        public DateTime CDate { get; set; }
        [StringLength(maximumLength: 100)]
        [Display(Name = "صورة الخبر")]
        public string Image { get; set; }
        [StringLength(maximumLength: 300)]
        [Display(Name = "العنوان باللغة العربية")]
        [Required(ErrorMessage = "مطلوب")]
        public string Header { get; set; }
        [StringLength(maximumLength: 300)]
        [Display(Name = "العنوان باللغة الانجليزية")]
        [Required(ErrorMessage = "مطلوب")]
        public string HeaderEn { get; set; }
        [Display(Name = "التفاصيل باللغة العربية")]
        //[Required(ErrorMessage ="مطلوب")]
        public string Content { get; set; }
        [Display(Name = "التفاصيل باللغة الانجليزية")]
        //[Required(ErrorMessage ="مطلوب")]
        public string ContentEn { get; set; }
        public int JournalId { get; set; }
        [Display(Name = "صورة الخبر")]
        public IFormFile ImageFile { get; set; }
        public bool HasDetails { get; set; }
    }
}
