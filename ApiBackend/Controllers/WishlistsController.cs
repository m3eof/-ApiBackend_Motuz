using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBackend.Controllers
{
    public class WishlistsModel
    {
        public int WishlistId { get; set; }
        public int UsersId { get; set; }
        public int BookId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController : ControllerBase
    {
        public recensiiContext Context { get; }

        public WishlistsController(recensiiContext context)
        {
            Context = context;
        }


        [HttpGet]
        public IActionResult Getall()
        {
            List<Wishlist> wishlists = Context.Wishlists.ToList();
            return Ok(wishlists);
        }

        [HttpGet("getById/{id:int}")]

        public IActionResult GetById(int id)
        {
            Wishlist? wishlist = Context.Wishlists.Where(x => x.WishlistId == id).FirstOrDefault();
            if (wishlist == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(wishlist);
        }



        [HttpPost]

        public IActionResult Add(WishlistsModel wishlist)
        {
            var wishlistAdd = new Wishlist()
            {

            
             UsersId = wishlist.UsersId,
             BookId = wishlist.BookId,


            };
            Context.Wishlists.Add(wishlistAdd);
            Context.SaveChanges();
            return Ok(wishlistAdd);
        }

        [HttpPut]
        public IActionResult Update(WishlistsModel wishlist)
        {

            var wishlistUpd = new Wishlist()
            {
                WishlistId = wishlist.WishlistId,
                UsersId = wishlist.UsersId,
                BookId = wishlist.BookId,
            };

            Context.Wishlists.Update(wishlistUpd);
            Context.SaveChanges();
            return Ok();
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            Wishlist? wishlist = Context.Wishlists.Where(x => x.WishlistId == id).FirstOrDefault();
            if (wishlist == null)
            {
                return BadRequest("Not Found");
            }
            Context.Wishlists.Remove(wishlist);
            Context.SaveChanges();
            return Ok();
        }

    

    }
}
