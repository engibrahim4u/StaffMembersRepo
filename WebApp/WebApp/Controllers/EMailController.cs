using WebApp.Helper;
using WebApp.Models;
using WebApp.Models.Repository;
using WebApp.Models.ViewModels;
using WebApp.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Authorize(Roles = "superAdmin,admin")]

    public class EMailController : Controller
    {
        private readonly IEMailRepository eMails;
        private readonly IDataProtector protector;

        public EMailController(IEMailRepository _eMails
                                    , IDataProtectionProvider dataProtectionProvider
                                    , DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            eMails = _eMails;
            protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.SiteSecurity);

        }
        public IActionResult Index(int pag=1)
        {
            var emails = eMails.List();
            const int pageSize= 15;
            if (pag < 1)
                pag = 1;
            int recordCount = emails.Count();
            var pager = new Pager(recordCount, pag, pageSize,"Index","EMail");
            int recordSkip = (pag - 1) * pageSize;
            var data = emails.Skip(recordSkip).Take(pageSize)
                .Select(x => new EMailMessagesVM
                {
                    Code = x.Code,
                    Name=x.Name
                }).ToList();
            ViewBag.Pager = pager;
            return View(data);
        }

        public ActionResult Edit(string id, string code)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(code)) return RedirectToAction("Index");
            int journalId = Convert.ToInt32(protector.Unprotect(id));
            var email = eMails.Find(code);
            var emailMessage = EMailMessage(email);
            return View(emailMessage);
        }

        // POST: SiteSettingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EMailMessagesVM eMailMessagesVM)
        {
            if (string.IsNullOrEmpty(eMailMessagesVM.JournalIdEncrypted) || string.IsNullOrEmpty(eMailMessagesVM.Code)) return RedirectToAction("Index");
            int journalId = Convert.ToInt32(protector.Unprotect(eMailMessagesVM.JournalIdEncrypted));
            var email = eMails.Find(eMailMessagesVM.Code);

            try
            {
                if (email!=null)
                {
                    email.Title = eMailMessagesVM.Title;
                    email.TitleEn = eMailMessagesVM.TitleEn;
                    email.Msg = eMailMessagesVM.Msg;
                    email.MsgEn = eMailMessagesVM.MsgEn;
                    email.SendEmail = eMailMessagesVM.SendEmail;
                    eMails.Update(email);
                    ViewBag.Updated = true;
                }

            }
            catch(Exception ex)
            {

            }
            return View(eMailMessagesVM);
        }

        private EMailMessagesVM EMailMessage(EMail eMail)
        {
            EMailMessagesVM data = new EMailMessagesVM();
            data.Code = eMail.Code;
            data.Name = eMail.Name;
            data.Title = eMail.Title;
            data.TitleEn = eMail.TitleEn;
            data.Msg = eMail.Msg;
            data.MsgEn = eMail.MsgEn;
            data.SendEmail = eMail.SendEmail;
            return data;
        }

    }
}
