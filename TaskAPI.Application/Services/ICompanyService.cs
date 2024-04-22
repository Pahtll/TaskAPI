using TaskAPI.Domain.Models;

namespace TaskAPI.Application.Services;

public interface ICompanyService
{
    Task<Guid> CreateCompany(string companyName, Guid? bossId, string country);
    Task<List<Company>> GetAllCompanies();
    Task<Company> GetCompanyById(Guid id);
    Task<Company> GetCompanyByName(string companyName);
    Task<Guid> DeleteCompany(Guid id);
    Task<Guid> UpdateCompany(Guid id, string companyName, Guid bossId, string country);
}