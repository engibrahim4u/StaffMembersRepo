using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class PersonalDataVM
    {
        public int Id { get; set; }
        public string EncryptedId { get; set; }
        public string BaseUrl { get; set; }
        public string PrintPath { get; set; }
        public bool ExamPassed { get; set; }



        [Display(Name = "E-mail")]
        public string UserName { get; set; }
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Display(Name = "First Name(Ar)")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[ء-ي\s]{2,25}", ErrorMessage = "Arabic Letters Only (2-25)")]
        //[RegularExpression(@"[ء-ي]{2,15}[\s[ء-ي]{1,15}]?", ErrorMessage = "Arabic Letters Only (2-25)")]
        public string FirstNameAr { get; set; }
        [Display(Name = "Second Name(Ar)")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[ء-ي\s]{2,25}", ErrorMessage = "Arabic Letters Only (2-25)")]
        public string SecondNameAr { get; set; }
        [Display(Name = "Third Name(Ar)")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[ء-ي\s]{2,25}", ErrorMessage = "Arabic Letters Only (2-25)")]
        public string ThirdNameAr { get; set; }
        [Display(Name = "Family Name(Ar)")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[ء-ي\s]{2,25}", ErrorMessage = "Arabic Letters Only (2-25)")]
        public string FamilyNameAr { get; set; }
        [Display(Name = "Full Name(Ar)")]
        public string FullNameAr { get; set; }
        [Display(Name = "First Name(En)")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Z]{1}[a-z]{1,24}", ErrorMessage = "English Letters Only (2-25) and First Letter Capital")]
        public string FirstNameEn { get; set; }
        [Display(Name = "Second Name(En)")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Z]{1}[a-z]{1,24}", ErrorMessage = "English Letters Only (2-25) and First Letter Capital")]
        public string SecondNameEn { get; set; }
        [Display(Name = "Third Name(En)")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Z]{1}[a-z]{1,24}", ErrorMessage = "English Letters Only (2-25) and First Letter Capital")]
        public string ThirdNameEn { get; set; }
        [Display(Name = "Family Name(En)")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Z]{1}[a-z]{1,24}", ErrorMessage = "English Letters Only (2-25) and First Letter Capital")]
        public string FamilyNameEn { get; set; }
        [Display(Name = "Full Name(En)")]
        public string FullNameEn { get; set; }
        [Display(Name = "Nationality")]
        [Required(ErrorMessage = "Required")]
        public int NationalityId { get; set; }
        [Display(Name = "National ID")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[2-3]\d{13}", ErrorMessage = "Check National ID")]
        //[Remote(action: "IsNationalIdExsist", controller: "Trainee")]
        public string NationalId { get; set; }

        [Display(Name = "Passport Number")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"[A-Za-z0-9]{8,25}", ErrorMessage = "Check Passport Number")]
        //[Remote(action: "IsPassportNumberExsist", controller: "Trainee")]
        public string PassportNumber { get; set; }
        [StringLength(100)]
        public string Photo { get; set; }
        public string NationalIdImage { get; set; }
        [Display(Name = "Academic Degree")]
        [Required(ErrorMessage = "Required")]
        public int AcademicDegreeId { get; set; }

        [Display(Name = "From")]
        [Required(ErrorMessage = "Required")]
        public int PlaceId { get; set; }
        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }
        [StringLength(maximumLength: 25)]
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Choose")]
        public string Kind { get; set; }
        public string Name { get; set; }
        public bool TraineeDataConfirmed { get; set; }
        public bool AdminDataConfirmed { get; set; }
        public string FacultyName { get; set; }
        public string AcademicDegreeName { get; set; }
        public string GroupType { get; set; }
        public string GroupName { get; set; }
        public string CourseName { get; set; }

        public string NationalityName { get; set; }

        [Display(Name = "سعر الدورة التدريبية")]
        public double Cost { get; set; }
        [Display(Name = "تكلفة الدورة علي المتدرب")]
        public double PayedAmount { get; set; }

        public string LetterCost { get; set; }

        public double OnlineCost { get; set; }
        [Display(Name = "نسبة الخصم ")]
        [Range(minimum:0,maximum:100,ErrorMessage ="خطأ")]
        public double Discount { get; set; }

        public string AdminPrintingName { get; set; }
        public string SupervisorName { get; set; }
        [Display(Name = "رقم ايصال الدفع")]
        //[Required(ErrorMessage = "مطلوب")]
        public string PaymentNumber { get; set; }


        [Display(Name = "اسم الدورة")]
        [Required(ErrorMessage = "مطلوب")]
        public int CourseId { get; set; }
        [Display(Name = "رقم المجموعة")]
        //[Required(ErrorMessage = "مطلوب")]
        public int CourseGroupId { get; set; }

        [Display(Name = "الرقم المسلسل")]
        public Nullable<int> SerialNumber { get; set; }
        public int OldCourseGroupId { get; set; }
        public int CourseGroupRegisterCount { get; set; }


        public bool PersonalPhotoRequired { get; set; }
        public bool NationalIdImageRequired { get; set; }
        [Display(Name = "CV")]
        public string CV { get; set; }
        [Display(Name = "Visa Number")]
        [Required(ErrorMessage = "Required")]
        public string VisaNumber { get; set; }
        [Display(Name = "CV File")]
        [Required(ErrorMessage = "Required")]
        public IFormFile CVUpFile { get; set; }
        [Display(Name = "National ID File")]
        [Required(ErrorMessage = "Required")]
        public IFormFile StudentNationalIdUpFile { get; set; }
        [Display(Name = "National ID File(If need to replace old one)")]
        public IFormFile StudentNationalIdUpNewFile { get; set; }

        [Display(Name = "Personal Photo File")]
        [Required(ErrorMessage = "Required")]
        public IFormFile StudentPhotoUpFile { get; set; }
        [Display(Name = "New Personal Photo File(If need to replace old one)")]
        public IFormFile StudentPhotoUpNewFile { get; set; }
        public List<AcademicDegree> AcademicDegrees { get; set; }
        public List<Nationality> Nationalities { get; set; }
        public List<Place> Places { get; set; }



    }
}
