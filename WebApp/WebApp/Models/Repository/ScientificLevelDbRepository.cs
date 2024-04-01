using WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public class ScientificLevelDbRepository : IScientificLevelRepository
    {
        ApplicationDbContext db;

        public ScientificLevelDbRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(ScientificLevel scientificLevel)
        {
            throw new NotImplementedException();
        }

        public void Delete(ScientificLevel scientificLevel)
        {
            throw new NotImplementedException();
        }

        public ScientificLevel Find(int id)
        {
            throw new NotImplementedException();
        }

        public IList<ScientificLevel> List()
        {
            return db.ScientificLevels.OrderBy(x => x.Order).ToList();
        }

        public void Update(ScientificLevel scientificLevel)
        {
            throw new NotImplementedException();
        }
    }
}
