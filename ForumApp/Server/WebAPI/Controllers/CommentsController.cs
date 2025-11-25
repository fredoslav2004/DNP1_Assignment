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
    public class CommentsController(IRepository<Comment> commentRepo, IRepository<User> userRepo) : ControllerBase
    {
        private readonly IRepository<Comment> commentRepo = commentRepo;
        private readonly IRepository<User> userRepo = userRepo;

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> AddComment([FromBody] CreateCommentDTO comment)
        {
            Comment? addedComment = await commentRepo.AddAsync(comment.ToEntity());
            return addedComment == null ? BadRequest() : Created($"/comments/{addedComment.Id}", addedComment.ToDTO());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments([FromQuery] int? writtenByID = null, [FromQuery] string? writtenByName = null, [FromQuery] string? contentContains = null, [FromQuery] int? postId = null)
        {
            var commentsQuery = commentRepo.GetMany();
            commentsQuery = commentsQuery.Where(comment =>
                (writtenByID == null || comment.AuthorId == writtenByID) &&
                (writtenByName == null || userRepo.GetMany().Any(user => user.Id == comment.AuthorId && user.Name.Contains(writtenByName, StringComparison.OrdinalIgnoreCase))) &&
                (contentContains == null || comment.Content.Contains(contentContains, StringComparison.OrdinalIgnoreCase)) &&
                (postId == null || comment.PostId == postId)
            );

            var comments = await commentsQuery.ToListAsync();

            return comments == null ? NotFound() : Ok(comments.Select(comment => comment.ToDTO()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(int id)
        {
            Comment comment = await commentRepo.GetSingleAsync(id);
            return comment == null ? NotFound() : Ok(comment.ToDTO());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateComment(int id, [FromBody] CommentDTO comment)
        {
            if (id != comment.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the body");
            }

            await commentRepo.UpdateAsync(comment.ToEntity());
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            await commentRepo.DeleteAsync(id);
            return NoContent();
        }
    }
}
