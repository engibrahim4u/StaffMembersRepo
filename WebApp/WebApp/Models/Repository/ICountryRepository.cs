using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
   public interface ICountryRepository
    {
        Country Find(int id);

        void Update(Country country);
        void Add(Country country);
        void Delete(Country country);

        IList<Country> List();
    }
}
