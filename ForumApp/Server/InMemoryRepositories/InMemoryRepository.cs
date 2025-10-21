using System;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class InMemoryRepository<T> : IRepository<T> where T : class, IIdentifiable
{
    private readonly List<T> Ts = [];
    public Task<T> AddAsync(T T)
    {
        T.Id =
            Ts.Count != 0 ?
                Ts.Max(p => p.Id) + 1
                    :
                1;
        Ts.Add(T);
        return Task.FromResult(T);
    }

    public Task UpdateAsync(T T)
    {
        T existingT =
            Ts.SingleOrDefault(p => p.Id == T.Id)
                ?? throw new NotFoundException($"T with ID '{T.Id}' not found");
        Ts.Remove(existingT);
        Ts.Add(T);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        T TToRemove =
            Ts.SingleOrDefault(p => p.Id == id)
                ?? throw new NotFoundException($"T with ID '{id}' not found");
        Ts.Remove(TToRemove);
        return Task.CompletedTask;
    }

    public Task<T> GetSingleAsync(int id)
    {
        T T =
            Ts.SingleOrDefault(p => p.Id == id)
                ?? throw new NotFoundException($"T with ID '{id}' not found");
        return Task.FromResult(T);
    }

    public IQueryable<T> GetMany()
    {
        return Ts.AsQueryable();
    }

    public Task ClearAsync()
    {
        Ts.Clear();
        return Task.CompletedTask;
    }
}
