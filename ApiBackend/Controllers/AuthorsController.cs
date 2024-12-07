using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBackend.Controllers
{
    public class AuthorsModel
    {
        public int AuthorId { get; set; }
        public string FullName { get; set; }
        public string? Bio { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        public recensiiContext Context { get; }

        public AuthorsController(recensiiContext context)
        {
            Context = context;
        }


        [HttpGet]
        public IActionResult Getall()
        {
            List<Author> authors = Context.Authors.ToList();
            return Ok(authors);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            Author? author = Context.Authors.Where(x => x.AuthorId == id).FirstOrDefault();
            if (author == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(author);
        }

        [HttpGet("getUserByUsername/FullName")]
        public IActionResult GetUserByUsername(string fullName)
        {

            var author = Context.Authors.FirstOrDefault(x => x.FullName == fullName);


            if (author == null)
            {
                return NotFound("Автор не найден");
            }


            return Ok(author);
        }

        [HttpPost]

        public IActionResult Add(AuthorsModel author)
        {

            bool AuthorExists = Context.Authors.Any(a => a.FullName.ToLower() == author.FullName.ToLower());

            if (AuthorExists)
            {
                return BadRequest("Такой автор уже существует!");
            }

            var authorAdd = new Author()
            {

                FullName = author.FullName,
                Bio = author.Bio,
            };
            Context.Authors.Add(authorAdd);
            Context.SaveChanges();
            return Ok(authorAdd);
        }

        [HttpPut]
        public IActionResult Update(AuthorsModel author)
        {
            bool AuthorExists = Context.Authors.Any(a => a.FullName.ToLower() == author.FullName.ToLower());

            if (AuthorExists)
            {
                return BadRequest("Такой автор уже существует!");
            }

            var authorUpd = new Author()
            {
                AuthorId = author.AuthorId,
                FullName = author.FullName,
                Bio = author.Bio,
            };

            
            if (author.AuthorId < 0)
            {
                return BadRequest("ID не может быть минусовым :(");
            }
            
            // мб проверка на несуществующие айдишники хз
          
            Context.Authors.Update(authorUpd);
            Context.SaveChanges();
            return Ok();
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            Author? author = Context.Authors.Where(x => x.AuthorId == id).FirstOrDefault();
            if (author == null)
            {
                return BadRequest("Not Found");
            }
            Context.Authors.Remove(author);
            Context.SaveChanges();
            return Ok();
        }

    }
}
