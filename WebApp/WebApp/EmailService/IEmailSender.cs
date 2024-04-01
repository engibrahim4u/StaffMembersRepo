using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.EmailService
{
   public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);
        Task SendEmailAsync(Message message,string fileName,byte[] attachmentBinaryData);

    }
}
