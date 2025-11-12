using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IRepository<User> userRepo) : ControllerBase
    {
        private readonly IRepository<User> userRepo = userRepo;

        [HttpPost("login")]
        public async Task<ActionResult<UserInfoDTO>> Login([FromBody] LoginRequest loginRequest)
        {
            var users = userRepo.GetMany();
            var user = users.SingleOrDefault(u => u.Name == loginRequest.Name);
            if(user == null)
            {
                // No such user
                return Unauthorized();
            }
            if (user.Password != loginRequest.Password)
            {
                // Wrong password
                user = null;
                return Unauthorized();
            }
            else
            {
                // Successful login
                return Ok(user.ToUserInfoDTO());
            }
        }
    }
}
