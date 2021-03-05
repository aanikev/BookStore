using BookStore.Models;
using BookStore.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public static class Mapper
    {
        public static BookDTO Converter(Book book) 
        {

            BookDTO bookDTO = new BookDTO()
            {
                Id = book.Id,
                AuthorId = book.Author.Id,
                Name = book.Name,
                ReleaseDate = book.ReleaseDate,
                BookStatusId = book.BookStatus.Id 
            };

         return bookDTO;     
        }
    }
}
