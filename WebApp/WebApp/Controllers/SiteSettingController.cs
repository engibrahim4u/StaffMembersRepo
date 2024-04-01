using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Repository;
using WebApp.Security;

namespace WebApp.Controllers
{
   
        [Authorize(Roles = "superAdmin")]
        public class SiteSettingController : Controller
        {

            private readonly ISettingRepository settingRepository;
            private readonly IDataProtector protector;

            public SiteSettingController(ISettingRepository settingRepository,
                                                IDataProtectionProvider dataProtectionProvider,
                                                DataProtectionPurposeStrings dataProtectionPurposeStrings)
            {
                this.settingRepository = settingRepository;
                protector = dataProtectionProvider.CreateProtector(dataProtectionPurposeStrings.SiteSecurity);

            }

            // GET: SiteSettingController/Edit/5
            public ActionResult Edit(int id)
            {
                Setting setting = settingRepository.Find(1);
                return View(setting);
            }

            // POST: SiteSettingController/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(int id, Setting setting)
            {
                try
                {
                    if (id == setting.Id)
                    {
                        var newSetting = settingRepository.Find(id);
                        newSetting.Available = setting.Available;
                        //newSetting.AuditorAvailable = setting.AuditorAvailable;
                        //newSetting.AuthorAvailable = setting.AuthorAvailable;
                        //newSetting.EditorAvailable = setting.EditorAvailable;
                        //newSetting.SendConfirmationEmail = setting.SendConfirmationEmail;

                        //newSetting.CurrentAcademicYear = setting.CurrentAcademicYear;
                        newSetting.StartDate = setting.StartDate;
                        newSetting.EndDate = setting.EndDate;
                        //newSetting.NotAvailableMessage = setting.NotAvailableMessage;
                    newSetting.PersonalPhotoRequired = setting.PersonalPhotoRequired;
                    newSetting.ContactInfo = setting.ContactInfo;
                    //newSetting.SubscriptionInstitutionsValueInsideSA = setting.SubscriptionInstitutionsValueInsideSA;
                    //newSetting.SubscriptionInstitutionsValueOutsaideSA = setting.SubscriptionInstitutionsValueOutsaideSA;
                    //newSetting.SubscriptionPersonalValueInsideSA = setting.SubscriptionPersonalValueInsideSA;
                    //newSetting.SubscriptionPersonalValueOutsaideSA = setting.SubscriptionPersonalValueOutsaideSA;
                    //newSetting.TransferBankAccount = setting.TransferBankAccount;



                    newSetting.SecureSite = protector.Protect(setting.StartDate.ToString());
                        settingRepository.Update(newSetting);
                    }

                    //settingRepository.Update(setting);
                }
                catch(Exception ex)
                {

                }
                return View();
            }

            // GET: SiteSettingController/Delete/5

        }
    }

