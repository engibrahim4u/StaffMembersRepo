using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.EmailService
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }

        public Message(string toAddress,IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            To = new List<MailboxAddress>();

            //To.AddRange(to.Select(x => new MailboxAddress("","")));
            To.Add(new MailboxAddress("", toAddress));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
