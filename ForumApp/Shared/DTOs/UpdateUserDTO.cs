using System.ComponentModel.DataAnnotations;

namespace DTOs;

public record class UpdateUserDTO
(
    [Required, Range(0, int.MaxValue)] int Id,
    [Required, StringLength(50)] string Name,
    [Required, StringLength(50)] string Password
);
    
