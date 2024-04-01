using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
   public interface IContactMessageRepository
    {
        List<ContactMessage> GetByReferenceCode(double referenceCode);
        Task<ContactMessage> FindAsync(int id);
        Task Add(ContactMessage contactMessage);
        IList<ContactMessage> List(int journalId);
        Task<ContactMessage> ReferenceCode();
        Task Update(ContactMessage contactMessage);
    }
}
