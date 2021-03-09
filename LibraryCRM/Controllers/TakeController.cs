using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryCRM.Data;
using LibraryCRM.Data.Models;
using LibraryCRM.Models;
using LibraryCRM.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryCRM.Controllers
{
    public class TakeController : Controller
    {
        private IBookRepository repository;
        private Take take;

        public TakeController(IBookRepository repo, Take takeService)
        {
            repository = repo;
            take = takeService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new TakeIndexViewModel
            {
                Take = take,
                ReturnUrl = returnUrl
            });
        }

        public async Task<RedirectToActionResult> AddToTakeAsync(int bookId, string returnUrl)
        {
            Book book = repository.Books
                .FirstOrDefault(p => p.BookID == bookId);
            if (book != null && book.Count != 0)
            {
                take.AddItem(book, 1);
                book.Count--;
               await repository.SaveBookAsync(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public async Task<RedirectToActionResult> RemoveFromTakeAsync(int bookId,
                string returnUrl)
        {
            Book book = repository.Books
                .FirstOrDefault(p => p.BookID == bookId);

            if (book != null)
            {
                take.RemoveLine(book);
                book.Count++;
               await repository.SaveBookAsync(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
