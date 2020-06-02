using AutoMapper;
using LibraryApi.Domain;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace LibraryApi.Mappers
{
    public class EfBooksMapper : IMapBooks
    {
        LibraryDataContext Context;
        IMapper Mapper;
        MapperConfiguration Config;

        public EfBooksMapper(LibraryDataContext context, IMapper mapper, MapperConfiguration config)
        {
            Context = context;
            Mapper = mapper;
            Config = config;
        }

        public async Task<GetBooksResponse> GetAllBooksFor(string genre)
        {
            var books = Context.Books
                .Where(b => b.InStock)
                // just using the mapper.map caused a problem w/ the filtering by genre
                //.Select(book => Mapper.Map<GetBooksResponseItem>(book));
                
                // this is when we did it manually
                //.Select(b => new GetBooksResponseItem // Book -> GetBooksResponseItem
                //{
                //    Id = b.Id,
                //    Title = b.Title,
                //    Author = b.Author,
                //    Genre = b.Genre,
                //    NumberOfPages = b.NumberOfPages
                //});
                
                // using this, it fixed the filtering by genre, and i'm not sure why
                .ProjectTo<GetBooksResponseItem>(Config);


            if (genre != null)
            {
                books = books.Where(b => b.Genre == genre);
            }

            var booksList = await books.ToListAsync();
            var response = new GetBooksResponse
            {
                Books = booksList,
                GenreFilter = genre,
                NumberOfBooks = booksList.Count
            };

            return response;
        }
    }
}
