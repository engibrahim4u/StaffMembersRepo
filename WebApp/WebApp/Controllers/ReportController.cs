using AspNetCore.Reporting;
using AspNetCore.Reporting.ReportExecutionService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Repository;
using WebApp.Models.ViewModels;
using WebApp.Security;
using WebApp.Helper;
using WebApp.Services;

namespace WebApp.Controllers
{
    //[Authorize(Roles = "superAdmin,admin,supervisor")]

    public class ReportController : Controller
    {
        private readonly ISettingRepository settings;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IDataProtector protector;

        public ReportController(ISettingRepository _settings,
                                    UserManager<ApplicationUser> _userManager,
                                  IWebHostEnvironment _hostingEnvironment,
                                 IDataProtectionProvider dataProtectionProvider,
                                 DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            settings = _settings;
            userManager = _userManager;
            hostingEnvironment = _hostingEnvironment;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.SiteSecurity);

        }
        public IActionResult Index()
        {
            return RedirectToAction("PrintSettings()");
            //return View();
        }

        //public IActionResult PrintSettings()
        //{
        //    try
        //    {
        //        int id = 1;
        //    string mimtype = "";
        //    int extension = 1;
        //    var path = $"{hostingEnvironment.ContentRootPath}\\Reports\\rpSettings.rdlc";
        //    LocalReport localReport = new LocalReport(path);
        //    localReport.AddDataSource("DsSetting", Settings(id));
        //    var result = localReport.Execute(RenderType.Pdf, extension, null, mimtype);
        //    return File(result.MainStream, "application/pdf");

        //    }
        //    catch (Exception ex)
        //    {
        //        AddErrors(ex.Message);
        //    }
        //    return null;
        //}
        //public IActionResult PrintTraineeDataUpgrade(string id)
        //{
        //    if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Search");
        //    try
        //    {
        //        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        //        int traineeId =Convert.ToInt32(protector.Unprotect(id));
        //        string mimtype = "";
        //        int extension = 1;
        //        var traineeData = TraineeData(traineeId);
        //        var nationalIdImage = traineeData.SingleOrDefault().NationalIdImage;
        //        var photoImage = traineeData.SingleOrDefault().Photo;

        //        var path = $"{hostingEnvironment.ContentRootPath}\\Reports\\rpTraineeData_Upgrade.rdlc";
        //        var nationalIdImagePath= $"{hostingEnvironment.WebRootPath}\\UploadedImages\\"+ nationalIdImage;
        //        var photoImagePath = $"{hostingEnvironment.WebRootPath}\\UploadedImages\\" + photoImage;

        //        var paramNationalIdImageValue = ImageToBase64(nationalIdImagePath);
        //        var paramPhotoImageValue = ImageToBase64(photoImagePath);


        //        LocalReport localReport = new LocalReport(path);
        //        localReport.AddDataSource("DsTraineeData", traineeData);
        //        keyValuePairs.Add("nationalIdImage", paramNationalIdImageValue);
        //        keyValuePairs.Add("photoImage", paramPhotoImageValue);

        //        var result = localReport.Execute(RenderType.Pdf, extension, keyValuePairs, mimtype);
        //        string fileName = SavePdfFile(result.MainStream);
        //        return File(result.MainStream, "application/pdf");


        //    }
        //    catch (Exception ex)
        //    {
        //        AddErrors(ex.Message);
        //    }
        //    return null;
        //}

      
        //public IActionResult ExportTraineeUserLogin(string id)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Search");
        //        int groupId = Convert.ToInt32(protector.Unprotect(id));
        //        string mimtype = "";
        //        int extension = 1;
        //        var path = $"{hostingEnvironment.ContentRootPath}\\Reports\\rpTraineeUserLoginList.rdlc";
        //        LocalReport localReport = new LocalReport(path);
        //        localReport.AddDataSource("DsTraineeReportData", TraineeReportData (groupId));
        //        var result = localReport.Execute(RenderType.Excel, extension, null, mimtype);
        //        return File(result.MainStream, "application/msexcel", "userLogin_G"+ groupId+ DateTime.Now.ToString("_dd-MM-yyyy_HH_mm_ss") + ".xls");

        //    }
        //    catch (Exception ex)
        //    {
        //        AddErrors(ex.Message);
        //    }
        //    return null;

        //}

       
        private List<Setting> Settings(int id)
        {
            var list = new List<Setting>();
            list.Add(new Setting { StartDate = DateTime.Now, EndDate = DateTime.Now });
            list.Add(new Setting { StartDate = DateTime.Now, EndDate = DateTime.Now });
            list.Add(new Setting { StartDate = DateTime.Now, EndDate = DateTime.Now });
            list.Add(new Setting { StartDate = DateTime.Now, EndDate = DateTime.Now });
            list.Add(new Setting { StartDate = DateTime.Now, EndDate = DateTime.Now });

            return list;
        }
 

        private string PrintingUser()
        {
            return  userManager.GetUserAsync(User).Result.Name;
        }

