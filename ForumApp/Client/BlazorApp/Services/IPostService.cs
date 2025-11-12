using System;
using DTOs;

namespace BlazorApp.Services;

public interface IPostService
{
    public Task<PostDTO> AddPostAsync(CreatePostDTO request);
    public Task UpdatePostAsync(PostDTO request);
    public Task DeletePostAsync(int id);
    public Task<PostDTO> GetPostAsync(int id);
    public Task<List<PostDTO>> GetPostsAsync();
}
