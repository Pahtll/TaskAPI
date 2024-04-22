namespace TaskAPI.Persistence.Entities;

public class CompanyEntity
{
    public Guid Id { get; set; }
    
    public string? CompanyName { get; set; }
    
    public Guid BossId { get; set; }
    
    public string? Country { get; set; }
}