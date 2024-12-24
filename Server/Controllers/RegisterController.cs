using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Server.Services.Register;

namespace Server.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterService _registeredservices;
        public RegisterController(IRegisterService registeredservices)
        {
            _registeredservices = registeredservices;
        }
        [HttpPost]
        public ActionResult addUser([FromBody] Models.User user) 
        { 
            _registeredservices.AddUser(user);
           return Ok(user);
        }
    }
}
