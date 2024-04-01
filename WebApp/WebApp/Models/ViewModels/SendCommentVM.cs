using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class SendCommentVM
    {
        [Display(Name = "Comment")]
        [Required(ErrorMessage = "Required")]

        [StringLength(maximumLength: 1000, MinimumLength = 2, ErrorMessage = "From 2 to 1000 Characters")]
        public string Comment { get; set; }
        public string MessageEmailTitle { get; set; }

        public IFormFile File { get; set; }
        public string ResearchId { get; set; }
        public int StageId { get; set; }
        public string UserId { get; set; }
        public int ResId { get; set; }
        public bool UpdateData { get; set; }
        public bool CompleteData { get; set; }
        public bool SendToAuthorEmail { get; set; }
        public bool SendToAuditorEmail { get; set; }





    }
}
