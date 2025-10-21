using System;
using DTOs;

namespace WebAPI;

public static class DTOMappings
{
    public static Entities.Post ToEntity(this CreatePostDTO dto) =>
        new()
        {
            Title = dto.Title,
            Content = dto.Content,
            AuthorId = dto.AuthorId
        };

    public static CreatePostDTO ToDTO(this Entities.Post entity) => new(entity.Title, entity.Content, entity.AuthorId);
}
