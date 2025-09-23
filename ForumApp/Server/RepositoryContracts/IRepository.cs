using System;

namespace RepositoryContracts;

public interface IRepository<T> where T : class
{
    Task<T> AddAsync(T entity); // Takes an entity and returns the created entity
    Task UpdateAsync(T entity); // Takes an entity and replaces it by ID or throws an exception if ID not present
    Task DeleteAsync(int id); // Takes an ID and deletes the entity or throws an exception if ID not present
    Task<T> GetSingleAsync(int id); // Takes an ID and returns the entity or throws an exception if ID not present
    IQueryable<T> GetMany(); // Returns all entities as IQueryable
    Task ClearAsync();
}
