using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatWorkerServer.Interfaces
{
    public interface IDataRepository<T>
    {
        IEnumerable<T> GetAll();
        void AddRangeAsync(List<T> list);
        Task<bool> AddAsync(T item);
    }
}
