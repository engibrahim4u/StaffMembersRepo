using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
   public interface IEventActivityRepository
    {
        EventActivity Find(int id);
        //Task<EventActivity> FindAsync(int id);


        Task Update(EventActivity eventActivity);
        void Add(EventActivity eventActivity);
        Task AddAsync(EventActivity eventActivity);

        void Delete(EventActivity eventActivity);

        IList<EventActivity> List();
        IList<EventActivity> List(int journalId);
    }
}
