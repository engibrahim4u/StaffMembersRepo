using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.Repository
{
    public class PlaceDbRepository:IPlaceRepository
    {
        ApplicationDbContext db;

        public PlaceDbRepository(ApplicationDbContext _db)
        {
            db = _db;

        }
        public void Add(Place place)
        {
            db.Places.Add(place);
            db.SaveChanges();
        }

       

        public void Delete(Place place)
        {
            throw new NotImplementedException();
        }

        public Place Find(int id)
        {
            return db.Places.SingleOrDefault(x => x.Id == id);

        }

        public IList<Place> List()
        {
            return db.Places.ToList();

        }

        public void Update(Place newPlace)
        {
            db.Places.Update(newPlace);
            db.SaveChanges();
        }
    }
}
