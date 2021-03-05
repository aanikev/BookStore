using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public Author Author { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public BookStatus BookStatus { get; set; }
        public string LoginUserChangeStatus { get; set; }
        public DateTime CreatedOn { get; set; }

        // Autor, Name, ReleaseDate, BookStatus, LoginUserChangeStatus, CreateDate
    }
}
