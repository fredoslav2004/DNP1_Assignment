using System;
using DTOs;

namespace BlazorApp.Services;

public class HttpCommentService(HttpCrudService crudService) : ICommentService
{
    private readonly HttpCrudService crudService = crudService;
    public Task<CommentDTO> AddCommentAsync(CreateCommentDTO request) => crudService.CreateAsync<CommentDTO, CreateCommentDTO>("comments", request);
    public Task DeleteCommentAsync(int id) => crudService.DeleteAsync($"comments/{id}");
    public Task<CommentDTO> GetCommentAsync(int id) => crudService.GetAsync<CommentDTO>($"comments/{id}");
    public Task<List<CommentDTO>> GetCommentsAsync() => crudService.GetAsync<List<CommentDTO>>("comments");
    public Task UpdateCommentAsync(CommentDTO request) => crudService.UpdateAsync<CommentDTO, CommentDTO>("comments", request);
    public Task<List<CommentDTO>> GetCommentsByPostIdAsync(int postId) => crudService.GetAsync<List<CommentDTO>>($"comments?postId={postId}");
}
