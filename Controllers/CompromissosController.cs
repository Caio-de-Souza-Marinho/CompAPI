using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CompAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class CompromissosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CompromissosController(IConfiguration config)
        {
            _config = config;
        }
    }
}