using System;
using DTOs;

namespace BlazorApp.Services;

public interface ICommentService
{
    public Task<CommentDTO> AddCommentAsync(CreateCommentDTO request);
    public Task UpdateCommentAsync(CommentDTO request);
    public Task DeleteCommentAsync(int id);
    public Task<CommentDTO> GetCommentAsync(int id);
    public Task<List<CommentDTO>> GetCommentsAsync();
    public Task<List<CommentDTO>> GetCommentsByPostIdAsync(int postId);
}
