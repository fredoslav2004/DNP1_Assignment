using System;
using DTOs;

namespace BlazorApp.Services;

public class HttpPostService(HttpCrudService crudService) : IPostService
{
    private readonly HttpCrudService crudService = crudService;
    public async Task<PostDTO> AddPostAsync(CreatePostDTO request) => await crudService.CreateAsync<PostDTO, CreatePostDTO>("posts", request);
    public Task DeletePostAsync(int id) => crudService.DeleteAsync($"posts/{id}");
    public async Task UpdatePostAsync(PostDTO request) => await crudService.UpdateAsync<PostDTO, PostDTO>($"posts/update/{request.Id}", request);
    public async Task<PostDTO> GetPostAsync(int id) => await crudService.GetAsync<PostDTO>("posts", id);
    public async Task<List<PostDTO>> GetPostsAsync() => await crudService.GetAsync<List<PostDTO>>("posts");
}
