using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class SignaturesVM
    {
        public string OldAdminSignature { get; set; }
        public string OldEditorialHeadSignature { get; set; }
        public string OldJournalStampSignature { get; set; }
        public int journalId { get; set; }
        public int journalIdHead { get; set; }
        public int journalIdStamp { get; set; }

        [Required]
        public IFormFile AdminSignature { get; set; }
       [Required]
        public IFormFile EditorialHeadSignature { get; set; }
       [Required]
        public IFormFile JournalStampSignature { get; set; }

    }
}
