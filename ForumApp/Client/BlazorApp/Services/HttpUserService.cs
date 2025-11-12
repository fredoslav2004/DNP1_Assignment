using System;
using System.Text.Json;
using DTOs;

namespace BlazorApp.Services;

public class HttpUserService(HttpCrudService crudService) : IUserService
{
    private readonly HttpCrudService crudService = crudService;

    public async Task<UserInfoDTO> AddUserAsync(CreateUserDTO request) => await crudService.CreateAsync<UserInfoDTO, CreateUserDTO>("users", request);
    public async Task UpdateUserAsync(UpdateUserDTO request) => await crudService.UpdateAsync<UserInfoDTO, UpdateUserDTO>($"users/update/{request.Id}", request);
    public async Task DeleteUserAsync(int id) => await crudService.DeleteAsync($"users/{id}");
    public async Task<UserInfoDTO> GetUserAsync(int id) => await crudService.GetAsync<UserInfoDTO>("users", id);
}
