namespace RepositoryContracts;
using Entities;

public interface IPostRepository
{
    Task<Post> AddAsync(Post post); // Takes a post and returns the created post
    Task UpdateAsync(Post post); // Takes a post and replaces it by ID or throws an exception if ID not present
    Task DeleteAsync(int id); // Takes an ID and deletes the post or throws an exception if ID not present
    Task<Post> GetSingleAsync(int id); // Takes an ID and returns the post or throws an exception if ID not present
    IQueryable<Post> GetManyAsync(); // Returns all posts as IQueryable
}