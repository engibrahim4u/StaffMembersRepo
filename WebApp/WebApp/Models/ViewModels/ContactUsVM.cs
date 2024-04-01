using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class ContactUsVM
    {
        [Display(Name ="Message Details")]
        [StringLength(maximumLength: 2000,MinimumLength =30,ErrorMessage ="At least from 30 charcters")]
       [Required(ErrorMessage ="Required")]
        public string Message { get; set; }
        public DateTime CDate { get; set; }
        [StringLength(maximumLength: 100)]
        public string ReceiverName { get; set; }
        [StringLength(maximumLength: 100,MinimumLength =10,ErrorMessage ="From 10 to 50 charcters")]
        [Display(Name = "Sender Name")]
       [Required(ErrorMessage ="Required")]
        public string SenderName { get; set; }
        [Display(Name ="Message Title")]
        [StringLength(maximumLength: 250,MinimumLength =10,ErrorMessage ="From 10 to 250 charcters")]
       [Required(ErrorMessage ="Required")]
        public string Title { get; set; }
        public bool IsRead { get; set; }
        public bool IsReplayed { get; set; }

        [StringLength(maximumLength: 256)]
        public string FileName { get; set; }
        [RegularExpression(@"^((([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", ErrorMessage = "Not Email Address")]
        [StringLength(100, ErrorMessage = "Text must not more than 100 Characters")]
        [Display(Name ="Email")]
       [Required(ErrorMessage ="Required")]
        public string Email { get; set; }
        [RegularExpression(@"0\d{9,18}", ErrorMessage = "Input Error")]
        [Display(Name ="Mobile")]
       [Required(ErrorMessage ="Required")]
        public string Mobile { get; set; }
        public double ReferenceCode { get; set; }
        public int JournalId { get; set; }
        public int PageNumber { get; set; }
        public string  JournalContactInfo { get; set; }
    }
}
