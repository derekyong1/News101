namespace NewsBlazorApp.Models;

public class NewsResponse
{
    public string Status { get; set; } = string.Empty;
    public int TotalResults { get; set; }
    public List<Article> Articles { get; set; } = new();
}