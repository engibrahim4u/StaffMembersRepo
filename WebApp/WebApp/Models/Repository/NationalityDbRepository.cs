using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.Repository
{
    public class NationalityDbRepository : INationalityRepository
    {
        ApplicationDbContext db;
        public NationalityDbRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(Nationality nationality)
        {
            db.Nationalities.Add(nationality);
            db.SaveChanges();
        }

        public void Delete(Nationality nationality)
        {
            db.Nationalities.Remove(nationality);
            db.SaveChanges();
        }

        public Nationality Find(int id)
        {
            return db.Nationalities.SingleOrDefault(x => x.Id == id);
        }

        public IList<Nationality> List()
        {
            return db.Nationalities.ToList();
        }

        public void Update(Nationality newNationality)
        {
            db.Nationalities.Update(newNationality);
            db.SaveChanges();
        }
    }
}
