using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Abstractions;

public record CompanyResponse(
    [Required] Guid Id,
    [Required] string CompanyName,
    [Required] Guid BossId,
    [Required] string CountryName);