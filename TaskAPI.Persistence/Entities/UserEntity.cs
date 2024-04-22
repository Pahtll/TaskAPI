namespace TaskAPI.Persistence.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? PasswordHash { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public Guid CompanyId { get; set; }
    
    public bool IsBoss { get; set; }
}