using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApp.Models.ViewModels
{
    public class EMailEntityVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string MsgBody { get; set; }
        public string MsgTitle { get; set; }
        public int JournalId { get; set; }
        public bool  SendEmail { get; set; }
        public string FileNames { get; set; }
        public List<Attachment> AttachmentFiles { get; set; }
        public string LangId { get; set; }

    }
}
