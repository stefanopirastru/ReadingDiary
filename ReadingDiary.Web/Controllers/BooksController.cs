using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ReadingDiary.Web.Models;
using System.Text;

namespace ReadingDiary.Web.Controllers;

public class BooksController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public BooksController(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index(
    string? search,
    int page = 1,
    int pageSize = 10)
    {
        var client = _httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.Add(
            "X-API-KEY",
            _configuration["ApiSettings:ApiKey"]);

        var url =
            $"{_configuration["ApiSettings:BaseUrl"]}/api/Books?page={page}&pageSize={pageSize}";

        if (!string.IsNullOrWhiteSpace(search))
        {
            url += $"&search={Uri.EscapeDataString(search)}";
        }

        var response = await client.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            ViewBag.Search = search;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.HasPreviousPage = false;
            ViewBag.HasNextPage = false;

            return View(new List<Book>());
        }

        var json = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<PagedResult<Book>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        ViewBag.Search = search;

        ViewBag.CurrentPage = result.Page;

        ViewBag.PageSize = result.PageSize;

        ViewBag.TotalPages = result.TotalPages;

        ViewBag.TotalCount = result.TotalCount;

        return View(result.Items);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        if (!ModelState.IsValid)
        {
            return View(book);
        }

        var client = _httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.Add(
            "X-API-KEY",
            _configuration["ApiSettings:ApiKey"]);

        var json = JsonSerializer.Serialize(book);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await client.PostAsync(
            $"{_configuration["ApiSettings:BaseUrl"]}/api/Books",
            content);

        if (!response.IsSuccessStatusCode)
        {
            return View(book);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    { 
        var client = _httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.Add(
            "X-API-KEY",
            _configuration["ApiSettings:ApiKey"]);

        var response = await client.GetAsync(
            $"{_configuration["ApiSettings:BaseUrl"]}/api/Books/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        var json = await response.Content.ReadAsStringAsync();

        var book = JsonSerializer.Deserialize<Book>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Book book)
    {
        if(!ModelState.IsValid)
        {
            return View(book);
        }

        var client = _httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.Add(
            "X-API-KEY",
            _configuration["ApiSettings:ApiKey"]);

        var json = JsonSerializer.Serialize(book);

        var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        var response = await client.PutAsync(
            $"{_configuration["ApiSettings:BaseUrl"]}/api/Books/{book.Id}",
            content);

        if (!response.IsSuccessStatusCode)
        {
            return View(book);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.Add(
            "X-API-KEY",
            _configuration["ApiSettings:ApiKey"]);

        var response = await client.GetAsync(
            $"{_configuration["ApiSettings:BaseUrl"]}/api/Books/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        var json = await response.Content.ReadAsStringAsync();

        var book = JsonSerializer.Deserialize<Book>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var client = _httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.Add(
            "X-API-KEY",
            _configuration["ApiSettings:ApiKey"]);

        await client.DeleteAsync(
            $"{_configuration["ApiSettings:BaseUrl"]}/api/Books/{id}");

        return RedirectToAction(nameof(Index));
    }
}