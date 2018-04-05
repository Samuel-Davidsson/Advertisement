using Domain.Entites;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IStoreService
    {
        void Add(Store store);
        IEnumerable<Store> GetAll();
    }
}