        private void AddErrors(string error)
        {
           WebApp.Helper. Services serv = new WebApp.Helper.Services(hostingEnvironment);
            serv.SaveErrorsToFile(DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss ") + error);
        }


        public string ImageToBase64(string _imagePath)
        {

            string image_path = _imagePath;
            if (!System.IO.File.Exists(image_path))
            {
                var nationalIdImagePath_no = $"{hostingEnvironment.WebRootPath}\\UploadedImages\\no-nationalId-image-available.png";
                byte[] byes_array_no = System.IO.File.ReadAllBytes(nationalIdImagePath_no);
                string base64String_no = Convert.ToBase64String(byes_array_no);
                return base64String_no;
            }
            byte[] byes_array = System.IO.File.ReadAllBytes(image_path);
            string base64String = Convert.ToBase64String(byes_array);
            return base64String;
        }

        //public string PrintTraineeDataUpgradeFile(string id)
        //{
        //    //if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Search");
        //    try
        //    {
        //        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        //        int traineeId = Convert.ToInt32(protector.Unprotect(id));
        //        string mimtype = "";
        //        int extension = 1;
        //        var traineeData = TraineeData(traineeId);
        //        var nationalIdImage = traineeData.SingleOrDefault().NationalIdImage;
        //        var photoImage = traineeData.SingleOrDefault().Photo;

        //        var path = $"{hostingEnvironment.ContentRootPath}\\Reports\\rpTraineeData_Upgrade.rdlc";
        //        var nationalIdImagePath = $"{hostingEnvironment.WebRootPath}\\UploadedImages\\" + nationalIdImage;
        //        var photoImagePath = $"{hostingEnvironment.WebRootPath}\\UploadedImages\\" + photoImage;

        //        var paramNationalIdImageValue = ImageToBase64(nationalIdImagePath);
        //        var paramPhotoImageValue = ImageToBase64(photoImagePath);


        //        LocalReport localReport = new LocalReport(path);
        //        localReport.AddDataSource("DsTraineeData", traineeData);
        //        keyValuePairs.Add("nationalIdImage", paramNationalIdImageValue);
        //        keyValuePairs.Add("photoImage", paramPhotoImageValue);

        //        var result = localReport.Execute(RenderType.Pdf, extension, keyValuePairs, mimtype);
        //        string fileName = SavePdfFile(result.MainStream);
        //        return fileName;

        //    }
        //    catch (Exception ex)
        //    {
        //        AddErrors(ex.Message);
        //    }
        //    return null;
        //}

      
       

     
        //[Authorize(Roles = "superAdmin,admin")]
        //public IActionResult PrintTakenCertificateList(string id)
        //{
        //    if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Search");
        //    try
        //    {


        //        int groupId = Convert.ToInt32(protector.Unprotect(id));
        //        string mimtype = "";
        //        int extension = 1;
        //        var path = $"{hostingEnvironment.ContentRootPath}\\Reports\\rpTraineeTakeCertificateList.rdlc";

        //        LocalReport localReport = new LocalReport(path);
        //        localReport.AddDataSource("DsTraineeReportData", TraineeReportData(groupId));
        //        var result = localReport.Execute(RenderType.Pdf, extension, null, mimtype);

        //        return File(result.MainStream, "application/pdf");

        //    }
        //    catch (Exception ex)
        //    {
        //        AddErrors(ex.Message);
        //    }
        //    return null;
        //}

        //[Authorize(Roles = "superAdmin,admin")]
        //public IActionResult PrintTraineeUsersPasswords(string id)
        //{
        //    if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Search");
        //    try
        //    {


        //        int groupId = Convert.ToInt32(protector.Unprotect(id));
        //        string mimtype = "";
        //        int extension = 1;
        //        var path = $"{hostingEnvironment.ContentRootPath}\\Reports\\rpTraineeUsersPasswords.rdlc";

        //        LocalReport localReport = new LocalReport(path);
        //        localReport.AddDataSource("DsTraineeReportData", TraineeReportData(groupId));
        //        var result = localReport.Execute(RenderType.Pdf, extension, null, mimtype);

        //        //return File(result.MainStream, "application/pdf", "UsersPasswords_G" + groupId + DateTime.Now.ToString("_dd-MM-yyyy_HH_mm_ss") + ".pdf");
        //        return File(result.MainStream, "application/pdf");


        //    }
        //    catch (Exception ex)
        //    {
        //        AddErrors(ex.Message);
        //    }
        //    return null;
        //}
    
        private string ExamCount(int groupTypeId)
        {
            if (groupTypeId == 1) return "( 7 )";
            else if (groupTypeId == 2) return "( 2 )";
            else return "";
        }
   
        private string ExtraCertificateLetterData(int certificateCount, float certificatePrice)
        {
            string text = "";
            text += "ارجوا من سيادتكم التكرم بتحصيل مبلغ (";
            text += certificateCount * certificatePrice;
            text += ") مقابل استخراج عدد (";
            text += certificateCount;
            text += ") نسخة إضافية  ";
            return text;
        }
     
        private string ShowNationalIdImage(bool displayNationalIdImage)
        {
            return displayNationalIdImage ? "Yes" : "No";
        }

        private string NationalOrPassportData(string nationalId, string passportNumber, int nationality)
        {
            return (nationality == 1) ? passportNumber : nationalId;
        }

    
        private string SavePdfFile(byte[] bytes)
        {
            var fileName = "file_" + Guid.NewGuid().ToString() + ".pdf";
            string filePath = Path.Combine(hostingEnvironment.WebRootPath, "TempFiles", fileName);
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }

            return fileName;
        }

    }
}
