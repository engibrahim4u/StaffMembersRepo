using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
   public interface IEMailRepository
    {
        EMail Find(string code);
        Task<EMail> FindAsync(string code);


        void Update(EMail eMail);
        void Add(EMail eMail);
        void Delete(EMail eMail);

        IList<EMail> List();
    }
}
