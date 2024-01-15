using CsvReaderAPI.Services.Implementation;
using CsvReaderAPI.Services.Interfaces;
using CsvReaderAPI.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CsvReader.API.Controllers

{
    [Route("api/[controller]")]

    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountService _accountService;
        public AccountController(IAuthenticationService authenticationService, IAccountService accountService)
        {
            _authenticationService = authenticationService;
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        [Route("getAll")]
        public List<UserViewModel> GetAllAccounts()
        {
            return _accountService.GetAllAccount();
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data properties");
            }
            return StatusCode(_authenticationService.Register(model));
        }
        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            return Json(_authenticationService.Login(email, password));
        }
        [HttpPost("deleteAccount")]
        public IActionResult DeleteAccount(string deleteAccountId,string currentAccontId)
        {
            var res = _accountService.DeleteAccount(deleteAccountId, currentAccontId);
            return StatusCode(res);
        }

    }
}
