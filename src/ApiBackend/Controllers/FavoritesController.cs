using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBackend.Controllers
{
    public class FavoritesModel
    {
        public int FavId { get; set; }
        public int UsersId { get; set; }
        public int BookId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        public recensiiContext Context { get; }

        public FavoritesController(recensiiContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult Getall()
        {
            List<Favorite> favorites = Context.Favorites.ToList();
            return Ok(favorites);
        }

        [HttpGet("getById/{id:int}")]

        public IActionResult GetById(int id)
        {
            Favorite? favorite = Context.Favorites.Where(x => x.FavId == id).FirstOrDefault();
            if (favorite == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(favorite);
        }

        [HttpPost]

        public IActionResult Add(FavoritesModel favorite)
        {
            var favoriteAdd = new Favorite()
            {
               FavId = favorite.FavId,
               UsersId = favorite.UsersId,
               BookId = favorite.BookId,
            };
            Context.Favorites.Add(favoriteAdd);
            Context.SaveChanges();
            return Ok(favoriteAdd);
        }

        [HttpPut]
        public IActionResult Update(FavoritesModel favorite)
        {

            var favoriteUpd = new Favorite()
            {
                FavId = favorite.FavId,
                UsersId = favorite.UsersId,
                BookId = favorite.BookId,
            };

            Context.Favorites.Update(favoriteUpd);
            Context.SaveChanges();
            return Ok();
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            Favorite? favorite = Context.Favorites.Where(x => x.FavId == id).FirstOrDefault();
            if (favorite == null)
            {
                return BadRequest("Not Found");
            }
            Context.Favorites.Remove(favorite);
            Context.SaveChanges();
            return Ok();
        }

    }
}
