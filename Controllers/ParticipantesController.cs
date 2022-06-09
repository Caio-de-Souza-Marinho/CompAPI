using Microsoft.AspNetCore.Mvc

namespace CompAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ParticipantesController : ControllerBase
    {
        private readonly IConfiguration _config;    
    }
}