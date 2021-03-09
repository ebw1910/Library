using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryCRM.Data;
using LibraryCRM.Data.Models;
using LibraryCRM.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryCRM.Controllers
{
    public class TransferController : Controller
    {
        private readonly IMapper _mapper;
        private ITransferRepository repository;
        private IBookRepository bookRepository;
        private Take take;
        public TransferController(ITransferRepository repoService, Take takeService, IBookRepository bookrepo, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            bookRepository = bookrepo;
            repository = repoService;
            take = takeService;
        }

        public ViewResult List()
        {
            var model = _mapper.Map<IEnumerable<TransferViewModel>>(repository.Transfers.Where(o => !o.Shipped));
            return View(model);
        }
           
        [HttpPost]
        public async Task<IActionResult> MarkShipped(int transferID)
        {
            Transfer transfer = repository.Transfers
                .FirstOrDefault(o => o.TransferID == transferID);
            if (transfer != null)
            {
                var books = bookRepository.Books;
                foreach (var item in transfer.Lines)
                {
                    var i = books.FirstOrDefault(a => a.BookID == item.Book.BookID);
                    i.Count += item.Quantity;
                    await bookRepository.SaveBookAsync(i);

                }
                transfer.Shipped = true;
             await repository.SaveTransferAsync(transfer);
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout() => View(new TransferViewModel());

        [HttpPost]
        public async Task<IActionResult> CheckoutAsync(TransferViewModel transfer)
        {
            if (take.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                transfer.Lines = take.Lines.ToArray();
                var model = _mapper.Map<Transfer>(transfer);
                await repository.SaveTransferAsync(model);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(transfer);
            }
        }

        public ViewResult Completed()
        {
            take.Clear();
            return View();
        }
    }
}
