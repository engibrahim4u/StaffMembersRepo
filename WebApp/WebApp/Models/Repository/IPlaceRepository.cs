using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public interface IPlaceRepository
    {
        Place Find(int id);

        void Update(Place newPlace);
        void Add(Place place);
        void Delete(Place place);

        IList<Place> List();
    }
}
