using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Repository;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = "superAdmin,supervisor")]

    public class StatisticController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public StatisticController(UserManager<ApplicationUser> _userManager
                              

            )
        {
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

   

    }
}
