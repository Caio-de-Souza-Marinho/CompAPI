using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CompAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ParticipantesController : ControllerBase
    {
       private readonly IConfiguration _config;

       public ParticipantesController(IConfiguration config)
       {
           _config = config;
       }  




    }
}