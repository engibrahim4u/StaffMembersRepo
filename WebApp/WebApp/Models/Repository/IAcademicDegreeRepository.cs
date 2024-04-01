using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
   public interface IAcademicDegreeRepository
    {
        AcademicDegree Find(int id);
      Task<AcademicDegree> FindAsync(int id);


        void Update(AcademicDegree academicDegree);
        void Add(AcademicDegree academicDegree);
        void Delete(AcademicDegree academicDegree);

        IList<AcademicDegree> List();
    }
}
