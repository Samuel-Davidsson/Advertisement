using Domain.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICompanyService
    {
        void Add(Company company);
        IEnumerable<Company> GetAll();
    }
}