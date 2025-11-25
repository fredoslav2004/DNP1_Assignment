using System.ComponentModel.DataAnnotations;

namespace DTOs;

public record class PostFullDTO(
    [Required, Range(0, int.MaxValue)] int Id,
    [Required, StringLength(100)] string Title,
    [Required] string Content,
    [Required, Range(0, int.MaxValue)] UserInfoDTO Author,
    IEnumerable<CommentDTO> Comments);
