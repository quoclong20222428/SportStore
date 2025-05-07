using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Component
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        public IStoreRepository repository;
        public NavigationMenuViewComponent(IStoreRepository repo)
        {
            repository = repo;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.GetProducts
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
