using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> Users = [];
    public Task<User> AddAsync(User User)
    {
        User.Id =
            Users.Count != 0 ?
                Users.Max(p => p.Id) + 1
                    :
                1;
        Users.Add(User);
        return Task.FromResult(User);
    }

    public Task UpdateAsync(User User)
    {
        User existingUser =
            Users.SingleOrDefault(p => p.Id == User.Id)
                ?? throw new InvalidOperationException($"User with ID '{User.Id}' not found");
        Users.Remove(existingUser);
        Users.Add(User);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User UserToRemove =
            Users.SingleOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException($"User with ID '{id}' not found");
        Users.Remove(UserToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User User =
            Users.SingleOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException($"User with ID '{id}' not found");
        return Task.FromResult(User);
    }

    public IQueryable<User> GetManyAsync()
    {
        return Users.AsQueryable();
    }

    public Task ClearAsync()
    {
        Users.Clear();
        return Task.CompletedTask;
    }
}