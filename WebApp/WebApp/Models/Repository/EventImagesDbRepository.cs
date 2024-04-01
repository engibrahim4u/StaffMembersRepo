using WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public class EventImagesDbRepository : IEventImagesRepository
    {
        ApplicationDbContext db;
        public EventImagesDbRepository(ApplicationDbContext _db)
        {
            db = _db;

        }
        public void Add(EventImage eventImage)
        {
            db.EventImages.Add(eventImage);
            db.SaveChanges();
        }

        public async Task AddAsync(EventImage eventImage)
        {
           await db.EventImages.AddAsync(eventImage);
           await db.SaveChangesAsync();
        }

        public void Delete(EventImage eventImage)
        {
            db.EventImages.Remove(eventImage);
            db.SaveChanges();
        }

        public EventImage Find(int id)
        {
            return db.EventImages
                .SingleOrDefault(x => x.Id == id);
        }

        public EventImage Find(int id, int eventId, int category)
        {
            return db.EventImages
                .SingleOrDefault(x => x.Id == id && x.EventId==eventId && x.Category==category);
        }

        public Task<EventImage> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IList<EventImage> List()
        {
            return db.EventImages
                           .OrderBy(x =>  x.Category).ThenBy(x=>x.Order)
                           .ToList();
        }

        public IList<EventImage> List(int category,int eventId)
        {
            return db.EventImages
                .Where(x=>x.EventId==eventId && x.Category==category)
                           .OrderBy(x => x.Order)
                           .ToList();
        }

        public void Update(EventImage eventImage)
        {
            throw new NotImplementedException();
        }
    }
}
