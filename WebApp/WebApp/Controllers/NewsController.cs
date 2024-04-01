using WebApp.Helper;
using WebApp.Models;
using WebApp.Models.Repository;
using WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    [Authorize(Roles = "superAdmin,admin")]

    public class NewsController : Controller
    {
        private readonly INewsRepository journalNews;

        public NewsController(INewsRepository _journalNews
                               )
        {
            journalNews = _journalNews;
            
        }
        public IActionResult Index(int id = 1)
        {
            var model = journalNews.List(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (id == 0) return RedirectToAction("Index");
            var news = journalNews.Find(id);
            var data = EventData(news);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(NewsVM model)
        {
            if (model.Id == 0) return RedirectToAction("Index");
            var news = journalNews.Find(model.Id);

            try
            {
                if (news != null)
                {
                    news.CDate = MyTimeZone.Now;
                    news.Content = model.Content;
                    news.ContentEn = model.ContentEn;
                    news.DisplayInHome = model.DisplayInHome;
                    news.DisplayInHomeEn = model.DisplayInHomeEn;
                    news.Header = model.Header;
                    news.HeaderEn = model.HeaderEn;
                    news.Order = model.Order;

                    await journalNews.Update(news);
                    model.Image = news.Image;
                    ViewBag.Updated = true;
                }

            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        public ActionResult Add()
        {
            int journalId = 1;
            NewsVM model = new NewsVM();
            model.JournalId = journalId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(NewsVM model)
        {
            if (model.JournalId == 0) return RedirectToAction("Index");
            try
            {
                var news = new News();

                news.CDate = MyTimeZone.Now;
                news.Content = model.Content;
                news.ContentEn = model.ContentEn;
                news.DisplayInHome = model.DisplayInHome;
                news.DisplayInHomeEn = model.DisplayInHomeEn;
                news.Header = model.Header;
                news.HeaderEn = model.HeaderEn;
                news.Order = model.Order;
                await journalNews.AddAsync(news);
                ViewBag.Updated = true;


            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (id == 0) return RedirectToAction("Index");
            var news = journalNews.Find(id);
            if (news != null)
                journalNews.Delete(news);
            return RedirectToAction("Index");
        }
        private NewsVM EventData(News news)
        {
            NewsVM data = new NewsVM();
            data.CDate = news.CDate;
            data.Content = news.Content;
            data.ContentEn = news.ContentEn;
            data.DisplayInHome = news.DisplayInHome;
            data.DisplayInHomeEn = news.DisplayInHomeEn;
            data.Header = news.Header;
            data.HeaderEn = news.HeaderEn;
            data.Order = news.Order;
            data.Image = news.Image;
            return data;
        }
        private List<NewsVM> JournalEventsList(int journalId)
        {
            var list = journalNews.List(journalId)
              .Select(x => new NewsVM
              {
                  Header = x.Header,
                  DisplayInHome = x.DisplayInHome,
                  DisplayInHomeEn = x.DisplayInHomeEn,
                  Order = x.Order,
                  Image = x.Image
              })
              .OrderBy(x => x.Order).ToList();
            return list;
        }
    }
}
