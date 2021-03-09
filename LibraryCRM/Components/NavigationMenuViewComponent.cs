using LibraryCRM.Data;
using LibraryCRM.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LibraryCRM.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IBookRepository repository;

        public NavigationMenuViewComponent(IBookRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedGenres = RouteData?.Values["genres"];
            return View(repository.Books
            .Select(x => x.Genres.Name)
            .Distinct()
            .OrderBy(x => x));
        }
    }
}
