using System.Collections.Generic;
using System.Threading.Tasks;
using NewsBlazorApp.Models;
using NewsBlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace NewsBlazorApp.Components.Pages;

public partial class Home 
{

    [Inject] public NewsService NewsService { get; set; } = default!;

    private List<Article> articles = new();
    private bool loading = false;
    private string searchQuery = "";
    private string selectedCategory = "";
    private int currentPage = 1;

    protected override async Task OnInitializedAsync()
    {
        await LoadNews();
    }

    private async Task LoadNews()
    {
        loading = true;
        StateHasChanged();

        var response = await NewsService.GetTopHeadlinesAsync(
            country: "us",
            category: selectedCategory,
            query: searchQuery,
            page: currentPage);

        articles = response.Articles;
        loading = false;
        StateHasChanged();
    }

    private async Task ChangePage(int page)
    {
        if (page < 1) return;
        currentPage = page;
        await LoadNews();
    }

}
