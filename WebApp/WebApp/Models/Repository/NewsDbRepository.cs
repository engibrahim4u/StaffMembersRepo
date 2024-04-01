using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{

    public class NewsDbRepository:INewsRepository
    {
        ApplicationDbContext db;

        public NewsDbRepository(ApplicationDbContext _db)
        {
            db = _db;

        }

        public void Add(News news)
        {
            db.News.Add(news);
            db.SaveChanges();
        }

        public async Task AddAsync(News news)
        {
            await db.News.AddAsync(news);
            await db.SaveChangesAsync();
        }

        public void Delete(News news)
        {
            news.IsDeleted = true;
            db.News.Update(news);
            db.SaveChanges();
        }

        public News Find(int id)
        {
            return db.News
                           
                           .SingleOrDefault(x => x.Id == id);
        }

        public async Task<News> FindAsync(int id)
        {
            return await db.News
                          
                            .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IList<News> List()
        {
            return db.News
                            
                            .Where(x => !x.IsDeleted)
                            .OrderBy(x => x.Order)
                            .ThenByDescending(x => x.CDate)
                            .ToList();
        }

        public IList<News> List(int journalId)
        {
            return db.News
                             
                             .Where(x => !x.IsDeleted )
                             .OrderBy(x => x.Order)
                             .ThenByDescending(x => x.CDate)
                             .ToList();
        }

        public async Task Update(News news)
        {
            db.News.Update(news);
            await db.SaveChangesAsync();
        }
    }
}
