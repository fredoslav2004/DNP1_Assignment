using System.ComponentModel.DataAnnotations;

namespace DTOs;

public record class CreatePostDTO(
    [Required, StringLength(100)] string Title,
    [Required] string Content,
    [Required, Range(0, int.MaxValue)] int AuthorId);
