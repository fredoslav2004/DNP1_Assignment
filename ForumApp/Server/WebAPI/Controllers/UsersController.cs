using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController(IRepository<User> userRepo) : ControllerBase
    {
        private readonly IRepository<User> userRepo = userRepo;

        [HttpPost]
        public async Task<ActionResult<UserInfoDTO>> AddUser([FromBody] CreateUserDTO user)
        {
            User? addedUser = await userRepo.AddAsync(user.ToEntity());
            return addedUser == null ? BadRequest() : Created($"/users/{addedUser.Id}", addedUser.ToUserInfoDTO());
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserInfoDTO>> GetUserInfos([FromQuery] string? nameContains = null)
        {
            var users = userRepo.GetMany();
            users = users.Where(user => nameContains == null || user.Name.Contains(nameContains, StringComparison.OrdinalIgnoreCase));
            return users == null ? NotFound() : Ok(users.Select(user => user.ToUserInfoDTO()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserInfoDTO>> GetUserInfo(int id)
        {
            User user = await userRepo.GetSingleAsync(id);
            return user == null ? NotFound() : Ok(user.ToUserInfoDTO());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO user)
        {
            if (id != user.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the body");
            }

            await userRepo.UpdateAsync(user.ToEntity());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await userRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
