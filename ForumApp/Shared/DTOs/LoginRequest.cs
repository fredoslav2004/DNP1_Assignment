using System.ComponentModel.DataAnnotations;

namespace DTOs;

public record class LoginRequest
(
    [Required, StringLength(50)] string Name,
    [Required, StringLength(50)] string Password
);
    
