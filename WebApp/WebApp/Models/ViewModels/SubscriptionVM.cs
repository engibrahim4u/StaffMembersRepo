using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class SubscriptionVM
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[ء-يA-Za-z\s]{2,25}", ErrorMessage = "Arabic and English Charcters Only (2-25)")]
        public string FirstName { get; set; }
        [Display(Name = "Father Name")]
        [RegularExpression(@"[ء-يA-Za-z\s]{2,25}", ErrorMessage = "Arabic and English Charcters Only (2-25)")]
        public string FatherName { get; set; }
        [Display(Name = "Family Name")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[ء-يA-Za-z\s]{2,25}", ErrorMessage = "Arabic and English Charcters Only (2-25)")]
        public string FamilyName { get; set; }
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^((([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", ErrorMessage = "Not Email Address")]
        [StringLength(100, ErrorMessage = "Text must not more than 100 Characters")]
        public string Email { get; set; }
        [StringLength(maximumLength: 50)]
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Required")]
        public string Gender { get; set; }
        [Display(Name = "National Id")]
        //[Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-z0-9\s\.\-]{3,250}", ErrorMessage = "Input Error")]
        public string NationalId { get; set; }
        [Display(Name = "POB")]
        //[Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-z0-9ء-ي\s\.\-]{3,250}", ErrorMessage = "Like(POB 123456 Town Name 123456)")]
        public string POB { get; set; }
        [Display(Name = "Address")]
        //[Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-z0-9ء-ي\s\.\-]{3,250}", ErrorMessage = "Input Error")]
        public string Address { get; set; }
        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"0\d{9,18}", ErrorMessage = "Input Error")]
        public string Mobile { get; set; }
        [Display(Name = "Fax")]
        [RegularExpression(@"\d{9,18}", ErrorMessage = "Input Error")]
        public string Fax { get; set; }
        [Display(Name = "Phone")]
        [RegularExpression(@"\d{9,18}", ErrorMessage = "Input Error")]
        public string Phone { get; set; }
        [Display(Name = "Work Phone")]
        [RegularExpression(@"\d{9,18}", ErrorMessage = "Input Error")]
        public string WorkPhone { get; set; }
        [Display(Name = "Work Fax")]
        [RegularExpression(@"\d{9,18}", ErrorMessage = "Input Error")]
        public string WorkFax { get; set; }
        [Display(Name = "Work Address")]
        //[Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-z0-9ء-ي\s\.\-]{3,250}", ErrorMessage = "Input Error")]
        public string WorkAddress { get; set; }
        [Display(Name = "Subscription Period")]
        [Required(ErrorMessage = "Required")]
        public string SubscriptionPeriod { get; set; }
        [StringLength(maximumLength: 50)]
        public string SubscriptionType { get; set; }
        public float Amount { get; set; }
        public string TransferImage { get; set; }
        [Display(Name = "Birth Date")]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public DateTime AddDate { get; set; }
        public bool Confirmed { get; set; }
        public int JournalId { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Required")]
        public int CountryId { get; set; }
        [Display(Name = "Bank Tranfer Image(pdf)")]
        [Required(ErrorMessage = "Required")]
        public IFormFile TransferImageFile { get; set; }
        public float SubscriptionPersonalValueInsideSA { get; set; }
        public float SubscriptionPersonalValueOutsaideSA { get; set; }
        public float SubscriptionInstitutionsValueInsideSA { get; set; }
        public float SubscriptionInstitutionsValueOutsaideSA { get; set; }
        public List<Country> Countries { get; set; }
        public List<NameIdValueVM> SubscriptionPeriods { get; set; }
        public string TransferBankAccount { get; set; }


    }
}
