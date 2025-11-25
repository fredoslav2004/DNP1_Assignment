using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace EfcRepositories;

public class EfcRepository<T>(AppContext context) : IRepository<T> where T : class, IIdentifiable
{
    private readonly AppContext ctx = context;

    public async Task<T> AddAsync(T entity)
    {
        await ctx.Set<T>().AddAsync(entity);
        await ctx.SaveChangesAsync();
        return entity;
    }

    public async Task ClearAsync()
    {

    }

    public async Task DeleteAsync(int id)
    {
        T? existing = await ctx.Set<T>().SingleOrDefaultAsync(p => p.Id == id) ?? throw new NotFoundException($"Entity with id {id} not found");
        ctx.Set<T>().Remove(existing);
        await ctx.SaveChangesAsync();

    }

    public IQueryable<T> GetMany()
    {
        return ctx.Set<T>().AsQueryable();
    }

    public async Task<T> GetSingleAsync(int id)
    {
        T? existing = await ctx.Set<T>().SingleOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException($"Entity with id {id} not found");
        return existing;
    }

    public async Task UpdateAsync(T entity)
    {
        if (!await ctx.Set<T>().AnyAsync(e => e.Id == entity.Id))
        {
            throw new KeyNotFoundException($"Entity with id {entity.Id} not found");
        }

        ctx.Set<T>().Update(entity);
        await ctx.SaveChangesAsync();

    }
}
