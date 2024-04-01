using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class BoardVM
    {
        public int Id { get; set; }
        [Display(Name = "نرتيب العرض")]
        [Required(ErrorMessage = "مطلوب")]
        public int Order { get; set; }
        [Display(Name = "الاسم")]
        [Required(ErrorMessage = "مطلوب")]
        [StringLength(200, ErrorMessage = "200 حرف فقط")]
        public string Name { get; set; }
        [Display(Name = "الاسم باللغة الانجليزية")]
        [Required(ErrorMessage = "مطلوب")]
        [StringLength(200, ErrorMessage = "200 حرف فقط")]
        public string NameEn { get; set; }

        [Display(Name = "المنصب العلمي")]
        //[Required(ErrorMessage = "مطلوب")]
        [StringLength(200, ErrorMessage = "200 حرف فقط")]
        public string Title { get; set; }
        [Display(Name = "المنصب العلمي باللغة الانجليزية")]
        //[Required(ErrorMessage = "مطلوب")]
        [StringLength(200, ErrorMessage = "200 حرف فقط")]
        public string TitleEn { get; set; }

        [Display(Name = "المنصب")]
        //[Required(ErrorMessage = "مطلوب")]
        [StringLength(50, ErrorMessage = "50 حرف فقط")]
        public string Position { get; set; }
        public string PositionEn { get; set; }

        [Display(Name = "الصورة الشخصية")]

        public string Photo { get; set; }
        [Display(Name = "السيرة الذاتية")]

        public string CV { get; set; }

        [Display(Name = "اللقب")]
        //[Required(ErrorMessage = "مطلوب")]
        [StringLength(100, ErrorMessage = "100 حرف فقط")]
        public string Suffix { get; set; }

        [Display(Name = "اللقب باللغة الانجليزية")]
        //[Required(ErrorMessage = "مطلوب")]
        [StringLength(100, ErrorMessage = "100 حرف فقط")]
        public string SuffixEn { get; set; }
        [Display(Name = "الصورة الشخصية")]
        public IFormFile PhotoFile { get; set; }

        [Display(Name = "السيرة الذاتية")]
        public IFormFile CVFile { get; set; }

        [Display(Name = "البلد")]

        public string CountryName { get; set; }
        [Display(Name = "البلد")]
        [Required(ErrorMessage = "مطلوب")] 
        public int CountryId { get; set; }
        public List<Country> Countries { get; set; }
        public string EncryptedId { get; set; }
        public int JournalId { get; set; }
    }

}
