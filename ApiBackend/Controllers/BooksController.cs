using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBackend.Controllers
{
    public class BooksModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int PublishedYear { get; set; }
        public int GenreId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        public recensiiContext Context { get; }

        public BooksController(recensiiContext context)
        {
            Context = context;
        }


        [HttpGet]
        public IActionResult Getall()
        {
            List<Book> books = Context.Books.ToList();
            return Ok(books);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            Book? book = Context.Books.Where(x => x.BookId == id).FirstOrDefault();
            if (book == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(book);
        }

        [HttpGet("getUserByUsername/Title")]
        public IActionResult GetUserByUsername(string title)
        {

            var book = Context.Books.FirstOrDefault(x => x.Title == title);


            if (book == null)
            {
                return NotFound("Автор не найден");
            }


            return Ok(book);
        }

        [HttpPost]

        public IActionResult Add(BooksModel book)
        {

            bool BookExists = Context.Books.Any(a => a.Title.ToLower() == book.Title.ToLower());

            if (BookExists)
            {
                return BadRequest("Такая книга уже существует!");
            }

            var bookAdd = new Book()
            {
                Title = book.Title,
                AuthorId = book.AuthorId,
                PublishedYear = book.PublishedYear,
                GenreId = book.GenreId,
               
            };
            Context.Books.Add(bookAdd);
            Context.SaveChanges();
            return Ok(bookAdd);
        }

        [HttpPut]
        public IActionResult Update(BooksModel book)
        {
            bool BookExists = Context.Books.Any(a => a.Title.ToLower() == book.Title.ToLower());

            if (BookExists)
            {
                return BadRequest("Такая книга уже существует!");
            }

            var bookUpd = new Book()
            {
                Title = book.Title,
                AuthorId = book.AuthorId,
                PublishedYear = book.PublishedYear,
                GenreId = book.GenreId,
            };


            if (book.BookId < 0)
            {
                return BadRequest("ID не может быть минусовым :(");
            }

            // мб проверка на несуществующие айдишники хз

            Context.Books.Update(bookUpd);
            Context.SaveChanges();
            return Ok();
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            Book? book = Context.Books.Where(x => x.BookId == id).FirstOrDefault();
            if (book == null)
            {
                return BadRequest("Not Found");
            }
            Context.Books.Remove(book);
            Context.SaveChanges();
            return Ok();
        }

    }
}



