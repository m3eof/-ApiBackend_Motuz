using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBackend.Controllers
{
    public class PreferencesModel
    {
        public int PrefId { get; set; }
        public int UsersId { get; set; }
        public int GenreId { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PreferencesController : ControllerBase
    {

        public recensiiContext Context { get; }

        public PreferencesController(recensiiContext context)
        {
            Context = context;
        }


        [HttpGet]
        public IActionResult Getall()
        {
            List<Preference> preferences = Context.Preferences.ToList();
            return Ok(preferences);
        }

        [HttpGet("getById/{id:int}")]

        public IActionResult GetById(int id)
        {
            Preference? preference = Context.Preferences.Where(x => x.PrefId == id).FirstOrDefault();
            if (preference == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(preference);
        }

      

        [HttpPost]

        public IActionResult Add(PreferencesModel preference)
        {
            var preferenceAdd = new Preference()
            {
               UsersId = preference.UsersId,
               GenreId = preference.GenreId,
            };
            Context.Preferences.Add(preferenceAdd);
            Context.SaveChanges();
            return Ok(preferenceAdd);
        }

        [HttpPut]
        public IActionResult Update(PreferencesModel preference)
        {

            var preferenceUpd = new Preference()
            {
               PrefId = preference.PrefId,
                UsersId = preference.UsersId,
                GenreId = preference.GenreId,
            };

            Context.Preferences.Update(preferenceUpd);
            Context.SaveChanges();
            return Ok();
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            Preference? preference = Context.Preferences.Where(x => x.PrefId == id).FirstOrDefault();
            if (preference == null)
            {
                return BadRequest("Not Found");
            }
            Context.Preferences.Remove(preference);
            Context.SaveChanges();
            return Ok();
        }
    }
}
