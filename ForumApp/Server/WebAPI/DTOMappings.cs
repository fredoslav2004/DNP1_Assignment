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

    public static CreatePostDTO ToCreateDTO(this Entities.Post entity) => new(entity.Title, entity.Content, entity.AuthorId);

    public static PostDTO ToDTO(this Entities.Post entity) => new(entity.Id, entity.Title, entity.Content, entity.AuthorId);

    public static Entities.Post ToEntity(this PostDTO dto) =>
        new()
        {
            Id = dto.Id,
            Title = dto.Title,
            Content = dto.Content,
            AuthorId = dto.AuthorId
        };

    public static Entities.User ToEntity(this CreateUserDTO dto) =>
        new()
        {
            Name = dto.Name,
            Password = dto.Password
        };

    public static CreateUserDTO ToDTO(this Entities.User entity) => new(entity.Name, entity.Password!);

    public static Entities.Comment ToEntity(this CreateCommentDTO dto) =>
        new()
        {
            AuthorId = dto.AuthorId,
            PostId = dto.PostId,
            Content = dto.Content
        };

    public static Entities.Comment ToEntity(this CommentDTO dto) =>
        new()
        {
            AuthorId = dto.AuthorId,
            PostId = dto.PostId,
            Content = dto.Content
        };

    public static CreateCommentDTO ToCreateDTO(this Entities.Comment entity) => new(entity.AuthorId, entity.PostId, entity.Content);

    public static CommentDTO ToDTO(this Entities.Comment entity) => new(entity.Id, entity.AuthorId, entity.PostId, entity.Content);

    public static UserInfoDTO ToUserInfoDTO(this Entities.User entity) => new(entity.Id, entity.Name);

    public static Entities.User ToEntity(this UpdateUserDTO dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Password = dto.Password
        };
}
