using WebApp.EmailService;
using WebApp.Helper;
using WebApp.Models;
using WebApp.Models.Repository;
using WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Authorize(Roles = "superAdmin,admin")]

    public class ContactMessagesController : Controller
    {
        private readonly IContactMessageRepository contactMessages;
        private readonly IEmailSender emailSender;
        private readonly ILogger<HomeController> logger;
        private readonly IStringLocalizer<ContactMessagesController> localizer;

        public ContactMessagesController(IContactMessageRepository _contactMessages
                                 , IEmailSender _emailSender
                                , ILogger<HomeController> _logger
                             , IStringLocalizer<ContactMessagesController> _localizer)
        {
            contactMessages = _contactMessages;
            emailSender = _emailSender;
            logger = _logger;
            localizer = _localizer;
        }
        public IActionResult Index(int pag = 1)
        {
            int journalId = 1;
            var emails =contactMessages.List(journalId);
            const int pageSize = 15;
            if (pag < 1)
                pag = 1;
            int recordCount = emails.Count();
            var pager = new Pager(recordCount, pag, pageSize, "Index", "ContactMessages");
            int recordSkip = (pag - 1) * pageSize;
            var data = emails.Skip(recordSkip).Take(pageSize)
                .Select(x => new ContactUsVM
                {
                      Title=x.Title,
                      IsRead=x.IsRead,
                      IsReplayed=x.IsReplied,
                      PageNumber=pag,
                      ReferenceCode=x.ReferenceCode
                }).ToList();
            ViewBag.Pager = pager;
            return View(data);
        }

        public async Task< ActionResult> Info(double id, int pag)
        {
            if (id == 0) return RedirectToAction("Index");
            var MessageContactData = contactMessages.GetByReferenceCode(id);
            if(!MessageContactData.Any()) RedirectToAction("Index");
            ContactUsActionVM model = new ContactUsActionVM();
            var contactMessageInfo = MessageContactData.FirstOrDefault(x => !string.IsNullOrEmpty(x.Email));
                if(contactMessageInfo == null)
                return RedirectToAction("Index");
            contactMessageInfo.IsRead = true;
            await contactMessages.Update(contactMessageInfo);
            model.ContactMessageInfo = MessageInfo(contactMessageInfo);
            model.ContactMessageReplay = MessageReplayes( MessageContactData.Where(x => string.IsNullOrEmpty(x.Email)).ToList());
            model.PageNumber = pag;
            model.ReferenceCode = contactMessageInfo.ReferenceCode;
            model.JournalId = contactMessageInfo.JournalId;
            return View(model);
        }

        // POST: SiteSettingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Info(ContactUsActionVM model)
        {
            if (model.ReferenceCode == 0) return RedirectToAction("Index");
            var MessageContactData = contactMessages.GetByReferenceCode(model.ReferenceCode);
            if (!MessageContactData.Any()) RedirectToAction("Index");

            try
            {
                ContactMessage contactMessage = new ContactMessage();
                contactMessage.ReferenceCode = model.ReferenceCode;
                contactMessage.Message = model.Replay;
                contactMessage.JournalId = model.JournalId;
                contactMessage.CDate = MyTimeZone.Now;
               await contactMessages.Add(contactMessage);
                var currentContactMessage = MessageContactData.FirstOrDefault(x => !string.IsNullOrEmpty(x.Email));
                if (currentContactMessage != null)
                {
                    currentContactMessage.IsReplied = true;
                    await contactMessages.Update(currentContactMessage);
                   await SendEmail(currentContactMessage.Email, currentContactMessage.Title, currentContactMessage.Message,model.Replay);
                }
                    ViewBag.Success = true;
            }

            
            catch (Exception ex)
            {
                ViewBag.Fail = true;

            }
            return View(model);
        }

        private ContactUsVM MessageInfo(ContactMessage contactMessage)
        {
            ContactUsVM contact = new ContactUsVM
            {
                CDate = contactMessage.CDate,
                Email = contactMessage.Email,
                Message = contactMessage.Message,
                Mobile = contactMessage.Mobile,
                Title = contactMessage.Title,
                SenderName = contactMessage.SenderName
            };
            return contact;
            
        }

        private List<ContactUsVM> MessageReplayes(List<ContactMessage> contactMessageReplayes)
        {
            return contactMessageReplayes.Select(x => new ContactUsVM
            {
                CDate = x.CDate,
                Message = x.Message
            }).ToList();

        }

        private async Task SendEmail(string email, string title, string msg,string replay)
        {
            string content = "الرد على الرسالة التالية";
            content += "<br />" + title + " <br />" + msg;
            content += "<br />" + "(" + replay + ")";
            string subject = "الرد على  رسالة مرسلة ل  " + Constants.JournalName;

            var message = new Message(email, new string[] { email }, subject, content, null);

            await emailSender.SendEmailAsync(message);
        }


    }
}
