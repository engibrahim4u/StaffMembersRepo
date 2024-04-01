using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [Display(Name = "تاريخ البداية ")]
        [Required]
        public DateTime StartDate { get; set; }
        [Display(Name = "تاريخ النهاية ")]
        [Required]
        public DateTime EndDate { get; set; }
        [Display(Name = "العام الجامعي الحالي")]
        [Required]
        [StringLength(100, ErrorMessage = " النص المدخل لايزيد عن  100 حرف ")]
        public string CurrentAcademicYear { get; set; }

        [Display(Name = "اتاحة الموقع ")]
        public bool Available { get; set; }

        [Display(Name = "اتاحة الموقع للطلاب للارسال")]
        public bool StudentNewRequestAvailable { get; set; }
        [Display(Name = "اتاحة الموقع للطلاب للتعديل")]
        public bool StudentUpadateAvailable { get; set; }
        [Display(Name = "اتاحة الموقع للكليات للتعديل")]
        public bool placeUpdateAvailable { get; set; }


        [Display(Name = "اتاحة الموقع للادارة العامة للتعديل")]
        public bool AdminUpdateAvailable { get; set; }

        [Display(Name = "اتاحة الموقع للكليات")]
        public bool placeAvailable { get; set; }


        [Display(Name = "اتاحة الموقع للادارة العامة")]
        public bool AdminAvailable { get; set; }
        [Required]
        [Display(Name = "نص الرسالة في حال عدم اتاحة الموقع للطلاب ")]
        [StringLength(300, ErrorMessage = " النص المدخل لايزيد عن  300 حرف ")]
        [RegularExpression(@"[-\s\d\.ء-ي]*", ErrorMessage = "ادخل باللغة العربية")]
        public string NotAvailableMessage { get; set; }
        public string SecureSite { get; set; }
        public string Notes { get; set; }

        [Display(Name = "الصورة الشخصية مطلوبة")]
        public bool PersonalPhotoRequired { get; set; }
        [Display(Name = "صورة الرقم القومي مطلوبة")]
        public bool NationalIdImageRequired { get; set; }

        [Display(Name = "المدير التنفيذي للتدريب")]
        public string TrainingSupervisor { get; set; }
        [Display(Name = "ثمن النسخة الاضافية للشهادة")]
        public float ExtraCertificatePrice { get; set; }
        [Display(Name = "معلومات الاتصال ")]
        public string ContactInfo { get; set; }


    }
}
