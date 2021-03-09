using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryCRM.Data;
using LibraryCRM.Models;
using LibraryCRM.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryCRM.Controllers
{
    // [Authorize(Roles = "Admin, Librarian")]
    public class BookController : Controller
    {
     
        private readonly IMapper _mapper;
        private readonly IBookRepository _repository;

        private const int PageSize = 4;

        public BookController(IBookRepository repo, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<ViewResult> Index(string genres, int bookPage = 1, string search = "")
        {
            
            var bookList = await _repository.Books.Include(p => p.Genres).ToListAsync();

            if (!string.IsNullOrEmpty(search))
            {
                bookList = bookList.Where(c => c.Name.Contains(search) || c.Author.Contains(search) || c.Genres.Name.Contains(search)).ToList();
            }
            
            var viewModel = _mapper.Map<BookListViewModel>(bookList
                .Where(p => genres == null || p.Genres.Name == genres)
                .OrderBy(p => p.BookID)
                .Skip((bookPage - 1) * PageSize)
                .Take(PageSize));

            viewModel.CurrentGenres = genres;
            viewModel.PagingInfo = new PagingInfo
            {
                CurrentPage = bookPage,
                ItemsPerPage = PageSize,
                TotalItems = genres == null ? bookList.Count : bookList.Count(e => e.Genres.Name == genres)
            };
            
            return View(viewModel);
        }
    }
}
