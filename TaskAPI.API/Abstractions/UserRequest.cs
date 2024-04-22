using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Abstractions;

public record UserRequest(
    [Required] string Name,
    [Required] string Password,
    [Required] string PhoneNumber,
    [Required] Guid CompanyId,
    [Required] bool IsBoss);