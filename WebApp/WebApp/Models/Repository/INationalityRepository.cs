using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
   public interface INationalityRepository
    {
        Nationality Find(int id);

        void Update(Nationality newNationality);
        void Add(Nationality nationality);
        void Delete(Nationality nationality);

        IList<Nationality> List();
    }
}
