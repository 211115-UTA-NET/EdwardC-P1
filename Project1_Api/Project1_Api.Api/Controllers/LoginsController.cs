using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1_Api.DataStorage;

namespace Project1_Api.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ILoginRepo _loginRepo;

        public LoginsController(ILoginRepo loginRepo)
        {
            _loginRepo = loginRepo;
        }

        [HttpGet]
        public async Task<List<string>> FindUsernamePassword([FromQuery] string Username, string Password)
        {
            List<string> result = await _loginRepo.checkUsernamePassword(Username, Password);

            return result;
        }
    }
}
