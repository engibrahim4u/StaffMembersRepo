using WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public class CountryDbRepository : ICountryRepository
    {
        ApplicationDbContext db;

        public CountryDbRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(Country country)
        {
            throw new NotImplementedException();
        }

        public void Delete(Country country)
        {
            throw new NotImplementedException();
        }

        public Country Find(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Country> List()
        {
            return db.Countries.OrderBy(x => x.Order).ThenBy(x => x.Name).ToList();
        }

        public void Update(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
