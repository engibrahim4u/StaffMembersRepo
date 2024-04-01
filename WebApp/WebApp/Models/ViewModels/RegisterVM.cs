using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Registraion Type")]
        [Required(ErrorMessage = "Required")]
        public string RegisterType { get; set; }
        public string Id { get; set; }
        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"0\d{9,18}", ErrorMessage = "Input Error")]
        [Remote(action: "IsMobileExsist", controller: "Account")]
        public string Mobile { get; set; }

      
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^((([a-zA-Z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", ErrorMessage = "Not Email Address")]
        [StringLength(50, ErrorMessage = "Text must not more than 50 Characters")]
        [Remote(action: "IsUserExsist", controller: "Account")]
        public string Email { get; set; }
        [Display(Name = "Password")]

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[a-zA-Z0-9@$_.%#&]{8,12}", ErrorMessage = "Password contain  8-12 English Characters and digits and special Characters like @$_.%#&")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password not Same")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[ء-يA-Za-z\s]{2,25}", ErrorMessage = "Arabic and English Charcters Only (2-25)")]
        public string FirstName { get; set; }
        [Display(Name = "Father Name")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[ء-يA-Za-z\s]{2,25}", ErrorMessage = "Arabic and English Charcters Only (2-25)")]
        public string FatherName { get; set; }
        [Display(Name = "Grand Father Name")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[ء-يA-Za-z\s]{2,25}", ErrorMessage = "Arabic and English Charcters Only (2-25)")]
        public string GrandFatherName { get; set; }
        [Display(Name = "Family Name")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[ء-يA-Za-z\s]{2,25}", ErrorMessage = "Arabic and English Charcters Only (2-25)")]
        public string FamilyName { get; set; }
        [Display(Name = "Full Name")]
        [StringLength(100, ErrorMessage = "Max of 100 Charcter")]
        [RegularExpression(@"[ء-ي]{2,20}\s[ء-ي]{1,20}[\s[ء-ي]{1,20}]*", ErrorMessage = "Name in Arabic")]
        [Required(ErrorMessage = "Required")]
        public string FullName { get; set; }

        [Display(Name = "University")]
        //[Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "Max of 100 Charcter")]
        [RegularExpression(@"[ء-يA-Za-z]{2,20}\s[ء-يA-Za-z]{1,20}[\s[ء-يA-Za-z]{1,20}]*", ErrorMessage = "Name in Arabic or English")]

        public string University { get; set; }

        [Display(Name = "General Specialization")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-zء-ي\s]{3,250}", ErrorMessage = "Arabic and English Characters Only (3-250)")]
        public string GSpeciality { get; set; }

        [Display(Name = "Specialization")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-zء-ي\s]{3,250}", ErrorMessage = "Arabic and English Characters Only (3-250)")]
        public string  MSpeciality{ get; set; }
        [Display(Name = "IBAN")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-z0-9]{5,50}", ErrorMessage = "Numbers and English Characters Only (5-50)")]
        public string IBAN { get; set; }

        [Display(Name = "Swift Code")]
        //[Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-z0-9\-]{2,50}", ErrorMessage = "Numbers and English Characters Only (2-50)")]
        public string SwiftCode { get; set; }

        [Display(Name = "Branch Code")]
        //[Required(ErrorMessage = "Required")]
        [StringLength(100, ErrorMessage = "Max of 100 Charcter")]

        //[RegularExpression(@"[A-Za-z0-9\-]{5,50}", ErrorMessage = "Numbers and English Characters Only (5-50)")]
        public string BranchCode { get; set; }

        [Display(Name = "Bank Name")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-zء-ي\s]{5,250}", ErrorMessage = "Arabic and English Characters Only (5-250)")]
        public string Bank { get; set; }

        [Display(Name = "Bank City")]
        //[Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-zء-ي\s]{3,250}", ErrorMessage = "Arabic and English Characters Only (3-250)")]
        public string BankCity { get; set; }

        [Display(Name = "POB")]
        //[Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-z0-9ء-ي\s\.\-]{3,250}", ErrorMessage = "Like(POB 123456 Town Name 123456)")]
        public string POB { get; set; }

        [Display(Name = "Name In Bank(Arabic)")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[ء-ي\s]{8,150}", ErrorMessage = "Arabic Letters Only (8-150)")]
        public string NameInBank { get; set; }
        [Required(ErrorMessage = "Required")]

        [Display(Name = "Name In Bank(English)")]
        [StringLength(maximumLength: 100, MinimumLength = 8, ErrorMessage = "From 8 to 100 Characters")]
        [RegularExpression(@"[A-Za-z\s]{8,150}", ErrorMessage = "Name must be English Characters")]
        public string EnNameInBank { get; set; }
        [Display(Name = "Author Instructions")]
        public int AuthorInstructions { get; set; }

        [Display(Name = "Reviewer Instructions")]
        public int AuditorInstructions { get; set; }
        [Display(Name = "Journal")]
        [Required(ErrorMessage = "Required")]
        public int JournalId { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Required")]
        public int CountryId { get; set; }

        [Display(Name = "Academic Degree")]
        [Required(ErrorMessage = "Required")]
        public int AcademicDegreeId { get; set; }

        [Display(Name = "Scientific Level")]
        [Required(ErrorMessage = "Required")]
        public int ScientificLevelId { get; set; }

        [Display(Name = "Language")]
        [Required(ErrorMessage = "Required")]
        public string LanguageId { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        [Display(Name = "Academic Degree")]
        public string AcademicDegreeName { get; set; }

        [Display(Name = "Scientific Level")]
        public string ScientificLevelName { get; set; }

        [Display(Name = "Language")]
        public string LanguageName { get; set; }
        [Required(ErrorMessage = "Click Confirm")]
        public string ConfirmData { get; set; }

        //[Required(ErrorMessage = "Click Confirm")]
        public bool Confirmed { get; set; }
        public bool Agreement { get; set; }
        public List<Country> Countries { get; set; }

        public List<AcademicDegree> AcademicDegrees { get; set; }
        public List<ScientificLevel> ScientificLevels { get; set; }
        public List<Language> Languages { get; set; }
        public string ReviewAgreement { get; set; }
        public string ReviewAgreementEn { get; set; }
        public string ResearchAgreement { get; set; }
        public string ResearchAgreementEn { get; set; }






    }
}
