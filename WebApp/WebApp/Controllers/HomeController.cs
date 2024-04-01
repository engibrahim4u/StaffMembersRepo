using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApp.Helper;
using WebApp.Models;
using WebApp.Models.ViewModels;
using WebApp.Models.Repository;
using WebApp.Services;
using WebApp.EmailService;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactMessageRepository contactMessages;
        private readonly IEventsRepository siteEvents;
        private readonly IEventImagesRepository eventImages;
        private readonly IEventActivityRepository eventActivities;
        private readonly INewsRepository news;
        private readonly ISettingRepository settings;
        private readonly IEmailSender emailSender;
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> localizer;
        public string SiteBaseUrl { get; set; }

        public HomeController(
                                IContactMessageRepository _contactMessages
                                ,IEventsRepository _siteEvents
                                ,IEventImagesRepository _eventImages
                                ,IEventActivityRepository _eventActivities
                                ,INewsRepository _news
                                ,ISettingRepository _settings
                                 ,     IEmailSender _emailSender
                                , ILogger<HomeController> logger
                             , IStringLocalizer<HomeController> _localizer)
        {
            contactMessages = _contactMessages;
            siteEvents = _siteEvents;
            eventImages = _eventImages;
            eventActivities = _eventActivities;
            news = _news;
            settings = _settings;
            emailSender = _emailSender;
            _logger = logger;
            localizer = _localizer;

        }

        public IActionResult Index()
        {
            int journalId = 1;
            //SiteBaseUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            SiteBaseUrl = $"{this.Request.PathBase}";
            //SiteBaseUrl = $"{this.Request.Host}";


            //     string returnUrl = SiteBaseUrl + "/Account/Login";
            //string returnUrl =  "/Account/Login";
            // string returnUrl =  "/Home/Index";


            //return RedirectToAction("SetLanguage", new { culture = "ar-EG", returnUrl = returnUrl });
            //return RedirectToAction("SetLanguage", new { culture = "ar-EG", returnUrl = "" });

            //return RedirectToAction("SetCulture", new { culture = "ar-EG", redirectionUri = "/Account/Login" });


            //return View();
            //SetArabicLanguage();
            HomeVM homeData = new HomeVM();
            homeData.Events = SiteEvents();
            homeData.News = SiteNews();
            homeData.EventActivities = EventActivities();


            return View(homeData);
        }

   
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact(int id=1)
        {
            ContactUsVM model = new ContactUsVM();
            model.JournalId = 1;
            model.JournalContactInfo = settings.Find(1).ContactInfo;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactUsVM model)
        {
            try
            {

            var contactMessageData =await ContactMessageData(model);
            await contactMessages.Add(contactMessageData);
                ViewBag.Success = true;
                await SendEmail(model);
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Fail = true;

                return View(model);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        [HttpGet]
        public IActionResult SetLanguage(string culture,string returnUrl)
        {
            switch (culture)
            {
                case "ar-EG":
                    break;
                case "en-US":
                    break;
                default:
                    culture = "ar-EG";
                    break;
            }
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = MyTimeZone.Now.AddDays(1) });
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(culture);
            return RedirectToLocal(returnUrl);
        }

        private void SetArabicLanguage(string culture= "ar-EG")
        {
            switch (culture)
            {
                case "ar-EG":
                    break;
                case "en-US":
                    break;
                default:
                    culture = "ar-EG";
                    break;
            }
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = MyTimeZone.Now.AddDays(1) });
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            //returnUrl = "/KawlaJournal" + returnUrl;
            SiteBaseUrl = $"{this.Request.PathBase}";
            returnUrl = SiteBaseUrl + returnUrl;
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        [HttpGet]
        public IActionResult SetCulture(string culture, string redirectionUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)));
            }
            if (redirectionUri == null) redirectionUri = "/";
            return LocalRedirect(redirectionUri);
        }


        public IActionResult PrintReceipt(string id)
        {
            var model = new CategoryVM();
            model.Id = id;
            return View(model);
        }

        [Route("/PageNotFound")]
        public IActionResult PageNotFound()
        {
            return View();
        }
        public IActionResult EventDetails(int id)
        {
            if (id == 0) return RedirectToAction("Index");
            var eventDetails = siteEvents.Find(id);
            EventsVM model = new EventsVM()
            {
                Content = eventDetails.Content,
                ContentEn = eventDetails.ContentEn,
                Header = eventDetails.Header,
                HeaderEn = eventDetails.HeaderEn,
                Image = eventDetails.Image,
                ExternalUrl = eventDetails.ExternalUrl
            };
            return View(model);
        }

        public IActionResult NewsDetails(int id)
        {
            if (id == 0) return RedirectToAction("Index");
            var eventDetails = news.Find(id);
            NewsVM model = new NewsVM()
            {
                Content = eventDetails.Content,
                ContentEn = eventDetails.ContentEn,
                Header = eventDetails.Header,
                HeaderEn = eventDetails.HeaderEn,
                Image = eventDetails.Image,
                CDate=eventDetails.CDate
            };
            return View(model);
        }
        public IActionResult NewsList(int pag = 1)
        {
            var allNews = news.List();
            const int pageSize = 15;
            if (pag < 1)
                pag = 1;
            int recordCount = allNews.Count();
            var pager = new Pager(recordCount, pag, pageSize, "NewsList", "Home");
            int recordSkip = (pag - 1) * pageSize;
            var data = allNews.Skip(recordSkip).Take(pageSize)
                .Select(x => new NewsVM
                {
                    Header=x.Header,
                    HeaderEn=x.HeaderEn,
                    Image=x.Image,
                    CDate=x.CDate,
                    DisplayInHome=x.DisplayInHome,
                    DisplayInHomeEn=x.DisplayInHomeEn,
                    Id=x.Id
                }).ToList();
            ViewBag.Pager = pager;
            return View(data);
        }

        public IActionResult ActivityDetails(int id)
        {
            if (id == 0) return RedirectToAction("Index");
            var activityDetails = eventActivities.Find(id);
            int category = (int)EventImagesEnum.Activity;
            EventsVM model = new EventsVM()
            {
                Content = activityDetails.Content,
                ContentEn = activityDetails.ContentEn,
                Header = activityDetails.Header,
                HeaderEn = activityDetails.HeaderEn,
                Image = activityDetails.Image,
                CDate = activityDetails.CDate,
                EventImages=eventImages.List(category,id).ToList()
            };
            return View(model);
        }
        public IActionResult ActivitiesList(int pag = 1)
        {
            var allActivities = eventActivities.List();
            const int pageSize = 15;
            if (pag < 1)
                pag = 1;
            int recordCount = allActivities.Count();
            var pager = new Pager(recordCount, pag, pageSize, "ActivitesList", "Home");
            int recordSkip = (pag - 1) * pageSize;
            var data = allActivities.Skip(recordSkip).Take(pageSize)
                .Select(x => new EventsVM
                {
                    Header = x.Header,
                    HeaderEn = x.HeaderEn,
                    Image = x.Image,
                    CDate = x.CDate,
                    DisplayInHome = x.DisplayInHome,
                    DisplayInHomeEn = x.DisplayInHomeEn,
                    Id = x.Id
                }).ToList();
            ViewBag.Pager = pager;
            return View(data);
        }
        public IActionResult Default()
        {
            return RedirectToAction("SetLanguage", new { culture = "ar-EG", returnUrl = "/Home/Index" });

        }

        public IActionResult Mission()
        {
            return View();

        }
        public IActionResult Vission()
        {
            return View();

        }
        public IActionResult Objectives()
        {
            return View();

        }

  


      
        private async Task<ContactMessage> ContactMessageData(ContactUsVM contactUsVM)
        {
            var refCode = await contactMessages.ReferenceCode();
            int randNum = new Random().Next(100);
            ContactMessage contactMessage = new ContactMessage();
            contactMessage.CDate = MyTimeZone.Now;
            contactMessage.Email = contactUsVM.Email;
            contactMessage.IsRead = false;
            contactMessage.Message = contactUsVM.Message;
            contactMessage.Mobile = contactUsVM.Mobile;
            //contactMessage.ReferenceCode = MyTimeZone.Now.Ticks;
            contactMessage.ReferenceCode =  refCode == null? 1000000000001: refCode.ReferenceCode+ randNum;

            contactMessage.SenderName = contactUsVM.SenderName;
            contactMessage.Title = contactUsVM.Title;
            return contactMessage;
        }

        private async Task SendEmail(ContactUsVM contactUsVM)
        {
            string content = "تم استلام الرسالة التالية";
            content += "<br />" + contactUsVM.Title + " <br />"+ contactUsVM.Message;
            content += "<br />"+" سوف يتم التواصل معكم في اقرب وقت";

            string subject = "استلام رسالة من " + Constants.JournalName;

            var message = new Message(contactUsVM.Email, new string[] { contactUsVM.Email }, subject,content, null);

            await emailSender.SendEmailAsync(message);
        }

        private List<EventsVM> SiteEvents()
        {
            return siteEvents.List()
                .Select(x => new EventsVM
                {
                    Image = x.Image,
                    DisplayInHome = x.DisplayInHome,
                    DisplayInHomeEn = x.DisplayInHomeEn,
                    HasDetails= EventHasDetails(x.Content,x.ExternalUrl),
                    Header=x.Header,
                    HeaderEn=x.HeaderEn,
                    ExternalUrl=x.ExternalUrl,
                    Id=x.Id

                }).ToList();
        }


        private List<EventsVM> EventActivities()
        {
            return eventActivities.List()
                .Select(x => new EventsVM
                {
                    Image = x.Image,
                    DisplayInHome = x.DisplayInHome,
                    DisplayInHomeEn = x.DisplayInHomeEn,
                    HasDetails = EventHasDetails(x.Content, x.ExternalUrl),
                    Header = x.Header,
                    HeaderEn = x.HeaderEn,
                    ExternalUrl = x.ExternalUrl,
                    Id = x.Id

                }).Take(5).ToList();
        }
        private List<NewsVM> SiteNews()
        {
            return news.List()
                .Select(x => new NewsVM
                {
                    Image = x.Image,
                    DisplayInHome = x.DisplayInHome,
                    DisplayInHomeEn = x.DisplayInHomeEn,
                    HasDetails = NewsHasDetails(x.Content),
                    Header = x.Header,
                    HeaderEn = x.HeaderEn,
                    CDate=x.CDate,
                    Id = x.Id

                }).Where(x=>x.DisplayInHome || x.DisplayInHomeEn)
                .Take(10)
                .ToList();
        }
        private bool EventHasDetails(string content,string externalUrl)
        {
            if (!string.IsNullOrEmpty(content)) return true;
            if (!string.IsNullOrEmpty(externalUrl)) return true;
            return false;
        }

        private bool NewsHasDetails(string content)
        {
            if (!string.IsNullOrEmpty(content)) return true;
            return false;
        }
    }
}
