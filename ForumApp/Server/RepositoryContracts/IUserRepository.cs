namespace RepositoryContracts;
using Entities;

public interface IUserRepository
{
    Task<User> AddAsync(User User); // Takes a User and returns the created User
    Task UpdateAsync(User User); // Takes a User and replaces it by ID or throws an exception if ID not present
    Task DeleteAsync(int id); // Takes an ID and deletes the User or throws an exception if ID not present
    Task<User> GetSingleAsync(int id); // Takes an ID and returns the User or throws an exception if ID not present
    IQueryable<User> GetManyAsync(); // Returns all Users as IQueryable
}