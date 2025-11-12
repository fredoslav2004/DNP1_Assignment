using System;
using DTOs;

namespace BlazorApp.Services;

public interface IUserService
{
    public Task<UserInfoDTO> AddUserAsync(CreateUserDTO request);
    
}
