using TaskAPI.Domain.Models;

namespace TaskAPI.Application.Services;

public interface IUserService
{
    Task<Guid> Register(string name, string password, string phoneNumber, Guid companyId, bool isBoss);
    Task<string> Login(string name, string password);
    Task<User> GetByName(string name);
    Task<List<User>> GetAllUsers();
    Task<List<User>> GetAllUsersByCompanyId(Guid companyId);
    Task<User> GetUserById(Guid id);
    Task<Guid> DeleteUser(Guid id);

    Task<Guid> UpdateUser(Guid id, string name, string password,
        string phoneNumber, Guid companyId, bool isBoss);
}