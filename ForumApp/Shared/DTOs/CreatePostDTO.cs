using System.ComponentModel.DataAnnotations;

namespace DTOs;

public record class CreatePostDTO(
    [property: Required, StringLength(100)] string Title,
    [property: Required] string Content,
    [property: Required, Range(0, int.MaxValue)] int AuthorId);
