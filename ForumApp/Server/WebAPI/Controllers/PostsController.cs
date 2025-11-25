using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts([FromQuery] string? titleContains = null, [FromQuery] int? writtenByID = null, [FromQuery] bool sortByCommentCount = false)
        {
            var postsQuery = postRepo.GetMany();

            postsQuery = postsQuery.
                        Where(post => titleContains == null || post.Title.Contains(titleContains, StringComparison.OrdinalIgnoreCase)
                        && (writtenByID == null || post.AuthorId == writtenByID));

            if (sortByCommentCount)
            {
                postsQuery = postsQuery.OrderByDescending(post => commentRepo.GetMany().Count(comment => comment.PostId == post.Id));
            }

            var posts = await postsQuery.ToListAsync();

            return posts == null ? NotFound() : Ok(posts.Select(post => post.ToDTO()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPostOld(int id)
        {
            Post post = await postRepo.GetSingleAsync(id);
            return post == null ? NotFound() : Ok(post.ToDTO());
        }

        [HttpGet("{id}/full")]
        public async Task<ActionResult<PostFullDTO>> GetPost(int id)
        {
            var postQuery = postRepo.GetMany().Where(p => p.Id == id).AsQueryable(); // AsQueryable basically not needed
            postQuery = postQuery.Include(p => p.Author);
            postQuery = postQuery.Include(p => p.Comments);
            PostFullDTO? post = await postQuery.Select(p =>
            new PostFullDTO(
                p.Id,
                p.Title,
                p.Content,
                new UserInfoDTO(p.Author.Id, p.Author.Name),
                p.Comments.Select(c => new CommentDTO(c.Id, c.AuthorId, c.PostId, c.Content)).ToList()
            )).SingleOrDefaultAsync();
            return post == null ? NotFound() : Ok(post);
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
