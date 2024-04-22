using Microsoft.EntityFrameworkCore;
using TaskAPI.Domain.Models;
using TaskAPI.Persistence;
using TaskAPI.Persistence.Entities;

namespace TaskAPI.Application.Services;

public class CompanyService(TaskApiDbContext context) : ICompanyService
{
    public async Task<Guid> CreateCompany(string companyName, Guid? bossId, string country)
    {
        var companyEntity = new CompanyEntity
        {
            Id = Guid.NewGuid(),
            CompanyName = companyName,
            BossId = bossId ?? Guid.Empty,
            Country = country
        };
        
        await context.Companies.AddAsync(companyEntity);
        await context.SaveChangesAsync();
        
        return companyEntity.Id;
    }
    
    public async Task<List<Company>> GetAllCompanies()
    {
        return await context.Companies
            .Select(c => Company.CreateCompany(c.Id, c.CompanyName!, c.BossId, c.Country!))
            .ToListAsync();
    } 

    public async Task<Company> GetCompanyById(Guid id)
    {
        var companyEntity = await context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new ArgumentException("Company does not exist");
        
        return Company.CreateCompany(companyEntity.Id, companyEntity.CompanyName!, companyEntity.BossId, companyEntity.Country!);
    }
    
    public async Task<Company> GetCompanyByName(string companyName)
    {
        var companyEntity = await context.Companies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CompanyName == companyName)
            ?? throw new ArgumentException("Company does not exist");
        
        return Company.CreateCompany(companyEntity.Id, companyEntity.CompanyName!, companyEntity.BossId, companyEntity.Country!);
    }
    
    public async Task<Guid> DeleteCompany(Guid id)
    {
        await context.Companies
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
    
    public async Task<Guid> UpdateCompany(Guid id, string companyName, Guid bossId, string country)
    {
        await context.Companies
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.CompanyName, companyName)
                .SetProperty(c => c.BossId, bossId)
                .SetProperty(c => c.Country, country));
        
        return id;
    }
}