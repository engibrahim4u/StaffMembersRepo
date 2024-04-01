using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
   public interface IEventsRepository
    {
        Event Find(int id);
        Task<Event> FindAsync(int id);


        Task Update(Event journalEvent);
        void Add(Event journalEvent);
        Task AddAsync(Event journalEvent);

        void Delete(Event journalEvent);

        IList<Event> List();
        IList<Event> List(int journalId);

    }
}
