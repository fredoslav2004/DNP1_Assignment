using System.ComponentModel.DataAnnotations;

namespace DTOs;

public record class UserInfoDTO
(
    [Required, Range(0, int.MaxValue)] int Id,
    [Required, StringLength(50)] string Name
);