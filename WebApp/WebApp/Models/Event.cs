using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public bool DisplayInHome { get; set; }
        public bool DisplayInHomeEn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CDate { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [StringLength(maximumLength: 300)]
        public string Header { get; set; }
        [StringLength(maximumLength: 300)]
        public string HeaderEn { get; set; }
        public string Content { get; set; }
        public string ContentEn { get; set; }
        public string ExternalUrl { get; set; }

        //public int JournalId { get; set; }
        //public Journal Journal { get; set; }

    }
}
