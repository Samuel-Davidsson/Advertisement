using Domain.Entites;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface ICompanyService
    {
        void Add(Company company);
        IEnumerable<Company> GetAll();
    }
}