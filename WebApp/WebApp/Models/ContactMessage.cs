using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CDate { get; set; }
        [StringLength(maximumLength: 100)]
        public string ReceiverName { get; set; }
        [StringLength(maximumLength: 100)]
        public string SenderName { get; set; }
        [StringLength(maximumLength: 256)]
        public string Title { get; set; }
        public bool IsRead { get; set; }
        public bool IsReplied { get; set; }

        [StringLength(maximumLength: 256)]
        public string FileName { get; set; }
        [StringLength(maximumLength: 250)]
        public string Email { get; set; }
        [StringLength(maximumLength: 20)]
        public string Mobile { get; set; }
        public double ReferenceCode { get; set; }
        public int JournalId { get; set; }
    }
}
