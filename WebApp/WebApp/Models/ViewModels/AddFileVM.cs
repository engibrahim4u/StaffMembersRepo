using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class AddFileVM
    {
        [Display(Name = "Reseach Corrected File(word)")]
        [Required(ErrorMessage = "Select file")]
        public IFormFile File { get; set; }
        public string ResearchId { get; set; }
        public int ResId { get; set; }
    }
}
