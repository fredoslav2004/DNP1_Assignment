using System;
using DTOs;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserInfoDTO?> GetUserInfoAsync(int userId);
    public Task<IEnumerable<UserInfoDTO>> GetUserInfosAsync(string? nameContains = null);
}
