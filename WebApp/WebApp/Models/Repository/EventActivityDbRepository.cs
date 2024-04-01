using WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public class EventActivityDbRepository : IEventActivityRepository
    {
        ApplicationDbContext db;
        public EventActivityDbRepository(ApplicationDbContext _db)
        {
            db = _db;

        }

        public void Add(EventActivity eventActivity)
        {
            db.EventActivities.Add(eventActivity);
            db.SaveChanges();
        }

        public async Task AddAsync(EventActivity eventActivity)
        {
            await db.EventActivities.AddAsync(eventActivity);
            await db.SaveChangesAsync();
        }

        public void Delete(EventActivity eventActivity)
        {
            eventActivity.IsDeleted = true;
            db.EventActivities.Update(eventActivity);
            db.SaveChanges();
        }

        public EventActivity Find(int id)
        {
            return db.EventActivities
                .SingleOrDefault(x => x.Id == id);
        }

        //public async Task<EventActivity> FindAsync(int id)
        //{
        //    return await db.EventActivities
        //        .SingleOrDefaultAsync(x => x.Id == id);
        //}

        public IList<EventActivity> List()
        {
            return db.EventActivities
                           .Where(x => !x.IsDeleted)
                           .OrderBy(x => x.Order)
                           .ThenByDescending(x => x.CDate)
                           .ToList();
        }

        public IList<EventActivity> List(int journalId)
        {
            return db.EventActivities
                             .Where(x => !x.IsDeleted)
                             .OrderBy(x => x.Order)
                             .ThenByDescending(x => x.CDate)
                             .ToList();
        }

        public async Task Update(EventActivity eventActivity)
        {
            db.EventActivities.Update(eventActivity);
            await db.SaveChangesAsync();
        }
    }
}
