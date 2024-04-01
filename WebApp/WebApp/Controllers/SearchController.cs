using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Repository;
using WebApp.Models.ViewModels;
using WebApp.Security;

namespace WebApp.Controllers
{
    [Authorize(Roles = "superAdmin,admin,supervisor")]

    public class SearchController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
      
        private readonly ISettingRepository settings;
        private readonly IPlaceRepository places;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IStringLocalizer<SearchController> localizer;
        private readonly IDataProtector protector;

        public SearchController(UserManager<ApplicationUser> _userManager
                              
                                    ,ISettingRepository _settings
                                    , IDataProtectionProvider dataProtectionProvider
                                    , DataProtectionPurposeStrings dataProtectionPurposeStrings
                                    , IWebHostEnvironment _hostEnvironment
                                    , IStringLocalizer<SearchController> _localizer)
        {
            userManager = _userManager;
         
            settings = _settings;
            hostEnvironment = _hostEnvironment;
            localizer = _localizer;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.SiteSecurity);

        }
        public IActionResult Index()
        {
            return RedirectToAction("Search");
        }

        public IActionResult Search()
        {
           var siteBaseUrl = $"{this.Request.PathBase}";
            SearchVM model = new SearchVM()
            {
                BaseUrl = siteBaseUrl
            };
            return View(model);
        }

       

        //[Authorize(Roles = "superAdmin")]
     

     

      

      

        
    
     
     
        private string PrintPath(int courseTypeId)
        {
            switch (courseTypeId)
            {
                case 1:
                    return "PrintTraineeDataGrantFile";
                case 2:
                    return "PrintTraineeDataUpgradeFile";
                case 3:
                    return "PrintTraineeDataExam";
                default:
                    return "";
            
            }

        }
       


    }
}
