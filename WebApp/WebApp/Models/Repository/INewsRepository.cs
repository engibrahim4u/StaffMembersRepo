using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
   public interface INewsRepository
    {
        News Find(int id);
        Task<News> FindAsync(int id);


        Task Update(News news);
        void Add(News news);
        Task AddAsync(News news);

        void Delete(News news);

        IList<News> List();
        IList<News> List(int journalId);
    }
}
