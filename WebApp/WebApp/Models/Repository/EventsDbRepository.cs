using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public class EventsDbRepository : IEventsRepository
    {
        ApplicationDbContext db;
        public EventsDbRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(Event journalEvent)
        {
            db.Events.Add(journalEvent);
            db.SaveChanges();
        }

        public async Task AddAsync(Event journalEvent)
        {
           await db.Events.AddAsync(journalEvent);
           await db.SaveChangesAsync();
        }

        public void Delete(Event journalEvent)
        {
            journalEvent.IsDeleted = true;
            db.Events.Update(journalEvent);
            db.SaveChanges();
        }

        public Event Find(int id)
        {
            return db.Events
                .SingleOrDefault(x => x.Id == id);
        }

        public async Task<Event> FindAsync(int id)
        {
            return await db.Events
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public IList<Event> List()
        {
            return db.Events
                 .Where(x => !x.IsDeleted)
                 .OrderBy(x => x.Order)
                 .ThenByDescending(x => x.CDate)
                 .ToList();
        }
        public IList<Event> List(int journalId)
        {
            return db.Events
                 .Where(x => !x.IsDeleted )
                 .OrderBy(x => x.Order)
                 .ThenByDescending(x => x.CDate)
                 .ToList();
        }

        public async Task Update(Event journalEvent)
        {
            db.Events.Update(journalEvent);
            await db.SaveChangesAsync();
        }
    }
}
