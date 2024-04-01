using WebApp.Models;
using WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Helper
{

    public class Services
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public Services(IWebHostEnvironment _hostingEnvironment)
        {
            hostingEnvironment = _hostingEnvironment;
        }
        public void SaveErrorsPasswordsToFile(string error)
        {
            try
            {
                var filePath = Path.Combine(hostingEnvironment.ContentRootPath, "Errors", "error_Password.txt");
                File.AppendAllText(@filePath, error);
            }
            catch (Exception ex)
            {

            }
        }

        public void SaveErrorsToFile(string error)
        {
            try
            {
                var filePath = Path.Combine(hostingEnvironment.ContentRootPath, "Errors", "error.txt");
                File.AppendAllText(@filePath, error);
            }
            catch (Exception ex)
            {

            }
        }

       
    }
}
