namespace TaskAPI.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? PasswordHash { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public Guid CompanyId { get; set; }
    
    public bool IsBoss { get; set; }

    private User(Guid id, string name, string passwordHash, string phoneNumber, Guid companyId, bool isBoss)
    {
        Id = id;
        Name = name;
        PasswordHash = passwordHash;
        PhoneNumber = phoneNumber;
        CompanyId = companyId;
        IsBoss = isBoss;
    }

    public static User CreateUser(Guid id, string name, string passwordHash, string phoneNumber, Guid companyId, bool isBoss)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber))
            throw new ArgumentException("Name or phone number does not exist");

        if (id == Guid.Empty || companyId == Guid.Empty)
            throw new ArgumentException("One of Guids does not exist");

        return new User(id, name, passwordHash, phoneNumber, companyId, isBoss);
    }
}