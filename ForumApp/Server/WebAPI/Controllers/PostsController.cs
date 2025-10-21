using DTOs;
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
        public async Task<ActionResult<Post>> AddPost([FromBody] CreatePostDTO post)
        {
            Post? addedPost = await postRepo.AddAsync(post.ToEntity());
            return addedPost == null ? BadRequest() : Created($"/posts/{addedPost.Id}", addedPost);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPosts()
        {
            var posts = postRepo.GetMany();
            return posts == null ? NotFound() : Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            Post post = await postRepo.GetSingleAsync(id);
            return post == null ? NotFound() : Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the body");
            }

            await postRepo.UpdateAsync(post);
            return NoContent();
        }        
    }
}
