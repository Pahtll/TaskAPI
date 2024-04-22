using Microsoft.AspNetCore.Mvc;
using TaskAPI.Abstractions;
using TaskAPI.Application.Services;

namespace TaskAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController(ICompanyService companyService) : ControllerBase
{
    [HttpGet("/GetAllCompanies")]
    public async Task<ActionResult<List<CompanyResponse>>> GetAllCompanies()
    {
        var companies = await companyService.GetAllCompanies();
        var result = companies.Select(c => new CompanyResponse(c.Id, c.CompanyName!, c.BossId, c.Country!)).ToList();
        return Ok(result);
    }
    
    [HttpGet("/GetById/{id:guid}")]
    public async Task<ActionResult<CompanyResponse>> GetCompanyById(Guid id)
    {
        var company = await companyService.GetCompanyById(id);
        var result = new CompanyResponse(company.Id, company.CompanyName!, company.BossId, company.Country!);
        return Ok(result);
    }
    
    [HttpGet("/GetByName/{name}")]
    public async Task<ActionResult<CompanyResponse>> GetCompanyByName(string name)
    {
        var company = await companyService.GetCompanyByName(name);
        var result = new CompanyResponse(company.Id, company.CompanyName!, company.BossId, company.Country!);
        return Ok(result);
    }
    
    [HttpPost("CreateCompany")]
    public async Task<ActionResult<Guid>> CreateCompany(string companyName, Guid bossId, string country)
    {
        var companyId = await companyService.CreateCompany(companyName, bossId, country);
        return Ok(companyId);
    }
    
    [HttpPut("/UpdateCompany/{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateCompany(Guid id, string companyName, Guid bossId, string country)
    {
        var updatedId = await companyService.UpdateCompany(id, companyName, bossId, country);
        return Ok(updatedId);
    }
    
    [HttpDelete("/DeleteCompany/{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteCompany(Guid id)
    {
        var deletedId = await companyService.DeleteCompany(id);
        return Ok(deletedId);
    }
}