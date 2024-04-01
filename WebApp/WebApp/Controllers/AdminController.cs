using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Repository;
using WebApp.Models.ViewModels;
using WebApp.Security;

namespace WebApp.Controllers
{
    [Authorize(Roles = "superAdmin,admin")]
    [Authorize(Roles = "superAdmin,admin,supervisor")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INationalityRepository nationalities;
        private readonly IAcademicDegreeRepository academicDegrees;
        private readonly IPlaceRepository places;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IStringLocalizer<AdminController> localizer;
        private readonly IDataProtector protector;

        public AdminController(UserManager<ApplicationUser> _userManager
                                    , INationalityRepository _nationalities
                                    , IAcademicDegreeRepository _academicDegrees
                                    , IPlaceRepository _places
                                    , IDataProtectionProvider dataProtectionProvider
                                    , DataProtectionPurposeStrings dataProtectionPurposeStrings
                                    , IWebHostEnvironment _hostEnvironment
                                    , IStringLocalizer<AdminController> _localizer)
        {
            userManager = _userManager;
            nationalities = _nationalities;
            academicDegrees = _academicDegrees;
            places = _places;
            hostEnvironment = _hostEnvironment;
            localizer = _localizer;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.SiteSecurity);

        }
        public IActionResult Index()
        {
           return RedirectToAction("Index", "Account");
            //return View();
        }

   

      
        
   
    }
}
