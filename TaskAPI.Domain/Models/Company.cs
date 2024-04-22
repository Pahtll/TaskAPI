namespace TaskAPI.Domain.Models;

public class Company
{
    public Guid Id { get; set; }
    
    public string? CompanyName { get; set; }
    
    public Guid BossId { get; set; }
    
    public string? Country { get; set; }

    private Company(Guid id, string companyName, Guid bossId, string country)
    {
        Id = id;
        CompanyName = companyName;
        BossId = bossId;
        Country = country;
    }

    public static Company CreateCompany(Guid id, string companyName, Guid bossId, string country)
    {
        if (string.IsNullOrEmpty(companyName) || string.IsNullOrEmpty(country))
            throw new ArgumentException("Company name or country does not exist");

        if (id == Guid.Empty || bossId == Guid.Empty)
            throw new ArgumentException("Guids does not exist");

        return new Company(id, companyName, bossId, country);
    }
}