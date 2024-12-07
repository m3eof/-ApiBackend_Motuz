using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        public recensiiContext Context { get; }

        public UsersController(recensiiContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult Getall()
        {
            List<User> users = Context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("getById/{id:int}")]

        public IActionResult GetById(int id)
        {
            User? user = Context.Users.Where(x => x.UsersId == id).FirstOrDefault();
            if (user == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(user);
        }

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


            // мб проверка на несуществующие айдишники хз

            Context.Users.Update(userUpd);
            Context.SaveChanges();
            return Ok();
        }

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



    
