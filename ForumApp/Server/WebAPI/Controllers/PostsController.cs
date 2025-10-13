using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController(IRepository<Post> postRepo) : ControllerBase
    {
        private readonly IRepository<Post> postRepo = postRepo;

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost([FromBody] Post post)
        {
            Post addedPost = await postRepo.AddAsync(post);
            return Created($"/posts/{addedPost.Id}", addedPost);
        }
    }
}
