using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiBackend.Entities;
using ApiBackend.Authorization;

namespace ApiBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
    }
}
