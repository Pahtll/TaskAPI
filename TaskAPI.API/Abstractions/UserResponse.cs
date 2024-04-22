using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Abstractions;

public record UserResponse(
    [Required] Guid Id,
    [Required] string Name,
    [Required] string PasswordHash,
    [Required] string PhoneNumber,
    [Required] Guid CompanyId,
    [Required] bool IsBoss);