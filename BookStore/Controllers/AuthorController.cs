using BookStore.Models;
using BookStore.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        ApplicationContext db;
        public AuthorController(ApplicationContext context)
        {
            db = context;

        }
        // GET: api/<AuthorController>
        [HttpGet]

        // Получение авторов по его имени 
        public IEnumerable<AuthorDTO> Get(string name)
        {
            var author = db.Authors.Where(author =>
                !String.IsNullOrEmpty(name) && author.Name.Contains(name));
                return author.Select(a => new AuthorDTO()
            {
                Name = a.Name,
                Surname = a.Surname,
                LastName = a.LastName,
                FullName = a.FullName,
                DateBirth = a.DateBirth
            });
            
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthorController>
        [HttpPost]
        public void AuthorCreate([FromBody] AuthorDTO authorDTO)
        {
            Author author = new Author
            {
                Name = authorDTO.Name,
                Surname = authorDTO.Surname,
                LastName = authorDTO.LastName,
                FullName = authorDTO.FullName,
                DateBirth = authorDTO.DateBirth
            };

            db.Authors.Add(author);
            db.SaveChanges();

        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public void DeleteAuthor(Guid authorId)
        {
            // метод удаления автора и всех его книг 

            Book deleteBook = db.Books
            .Where(p => p.Author.Id == authorId)
            .FirstOrDefault();
            db.Books.Remove(deleteBook);
            db.SaveChanges();

            Author deleteAuthor = db.Authors
             .Where(c => c.Id == authorId)
             .FirstOrDefault();

            db.Authors.Remove(deleteAuthor);
            db.SaveChanges();

        }
    }
}
