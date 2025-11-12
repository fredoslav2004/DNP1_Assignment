using System;
using DTOs;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserInfoDTO> AddUserAsync(CreateUserDTO request);
    public Task UpdateUserAsync(UpdateUserDTO request);
    public Task DeleteUserAsync(int id);
    public Task<UserInfoDTO> GetUserAsync(int id);
    public Task<List<UserInfoDTO>> GetUsersAsync();
}
