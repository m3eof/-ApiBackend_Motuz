using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiBackend.Authorization;
using AllowAnonymousAttribute = ApiBackend.Authorization.AllowAnonymousAttribute;
using Microsoft.AspNetCore.Authorization;

namespace ApiBackend.Controllers
{

    public class CommentsModel
    {
        public int CommentId { get; set; }
        public int ReviewId { get; set; }
        public int UsersId { get; set; }
        public string TextComment { get; set; } = null!;
    }
    [Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        public recensiiContext Context { get; }

        public CommentsController(recensiiContext context)
        {
            Context = context;
        }

        [Authorization.Authorize]
        [HttpGet]
        public IActionResult Getall()
        {
            List<Comment> comments = Context.Comments.ToList();
            return Ok(comments);
        }
        [Authorization.Authorize]
        [HttpGet("getById/{id:int}")]

        public IActionResult GetById(int id)
        {
            Comment? comment = Context.Comments.Where(x => x.CommentId == id).FirstOrDefault();
            if (comment == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(comment);
        }
        [Authorization.Authorize]
        [HttpGet("getUserByUsername/Username")]
        public IActionResult GetUserByUsername(string username)
        {
            Comment? comment = Context.Comments.Where(x => x.Users.Username == username).FirstOrDefault();

            if (comment == null)
            {
                return NotFound("User not found");
            }

            return Ok(comment);
        }
        [Authorization.Authorize]
        [HttpPost]

        public IActionResult Add(CommentsModel comment)
        {

            var commentAdd = new Comment()
            {
              ReviewId = comment.ReviewId,
              UsersId = comment.UsersId,
              TextComment = comment.TextComment,

            };
            Context.Comments.Add(commentAdd);
            Context.SaveChanges();
            return Ok(commentAdd);
        }
        [Authorization.Authorize]
        [HttpPut]
        public IActionResult Update(CommentsModel comment)
        {

            var commentUpd = new Comment()
            {
                UsersId = comment.UsersId,
                ReviewId = comment.ReviewId,
                TextComment = comment.TextComment,
            };


            Context.Comments.Update(commentUpd);
            Context.SaveChanges();
            return Ok();
        }
        [Authorization.Authorize]
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            Comment? comment = Context.Comments.Where(x => x.CommentId == id).FirstOrDefault();
            if (comment == null)
            {
                return BadRequest("Not Found");
            }
            Context.Comments.Remove(comment);
            Context.SaveChanges();
            return Ok();
        }

    }

}

