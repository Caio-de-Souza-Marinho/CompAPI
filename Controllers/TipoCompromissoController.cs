using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CompAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    public class TipoCompromissoController : Controller
    {
        private readonly IConfiguration _config;

        public TipoCompromissoController(IConfiguration config)
        {
            _config = config;
        }
    }
}