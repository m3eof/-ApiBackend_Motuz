using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBackend.Controllers
{
    public class SavedModel
    {
        public int SavedId { get; set; }
        public int ReviewId { get; set; }
        public int UsersId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SavedController : ControllerBase
    {
        public recensiiContext Context { get; }

        public SavedController(recensiiContext context)
        {
            Context = context;
        }


        [HttpGet]
        public IActionResult Getall()
        {
            List<Saved> saveds = Context.Saveds.ToList();
            return Ok(saveds);
        }

        [HttpGet("getById/{id:int}")]

        public IActionResult GetById(int id)
        {
            Saved? saved = Context.Saveds.Where(x => x.SavedId == id).FirstOrDefault();
            if (saved == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(saved);
        }



        [HttpPost]

        public IActionResult Add(SavedModel saved)
        {
            var savedAdd = new Saved()
            {
                SavedId = saved.SavedId,
                ReviewId = saved.ReviewId,
                UsersId = saved.UsersId,
               
            };
            Context.Saveds.Add(savedAdd);
            Context.SaveChanges();
            return Ok(savedAdd);
        }

        [HttpPut]
        public IActionResult Update(SavedModel saved)
        {

            var savedUpd = new Saved()
            {
                SavedId = saved.SavedId,
                ReviewId = saved.ReviewId,
                UsersId = saved.UsersId,
            };

            Context.Saveds.Update(savedUpd);
            Context.SaveChanges();
            return Ok();
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            Saved? saved = Context.Saveds.Where(x => x.SavedId == id).FirstOrDefault();
            if (saved == null)
            {
                return BadRequest("Not Found");
            }
            Context.Saveds.Remove(saved);
            Context.SaveChanges();
            return Ok();
        }

    }
}
