using projectMtuci.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectMtuci.DAL.Interfaces
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
        IQueryable<Subject> GetByName(string name);
    }
}
