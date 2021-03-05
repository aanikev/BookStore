using BookStore.Models;
using BookStore.Models.DTO;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        ApplicationContext db;
        Logger logger;

        public BookController(ApplicationContext context, Logger logger)
        {
            db = context;
            this.logger = logger;
        }     

        // Получение книг по названию 
        // Получение списка всех книг с полным именем автора
        // Поиск книг по названию или Имени, или Фамилии или Отчеству автора 
        [HttpGet]
        public IEnumerable<BookDTO> Get(string name, string firstName, string surName, string lastName)
        {
            IEnumerable<Book> books;

            if (!String.IsNullOrEmpty(name) ||
                !String.IsNullOrEmpty(firstName) ||
                !String.IsNullOrEmpty(surName)  ||
                !String.IsNullOrEmpty(lastName) )              
            {
                books = db.Books.Where(book =>
                    (!String.IsNullOrEmpty(name) && book.Name.Contains(name)) ||
                    (!String.IsNullOrEmpty(firstName) && book.Author.Name == firstName) ||
                    (!String.IsNullOrEmpty(surName) && book.Author.Surname == surName) ||
                    (!String.IsNullOrEmpty(lastName) && book.Author.LastName == lastName))
                    .Include(b => b.Author).Include(b => b.BookStatus);
            }
            else
            {
                books = db.Books.Include(b => b.Author).Include(b => b.BookStatus);
            }
            return books.Select(b => Mapper.Converter(b));
        }


       // POST api/<BookController>
       [HttpPost]
        public string BookCreate([FromBody] BookDTO bookDTO)
        {
            DefaultResponce resp = new DefaultResponce();
            var author = db.Authors.Where(a => (a.Id == bookDTO.AuthorId)).FirstOrDefault();
            if (author == null)
            {
                logger.Error("Author не существует");
                resp = new DefaultResponce(false, "Author не существует");
                return resp.ToString();
            }

            var bookStatus = db.BookStatuses.Where(a => (a.Id == bookDTO.BookStatusId)).FirstOrDefault();
            if (bookStatus == null)
            {
                logger.Error("BookStatus не существует");
                resp = new DefaultResponce(false, "BookStatus не существует");
                return resp.ToString();
            }
            Book book = new Book
             {
                 Id = bookDTO.Id,
                 Author = author,
                 Name = bookDTO.Name,
                 ReleaseDate = bookDTO.ReleaseDate,

                 BookStatus = bookStatus
            };
           
            db.Books.Add(book);
            db.SaveChanges();
            return resp.ToString();
        }

       
        // PUT api/<BookController>/5
        [HttpPut]
        // Метод изменения любого статуса кнмги на "в наличии" по идентификатору
        // Метод изменения статуса книги с "в наличии" на "продана" по идентификатору книги
        public string  ChangeAnyStatus(Guid Id, string login)
        {
            DefaultResponce resp = new DefaultResponce();
            Book status = db.Books.Where(p => (p.Id == Id)).Include(s=>s.BookStatus)
            .FirstOrDefault();
            if (status == null)
            {
                logger.Error("Книга не найдена");
                resp = new DefaultResponce(false, "Книга не найдена");
                return resp.ToString();
            }
            else if(status.BookStatus.Name == "в наличии")
            {
                status.BookStatus.Name = "продана";
                status.LoginUserChangeStatus = login;
                db.SaveChanges();
                logger.Info("Успешно");
                return new DefaultResponce().ToString();
            }
            else 
            {
                status.BookStatus.Name = "в наличии";
                status.LoginUserChangeStatus = login;
                db.SaveChanges();
                logger.Info("Успешно");
                return new DefaultResponce().ToString();
            }
        }

        [HttpDelete]
        public string DeleteBook(Guid bookId)
        {
            DefaultResponce resp = new DefaultResponce();
            Book deleteBook = db.Books.Where(p => p.Id == bookId).FirstOrDefault();
            if (deleteBook == null)
            {
                logger.Error("Книга не найдена");
                resp = new DefaultResponce(false, "Книга не найдена");
                return resp.ToString();
            }
            else
            {
                db.Books.Remove(deleteBook);
                db.SaveChanges();
                logger.Info("Успешно");
                return new DefaultResponce().ToString();
            }
        }
    }
}
