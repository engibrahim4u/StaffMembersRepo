using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
   public interface IScientificLevelRepository
    {
        ScientificLevel Find(int id);

        void Update(ScientificLevel  scientificLevel);
        void Add(ScientificLevel scientificLevel);
        void Delete(ScientificLevel  scientificLevel);

        IList<ScientificLevel> List();
    }
}
