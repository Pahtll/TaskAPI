using TaskAPI.Domain.Models;

namespace TaskAPI.Domain.Abstractions;

public interface IJwtProvider
{
    string GenerateToken(User user);
}