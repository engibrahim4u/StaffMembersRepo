using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.Repository
{
    public class PlaceUserDbRepository:IPlaceUserRepository
    {
        ApplicationDbContext db;

        public PlaceUserDbRepository(ApplicationDbContext _db)
        {
            db = _db;

        }
        public void Add(PlaceUser facultyUser)
        {
            db.PlaceUsers.Add(facultyUser);
            db.SaveChanges();
        }

        public void Delete(PlaceUser facultyUser)
        {
            db.PlaceUsers.Remove(facultyUser);
            db.SaveChanges();
        }



        public PlaceUser Find(int id)
        {
            return db.PlaceUsers
                  .Include(x => x.Place)
                  .SingleOrDefault(x => x.Id == id);
        }

        public PlaceUser Find(int facultyId, string UserName)
        {
            return db.PlaceUsers
                            .Include(x => x.Place)
                            .SingleOrDefault(x => x.PlaceId == facultyId && x.UserName == UserName);
        }

        public IList<PlaceUser> List()
        {
            return db.PlaceUsers
                 .Include(x => x.Place)
                 .ToList();
        }

        public IList<PlaceUser> List(int facultyId)
        {
            return db.PlaceUsers
                 .Where(x => x.PlaceId == facultyId)
                 .Include(x => x.Place)
                 .ToList();
        }

        public IList<PlaceUser> List(string userName)
        {
            return db.PlaceUsers
                .Where(x => x.UserName == userName)
               .Include(x => x.Place)
               .ToList();
        }

        public void Update(PlaceUser newFacultyUser)
        {
            db.PlaceUsers.Update(newFacultyUser);
            db.SaveChanges();
        }
    }
}
