using ApiBackend.Models;
using Microsoft.AspNetCore.Mvc;
using ApiBackend.Entities;
using ApiBackend.Authorization;
using AllowAnonymousAttribute = ApiBackend.Authorization.AllowAnonymousAttribute;
using Microsoft.AspNetCore.Authorization;

namespace ApiBackend.Controllers
{

    public class UsersModel
    {
        public int UsersId { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; }
        public string UserPassword { get; set; } 
        public int? FollowerNumber { get; set; }
        public int? FollowingNumber { get; set; }
    }
    [Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : BaseController
    {
        public recensiiContext Context { get; set; }

        public UsersController(recensiiContext context)
        {
            Context = context;
        }




        [Authorization.Authorize]
        [HttpGet]
        public IActionResult Getall()
        {
            List<User> users = Context.Users.ToList();
            return Ok(users);

        }


        [Authorization.Authorize]
        [HttpGet("getById/{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id != User.UsersId && User.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            User? user = Context.Users.FirstOrDefault(x => x.UsersId == id);
            if (user == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("getUserByUsername/{username}")]
        public IActionResult GetUserByUsername(string username)
        {


            var user = Context.Users.FirstOrDefault(x => x.Username == username);


            if (user == null)
            {
                return NotFound("User not found");
            }


            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]

        public IActionResult Add(UsersModel user)
        {

            bool UserExists = Context.Users.Any(a => a.Username.ToLower() == user.Username.ToLower());

            if (UserExists)
            {
                return BadRequest("Ник уже занят!");
            }

            bool gmailExists = Context.Users.Any(a => a.Email.ToLower() == user.Email.ToLower());
            if (gmailExists)
            {
                return BadRequest("На данную почту уже зарегестрирован аккаунт!");
            }

            var userAdd = new User()
            {
                Username = user.Username,
                Email = user.Email,
                UserPassword = user.UserPassword
            };
            Context.Users.Add(userAdd);
            Context.SaveChanges();
            return Ok(userAdd);
        }

        [Authorization.Authorize]
        [HttpPut]
        public IActionResult Update(UsersModel user)
        {
            bool UserExists = Context.Users.Any(a => a.Username.ToLower() == user.Username.ToLower());

            if (UserExists)
            {
                return BadRequest("Юзернейм уже занят другим пользователем");
            }


            bool gmailExists = Context.Users.Any(a => a.Email.ToLower() == user.Email.ToLower());

            if (gmailExists)
            {
                return BadRequest("На данную почту уже зарегестрирован другой аккаунт");
            }

            var userUpd = new User()
            {
                Username = user.Username,
                Email = user.Email,
                UserPassword = user.UserPassword
            };


            Context.Users.Update(userUpd);
            Context.SaveChanges();
            return Ok();
        }

        [Authorization.Authorize]
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            User? user = Context.Users.Where(x => x.UsersId == id).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Not Found");
            }
            Context.Users.Remove(user);
            Context.SaveChanges();
            return Ok();
        }

    }
}



    
