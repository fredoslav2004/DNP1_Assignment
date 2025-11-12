using System;
using System.Text.Json;

namespace BlazorApp.Services;

public class HttpCrudService(HttpClient client)
{
    private readonly HttpClient client = client;
    private readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<T> CreateAsync<T, R>(string endpoint, R request)
    {
        HttpResponseMessage httpResponse = await client.PostAsJsonAsync(endpoint, request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Exception: {response}");
        }
        return JsonSerializer.Deserialize<T>(response, jsonOptions)!;
    }

    public async Task<T> GetAsync<T>(string endpoint, int? id = null)
    {
        var finalEndpoint = id.HasValue ? $"{endpoint}/{id.Value}" : endpoint;
        HttpResponseMessage httpResponse = await client.GetAsync(finalEndpoint);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Exception: {response}");
        }
        return JsonSerializer.Deserialize<T>(response, jsonOptions)!;
    }

    public async Task DeleteAsync(string endpoint, int? id = null)
    {
        var finalEndpoint = id.HasValue ? $"{endpoint}/{id.Value}" : endpoint;
        HttpResponseMessage httpResponse = await client.DeleteAsync(finalEndpoint);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Exception: {response}");
        }
    }

    public async Task<T> UpdateAsync<T, R>(string endpoint, R request, int? id = null)
    {
        var finalEndpoint = id.HasValue ? $"{endpoint}/{id.Value}" : endpoint;
        HttpResponseMessage httpResponse = await client.PutAsJsonAsync(finalEndpoint, request);
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception($"Exception: {response}");
        }
        return JsonSerializer.Deserialize<T>(response, jsonOptions)!;
    }
}
