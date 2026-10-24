using Microsoft.AspNetCore.Mvc;
using ReadingDiary.Web.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;

namespace ReadingDiary.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            client.DefaultRequestHeaders.Add(
                "X-API-KEY",
                _configuration["ApiSettings:ApiKey"]);

            var response = await client.GetAsync(
                $"{_configuration["ApiSettings:BaseUrl"]}/api/Books");

            if (!response.IsSuccessStatusCode)
            {
                return View();
            }

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<PagedResult<Book>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            result ??= new PagedResult<Book>();

            var books = result.Items;

            ViewBag.TotalBooks = books.Count;

            ViewBag.CompletedBooks =
                books.Count(b => b.Status == "Completed");

            ViewBag.ReadingBooks =
                books.Count(b => b.Status == "Reading");

            ViewBag.ToReadBooks =
                books.Count(b => b.Status == "To Read");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
