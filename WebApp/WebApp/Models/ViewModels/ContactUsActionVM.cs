using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class ContactUsActionVM
    {
        public ContactUsVM ContactMessageInfo { get; set; }
        public List<ContactUsVM> ContactMessageReplay { get; set; }
        public double ReferenceCode { get; set; }
        [Required(ErrorMessage ="مطلوب")]
        public string Replay { get; set; }
        public int PageNumber { get; set; }
        public int JournalId { get; set; }

    }
}
