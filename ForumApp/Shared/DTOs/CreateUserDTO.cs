using System.ComponentModel.DataAnnotations;

namespace DTOs;

public record class CreateUserDTO
(
    [Required, StringLength(50)] string Name,
    [Required, StringLength(50)] string Password
);
    
