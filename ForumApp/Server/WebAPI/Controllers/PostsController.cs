using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController(IRepository<Post> postRepo, IRepository<Comment> commentRepo) : ControllerBase
    {
        private readonly IRepository<Post> postRepo = postRepo;
        private readonly IRepository<Comment> commentRepo = commentRepo;

        [HttpPost]
        public async Task<ActionResult<PostDTO>> AddPost([FromBody] CreatePostDTO post)
        {
            Post? addedPost = await postRepo.AddAsync(post.ToEntity());
            return addedPost == null ? BadRequest() : Created($"/posts/{addedPost.Id}", addedPost.ToDTO());
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostDTO>> GetPosts([FromQuery] string? titleContains = null, [FromQuery] int? writtenByID = null, [FromQuery] bool sortByCommentCount = false)
        {
            var posts = postRepo.GetMany();

            posts = posts.
                        Where(post => titleContains == null || post.Title.Contains(titleContains, StringComparison.OrdinalIgnoreCase) 
                        && (writtenByID == null || post.AuthorId == writtenByID));

            if (sortByCommentCount)
            {
                posts = posts.OrderByDescending(post => commentRepo.GetMany().Count(comment => comment.PostId == post.Id));
            }

            return posts == null ? NotFound() : Ok(posts.Select(post => post.ToDTO()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPost(int id)
        {
            Post post = await postRepo.GetSingleAsync(id);
            return post == null ? NotFound() : Ok(post.ToDTO());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(int id, [FromBody] PostDTO post)
        {
            if (id != post.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the body");
            }

            await postRepo.UpdateAsync(post.ToEntity());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            await postRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
