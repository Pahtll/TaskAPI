using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Domain.Abstractions;
using TaskAPI.Domain.Models;
using TaskAPI.Persistence;
using TaskAPI.Persistence.Entities;
using ArgumentException = System.ArgumentException;

namespace TaskAPI.Application.Services;

public class UserService(IJwtProvider jwtProvider, IPasswordHasher passwordHasher, TaskApiDbContext context) : IUserService
{
    public async Task<Guid> Register(string name, string password, string phoneNumber, Guid companyId, bool isBoss)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phoneNumber))
            throw new ArgumentException("Name or phone number does not exist");
        
        if (companyId == Guid.Empty)
            throw new ArgumentException("Company does not exist");
        
        if (await context.Users.AnyAsync(u => u.Name == name))
            throw new ArgumentException("User already exists");
        
        if (await context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber))
            throw new ArgumentException("Phone number already exists");
        
        var passwordHash = passwordHasher.Generate(password);
        
        var user = User.CreateUser(Guid.NewGuid(), name, passwordHash, phoneNumber, companyId, isBoss);

        var userEntity = new UserEntity
        {
            Id = user.Id,
            Name = user.Name,
            PasswordHash = user.PasswordHash,
            PhoneNumber = user.PhoneNumber,
            CompanyId = user.CompanyId,
            IsBoss = user.IsBoss
        };

        await context.Users.AddAsync(userEntity);
        await context.SaveChangesAsync();

        return userEntity.Id;
    }

    public async Task<string> Login(string name, string password)
    {
        var user = await GetByName(name);

        if (!passwordHasher.Verify(password, user.PasswordHash!))
            throw new ArgumentException("Incorrect password");
        
        var token = jwtProvider.GenerateToken(user);

        return token;
    }

    public async Task<User> GetByName(string name)
    {
        var userEntity = await context.Users
                             .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Name == name)
                         ?? throw new ArgumentException("User does not exist");
        
        return User.CreateUser(userEntity.Id, userEntity.Name!, userEntity.PasswordHash!, userEntity.PhoneNumber!, 
            userEntity.CompanyId, userEntity.IsBoss);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await context.Users.Select(
            ue => User.CreateUser(ue.Id, ue.Name!, ue.PasswordHash!, ue.PhoneNumber!, ue.CompanyId, ue.IsBoss))
            .ToListAsync();
    }

    public async Task<List<User>> GetAllUsersByCompanyId(Guid companyId)
    {
        return await context.Users
            .Where(u => u.CompanyId == companyId)
            .Select(ue => 
                User.CreateUser(ue.Id, ue.Name!, ue.PasswordHash!, ue.PhoneNumber!, ue.CompanyId, ue.IsBoss))
            .ToListAsync();
    }
    
    public async Task<User> GetUserById(Guid id)
    {
        var userEntity = await context.Users
                             .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id)
                         ?? throw new ArgumentException("User does not exist");
        
        return User.CreateUser(userEntity.Id, userEntity.Name!, userEntity.PasswordHash!, userEntity.PhoneNumber!, 
            userEntity.CompanyId, userEntity.IsBoss);
    }

    public async Task<Guid> DeleteUser(Guid id)
    {
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }

    public async Task<Guid> UpdateUser(Guid id, string name, string password,
        string phoneNumber, Guid companyId, bool isBoss)
    {
        await context.Users
            .Where(u => u.Id == id)
            .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.Name, name)
                    .SetProperty(u => u.PasswordHash, passwordHasher.Generate(password))
                    .SetProperty(u => u.CompanyId, companyId)
                    .SetProperty(u => u.IsBoss, isBoss)
                    .SetProperty(u => u.PhoneNumber, phoneNumber));
        
        return id;
    }
}