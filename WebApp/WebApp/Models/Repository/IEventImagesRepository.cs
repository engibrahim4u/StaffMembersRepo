using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
   public interface IEventImagesRepository
    {
        EventImage Find(int id);
        EventImage Find(int id,int eventId,int category);

        Task<EventImage> FindAsync(int id);


        void Update(EventImage eventImage);
        void Add(EventImage eventImage);
        Task AddAsync(EventImage eventImage);

        void Delete(EventImage eventImage);

        IList<EventImage> List();
        IList<EventImage> List(int category,int eventId);

    }
}
