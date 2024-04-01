using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class EMailMessagesVM
    {
        [StringLength(maximumLength: 50)]
        public string Code { get; set; }
        public int JournalId { get; set; }
        [Display(Name = "الاسم")]
        public string Name { get; set; }
        [Display(Name = " الرسالة باللغة العربية")]
        [Required(ErrorMessage = "ادخل الرسالة باللغة العربية ")]
        public string Msg { get; set; }
        [Display(Name = " الرسالة باللغة الانجليزية")]
        [Required(ErrorMessage = "ادخل الرسالة باللغة الانجليزية ")]
        public string MsgEn { get; set; }
        [StringLength(maximumLength: 255)]
        [Display(Name = " العنوان  باللغة العربية")]
        [Required(ErrorMessage = "ادخل العنوان باللغة العربية ")]
        public string Title { get; set; }
        [StringLength(maximumLength: 255)]
        [Display(Name = " العنوان  باللغة الانجليزية")]
        [Required(ErrorMessage = "ادخل العنوان باللغة الانجليزية ")]
        public string TitleEn { get; set; }
        [Display(Name = " ارسال الى البريد الالكتروني")]
        public bool SendEmail { get; set; }
        public string JournalIdEncrypted { get; set; }
    }
}
