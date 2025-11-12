using System.ComponentModel.DataAnnotations;

namespace DTOs;

public record class CommentDTO(
    [Required, Range(0, int.MaxValue)] int Id,
    [Required, Range(0, int.MaxValue)] int AuthorId,
    [Required, Range(0, int.MaxValue)] int PostId,
    [Required] string Content
);
