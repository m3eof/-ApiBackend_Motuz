using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiBackend.Authorization;
using AllowAnonymousAttribute = ApiBackend.Authorization.AllowAnonymousAttribute;
using Microsoft.AspNetCore.Authorization;

namespace ApiBackend.Controllers
{
    public class GenresModel
    {
        public int GenreId { get; set; }
        public string NameOfGenre { get; set; } = null!;
    }
    [Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        public recensiiContext Context { get; }

        public GenresController(recensiiContext context)
        {
            Context = context;
        }
        [Authorization.Authorize]
        [HttpGet]
        public IActionResult Getall()
        {
            List<Genre> genres = Context.Genres.ToList();
            return Ok(genres);
        }
        [Authorization.Authorize]
        [HttpGet("getById/{id:int}")]

        public IActionResult GetById(int id)
        {
            Genre? genre = Context.Genres.Where(x => x.GenreId == id).FirstOrDefault();
            if (genre == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(genre);
        }
        [Authorization.Authorize]
        [HttpPost]

        public IActionResult Add(GenresModel genre)
        {
            var genreAdd = new Genre()
            {
                NameOfGenre = genre.NameOfGenre,
            };
            Context.Genres.Add(genreAdd);
            Context.SaveChanges();
            return Ok(genreAdd);
        }
        [Authorization.Authorize]
        [HttpPut]
        public IActionResult Update(GenresModel genre)
        {

            var genreUpd = new Genre()
            {
              
                NameOfGenre = genre.NameOfGenre,
            };

            Context.Genres.Update(genreUpd);
            Context.SaveChanges();
            return Ok();
        }
        [Authorization.Authorize]
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            Genre? genre = Context.Genres.Where(x => x.GenreId == id).FirstOrDefault();
            if (genre == null)
            {
                return BadRequest("Not Found");
            }
            Context.Genres.Remove(genre);
            Context.SaveChanges();
            return Ok();
        }
    }
}
