using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Consulting()
        {
            return View();
        }
        public IActionResult Training()
        {
            return View();
        }
        public IActionResult Performance()
        {
            return View();
        }
        public IActionResult Computer()
        {
            return View();
        }
        public IActionResult Financial()
        {
            return View();
        }
    }
}
