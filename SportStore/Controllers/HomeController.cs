using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreRepository repository;
        public int pageSize = 4;
        public HomeController(ILogger<HomeController> logger, IStoreRepository repo)
        {
            _logger = logger;
            repository = repo;
        }

        public IActionResult Index(string? category, int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = repository.GetProducts
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemPerPage = pageSize,
                    TotalItems = category == null ? repository.GetProducts.Count() : repository.GetProducts
                    .Where(c => c.Category == category).Count()
                },
                currentCategory = category

            });          
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
