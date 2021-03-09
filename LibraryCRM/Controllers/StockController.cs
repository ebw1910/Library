using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryCRM.Data;
using LibraryCRM.Data.Models;
using LibraryCRM.Models;
using LibraryCRM.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryCRM.Controllers
{
    [Authorize(Roles = "Admin, Storekeeper")]
    public class StockController : Controller
    {
        private readonly IMapper _mapper;
        private IBookRepository repository;
        public StockController(IBookRepository repo, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            repository = repo;
        }
        public ViewResult Index() 
        {
            var model = _mapper.Map<IEnumerable<BookViewModel>>(repository.Books);
            return View(model);
        }
        
        public ViewResult Edit(int bookId)
        {
            var g = repository.Genres;
            var model = _mapper.Map<BookViewModel>(repository.Books
            .FirstOrDefault(p => p.BookID == bookId));
            ViewBag.Genres = new SelectList(g, "Id", "Name");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> EditAsync(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = _mapper.Map<Book>(model);
                await repository.SaveBookAsync(book);
                TempData["message"] = $"{book.Name} Сохранено";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(model);
            }
        }
        public ViewResult Create()
        {
            var g = repository.Genres;
            ViewBag.Genres = new SelectList(g, "Id", "Name");
            return View("Edit", new BookViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int bookID)
        {
            Book deletedBook = await repository.DeleteBookAsync(bookID);
            if (deletedBook != null)
            {
                TempData["message"] = $"{deletedBook.Name} Удалено";
            }
            return RedirectToAction("Index");
        }
    }
}
