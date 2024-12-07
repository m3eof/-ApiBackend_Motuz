using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBackend.Controllers
{
    public class ReviewsModel
    {
        public int ReviewId { get; set; }
        public int UsersId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        public recensiiContext Context { get; }

        public ReviewsController(recensiiContext context)
        {
            Context = context;
        }


        [HttpGet]
        public IActionResult Getall()
        {
            List<Review> reviews = Context.Reviews.ToList();
            return Ok(reviews);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            Review? review = Context.Reviews.Where(x => x.ReviewId == id).FirstOrDefault();
            if (review == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(review);
        }

        [HttpPost]

        public IActionResult Add(ReviewsModel review)
        {

            var reviewAdd = new Review()
            {
               UsersId = review.UsersId,
               BookId = review.BookId,
               Title = review.Title,
               Rating = review.Rating,
               ReviewText = review.ReviewText,

            };
            Context.Reviews.Add(reviewAdd);
            Context.SaveChanges();
            return Ok(reviewAdd);
        }

        [HttpPut]
        public IActionResult Update(ReviewsModel review)
        {

            var reviewUpd = new Review()
            {
                UsersId = review.UsersId,
                BookId = review.BookId,
                Title = review.Title,
                Rating = review.Rating,
                ReviewText = review.ReviewText,
            };


            Context.Reviews.Update(reviewUpd);
            Context.SaveChanges();
            return Ok();
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            Review? review = Context.Reviews.Where(x => x.ReviewId == id).FirstOrDefault();
            if (review == null)
            {
                return BadRequest("Not Found");
            }
            Context.Reviews.Remove(review);
            Context.SaveChanges();
            return Ok();
        }

    }

}

