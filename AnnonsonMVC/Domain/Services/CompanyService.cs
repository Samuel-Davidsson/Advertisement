using Domain.Entites;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _repository;

    public CompanyService(IRepository<Company> repository)
    {
         _repository = repository;
    }

    public void Add(Company company)
    {
        _repository.Add(company);
    }

    public Company Find(int id, params string[] includeProperties)
    {
        return _repository.Find(x => x.CompanyId == id, includeProperties);
    }

    public IEnumerable<Company> GetAll()
    {
        return _repository.GetAll();
    }
}
}
