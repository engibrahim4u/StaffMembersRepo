using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class EventImageVM
    {
        public int Id { get; set; }
        public string EventPageName { get; set; }
        [Range(minimum:1,maximum:100,ErrorMessage ="Error")]
        public int Order { get; set; }
        [Display(Name = "صورة")]
        [Required(ErrorMessage = "اختر الملف")]
        public IFormFile File { get; set; }
        public int Category { get; set; }
        public int EventId { get; set; }
        public List<EventImage> EventImages { get; set; }
    }
}
