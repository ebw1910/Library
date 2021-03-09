using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryCRM.Data.Models;
using LibraryCRM.Models;
using LibraryCRM.Models.ViewModels;

namespace LibraryCRM
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IEnumerable<Book>, BookListViewModel>()
                .ForMember(destination => destination.Books
                    , options => options.MapFrom(src => src));
            CreateMap<Book, BookViewModel>();
            CreateMap<BookViewModel, Book>();
            CreateMap<Genre, GenreViewModel>();
            CreateMap<Transfer, TransferViewModel>();
            CreateMap<TransferViewModel, Transfer>();
        }
    }
}
