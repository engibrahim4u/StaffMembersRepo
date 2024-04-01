using WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public class EMailDbRepository : IEMailRepository
    {
        ApplicationDbContext db;

        public EMailDbRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(EMail eMail)
        {
            throw new NotImplementedException();
        }

        public void Delete(EMail eMail)
        {
            throw new NotImplementedException();
        }

        public EMail Find(string code)
        {
            return db.EMails.SingleOrDefault(x => x.Code == code );
        }

        public async Task<EMail> FindAsync(string code)
        {
            return await db.EMails.FindAsync( code );
        }

      

        public IList<EMail> List()
        {
           return db.EMails.ToList();
        }

        public void Update(EMail eMail)
        {
            db.EMails.Update(eMail);
            db.SaveChanges();
        }
    }
}
