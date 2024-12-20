using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiBackend.Authorization;
using AllowAnonymousAttribute = ApiBackend.Authorization.AllowAnonymousAttribute;
using Microsoft.AspNetCore.Authorization;

namespace ApiBackend.Controllers
{
    public class WishlistsModel
    {
        public int WishlistId { get; set; }
        public int UsersId { get; set; }
        public int BookId { get; set; }
    }
    [Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController : ControllerBase
    {
        public recensiiContext Context { get; }

        public WishlistsController(recensiiContext context)
        {
            Context = context;
        }

        [Authorization.Authorize]
        [HttpGet]
        public IActionResult Getall()
        {
            List<Wishlist> wishlists = Context.Wishlists.ToList();
            return Ok(wishlists);
        }
        [Authorization.Authorize]
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


        [Authorization.Authorize]
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
        [Authorization.Authorize]
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
        [Authorization.Authorize]
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
