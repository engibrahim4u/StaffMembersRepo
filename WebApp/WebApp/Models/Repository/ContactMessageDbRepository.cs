using WebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public class ContactMessageDbRepository : IContactMessageRepository
    {
        ApplicationDbContext db;
        public ContactMessageDbRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task Add(ContactMessage contactMessage)
        {
           await db.ContactMessages.AddAsync(contactMessage);
            db.SaveChanges();
        }

        public async Task<ContactMessage> FindAsync(int id)
        {
          return await  db.ContactMessages
                
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public List<ContactMessage> GetByReferenceCode(double referenceCode)
        {
            return db.ContactMessages
             
               .Where(x => x.ReferenceCode == referenceCode)
               .OrderByDescending(x => x.CDate)
               .ToList();
        }

        public IList<ContactMessage> List(int journalId)
        {
            return db.ContactMessages
                
                .Where(x => x.JournalId == journalId && !string.IsNullOrEmpty(x.Email))
                .OrderByDescending(x => x.CDate)
                .ToList();
        }

        public async Task<ContactMessage> ReferenceCode()
        {
            return await db.ContactMessages.OrderByDescending(x => x.ReferenceCode)
                .FirstOrDefaultAsync();
        }

        public async Task Update(ContactMessage contactMessage)
        {
            db.Update(contactMessage);
            await db.SaveChangesAsync();
        }
    }
}
