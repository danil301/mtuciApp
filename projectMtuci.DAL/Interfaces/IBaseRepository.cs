using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectMtuci.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> Get(int id);

        Task<bool> Create(T entity);

        Task<List<T>> Select();

        bool Delete(T entity);
    }
}
