using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiBackend.Authorization;
using AllowAnonymousAttribute = ApiBackend.Authorization.AllowAnonymousAttribute;
using Microsoft.AspNetCore.Authorization;

namespace ApiBackend.Controllers
{
    public class ExchangesModel
    {
        public int ExchangeId { get; set; }
        public int BookId { get; set; }
        public int OwnerId { get; set; }
        public int SeekerId { get; set; }
        public string StatusOfExchange { get; set; } = null!;
    }

    [Authorization.Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangesController : ControllerBase
    {
        public recensiiContext Context { get; }

        public ExchangesController(recensiiContext context)
        {
            Context = context;
        }
        [Authorization.Authorize]
        [HttpGet]
        public IActionResult Getall()
        {
            List<Exchange> exchanges = Context.Exchanges.ToList();
            return Ok(exchanges);
        }
        [Authorization.Authorize]
        [HttpGet("getById/{id:int}")]

        public IActionResult GetById(int id)
        {
            Exchange? exchange = Context.Exchanges.Where(x => x.ExchangeId == id).FirstOrDefault();
            if (exchange == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(exchange);
        }
        [Authorization.Authorize]
        [HttpPost]

        public IActionResult Add(ExchangesModel exchange)
        {
            var exchangeAdd = new Exchange()
            {
                BookId = exchange.BookId,
                OwnerId = exchange.OwnerId,
                SeekerId = exchange.SeekerId,
                StatusOfExchange = exchange.StatusOfExchange,
            };
            Context.Exchanges.Add(exchangeAdd);
            Context.SaveChanges();
            return Ok(exchangeAdd);
        }
        [Authorization.Authorize]
        [HttpPut]
        public IActionResult Update(ExchangesModel exchange)
        {

            var exchangeUpd = new Exchange()
            {
               BookId = exchange.BookId,
               OwnerId = exchange.OwnerId,
               SeekerId = exchange.SeekerId,
               StatusOfExchange = exchange.StatusOfExchange
            };




            Context.Exchanges.Update(exchangeUpd);
            Context.SaveChanges();
            return Ok();
        }

        [Authorization.Authorize]
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            Exchange? exchange = Context.Exchanges.Where(x => x.ExchangeId == id).FirstOrDefault();
            if (exchange == null)
            {
                return BadRequest("Not Found");
            }
            Context.Exchanges.Remove(exchange);
            Context.SaveChanges();
            return Ok();
        }

    }
}
