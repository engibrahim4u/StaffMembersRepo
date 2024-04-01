using WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
    public class AcademicDegreeDbRepository : IAcademicDegreeRepository
    {
        ApplicationDbContext db;
       
        public AcademicDegreeDbRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public void Add(AcademicDegree academicDegree)
        {
            throw new NotImplementedException();
        }

        public void Delete(AcademicDegree academicDegree)
        {
            throw new NotImplementedException();
        }

        public AcademicDegree Find(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AcademicDegree> FindAsync(int id)
        {
            return await db.AcademicDegrees.FindAsync(id);
        }

        public IList<AcademicDegree> List()
        {
            return db.AcademicDegrees.OrderBy(x => x.Order).ToList();
        }

        public void Update(AcademicDegree academicDegree)
        {
            throw new NotImplementedException();
        }
    }
}
