using System.ComponentModel.DataAnnotations;

namespace DTOs;

public record class CreateCommentDTO(    
    [Required] int AuthorId,
    [Required] int PostId,
    [Required] string Content
);
