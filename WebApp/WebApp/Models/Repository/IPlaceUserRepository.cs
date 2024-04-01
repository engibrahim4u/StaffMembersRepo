using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
  public interface IPlaceUserRepository
    {
        PlaceUser Find(int id);
        PlaceUser Find(int placeUserId, string UserName);

        void Update(PlaceUser newPlaceUser);
        void Add(PlaceUser placeUser);
        void Delete(PlaceUser placeUser);

        IList<PlaceUser> List();
        IList<PlaceUser> List(int placeId);
        IList<PlaceUser> List(string userName);
    }
}
