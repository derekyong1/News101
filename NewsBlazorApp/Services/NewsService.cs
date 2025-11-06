using NewsBlazorApp.Models;
using System.Text.Json;

namespace NewsBlazorApp.Services;

public class NewsService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;
    private readonly JsonSerializerOptions _jsonOptions;

    public NewsService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    private string ApiKey => _config["NewsApi:Key"]!;
    private string BaseUrl => _config["NewsApi:BaseUrl"]!;

    public async Task<NewsResponse> GetTopHeadlinesAsync(
        string country = "us",
        string? category = null,
        string? query = null,
        int page = 1,
        int pageSize = 20)
    {
        var url = $"{BaseUrl}/top-headlines?country={country}&pageSize={pageSize}&page={page}&apiKey={ApiKey}";

        if (!string.IsNullOrWhiteSpace(category))
            url += $"&category={category}";

        if (!string.IsNullOrWhiteSpace(query))
            url += $"&q={Uri.EscapeDataString(query)}";

        var response = await _http.GetStringAsync(url);
        return JsonSerializer.Deserialize<NewsResponse>(response, _jsonOptions)!;
    }
}