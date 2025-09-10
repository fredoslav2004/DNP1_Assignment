namespace RepositoryContracts;
using Entities;

public interface ICommentRepository
{
    Task<Comment> AddAsync(Comment Comment); // Takes a Comment and returns the created Comment
    Task UpdateAsync(Comment Comment); // Takes a Comment and replaces it by ID or throws an exception if ID not present
    Task DeleteAsync(int id); // Takes an ID and deletes the Comment or throws an exception if ID not present
    Task<Comment> GetSingleAsync(int id); // Takes an ID and returns the Comment or throws an exception if ID not present
    IQueryable<Comment> GetManyAsync(); // Returns all Comments as IQueryable
}