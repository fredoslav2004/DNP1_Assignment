using System.Text.Json;
using Entities;
using RepositoryContracts;
using Ut = Utils.Utils;

namespace FileRepositories;

public class FileRepository<T> : IRepository<T> where T : class, IIdentifiable
{
    private readonly string repositoryName;
    private readonly string suffix = ".json";
    private string relativePath => $"{repositoryName}{suffix}";
    private string GetFilePath() => Ut.GetPersistantFilePath(relativePath);
    public FileRepository(string repositoryName)
    {
        this.repositoryName = repositoryName;
        var _path = GetFilePath();
        if (!File.Exists(_path))
        {
            File.WriteAllText(_path, "[]");
        }
    }
    public async Task<T> AddAsync(T entity)
    {
        await PerformActionOnEntities(async items =>
        {
            int maxId = items.Count == 0 ? 0 : items.Max(e => e.Id);
            entity.Id = maxId + 1;
            items.Add(entity);
            await Task.CompletedTask;
        });
        return entity;
    }

    public Task ClearAsync()
    {
        return PerformActionOnEntities(async items =>
        {
            items.Clear();
            await Task.CompletedTask;
        });
    }

    public Task DeleteAsync(int id)
    {
        return PerformActionOnEntities(async items =>
        {
            var itemToRemove = items.SingleOrDefault(e => e.Id == id);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
            else
            {
                throw new NotFoundException($"Entity with id {id} not found");
            }
            await Task.CompletedTask;
        });
    }

    public IQueryable<T> GetMany()
    {
        string entitiesAsJson = File.ReadAllTextAsync(GetFilePath()).Result;
        List<T> items = JsonSerializer.Deserialize<List<T>>(entitiesAsJson)!;
        return items.AsQueryable();
    }

    public Task<T> GetSingleAsync(int id)
    {
        IQueryable<T> items = GetMany();
        var item = items.SingleOrDefault(e => e.Id == id);
        return Task.FromResult(item ?? throw new NotFoundException($"Entity with id {id} not found"));
    }

    public Task UpdateAsync(T entity)
    {
        return PerformActionOnEntities(async items =>
        {
            var existingItem = items.SingleOrDefault(e => e.Id == entity.Id);
            if (existingItem != null)
            {
                items.Remove(existingItem);
                items.Add(entity);
            }
            else
            {
                throw new NotFoundException($"Entity with id {entity.Id} not found");
            }
            await Task.CompletedTask;
        });
    }

    private async Task PerformActionOnEntities(Func<List<T>, Task> action)
    {
        // Read the file
        string entitiesJSON = await File.ReadAllTextAsync(GetFilePath());
        List<T> items = JsonSerializer.Deserialize<List<T>>(entitiesJSON)!;
        // Perform the action
        await action(items);
        // Write the file
        entitiesJSON = JsonSerializer.Serialize(items);
        await File.WriteAllTextAsync(GetFilePath(), entitiesJSON);
    }
}
