using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private readonly List<Comment> Comments = [];
    public Task<Comment> AddAsync(Comment Comment)
    {
        Comment.Id =
            Comments.Count != 0 ?
                Comments.Max(p => p.Id) + 1
                    :
                1;
        Comments.Add(Comment);
        return Task.FromResult(Comment);
    }

    public Task UpdateAsync(Comment Comment)
    {
        Comment existingComment =
            Comments.SingleOrDefault(p => p.Id == Comment.Id)
                ?? throw new InvalidOperationException($"Comment with ID '{Comment.Id}' not found");
        Comments.Remove(existingComment);
        Comments.Add(Comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment CommentToRemove =
            Comments.SingleOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException($"Comment with ID '{id}' not found");
        Comments.Remove(CommentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        Comment Comment =
            Comments.SingleOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException($"Comment with ID '{id}' not found");
        return Task.FromResult(Comment);
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return Comments.AsQueryable();
    }
}