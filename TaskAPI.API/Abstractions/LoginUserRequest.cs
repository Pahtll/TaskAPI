using System.ComponentModel.DataAnnotations;

namespace TaskAPI.Abstractions;

public record LoginUserRequest(
    [Required]string Name,
    [Required]string Password);