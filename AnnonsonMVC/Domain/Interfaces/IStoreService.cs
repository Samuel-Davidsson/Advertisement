using Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStoreService
    {
        void Add(Store store);
        IEnumerable<Store> GetAll();
    }
}