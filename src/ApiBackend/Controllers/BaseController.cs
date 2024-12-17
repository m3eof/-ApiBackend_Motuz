using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiBackend.Models;

namespace ApiBackend.Controllers
{
    
    [ApiController]
    public class BaseController : ControllerBase
    {
        public User User => (User)HttpContext.Items["User"];

    }
}
