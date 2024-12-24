using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Server.Models;



namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly ChineseAuctionContext _context;
        public AuthController(JwtTokenService jwtTokenService, ChineseAuctionContext context)
        {
            _jwtTokenService = jwtTokenService;
            _context = context;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.Where(x=>x.UserName == request.Username).FirstOrDefault();
            var b = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!b)
            {
                return NotFound("הסיסמא לא נכונה");
            }
            if(user == null)
            {
                return NotFound("משתמש לא נמצא הירשם");
            }
            if (user.Role == "Admin")
            {
                var roles = new List<string> { "Admin" };
                // Generate JWT Token
                var token = _jwtTokenService.GenerateJwtToken(request.Username, roles);
                return Ok(new { Token = token });
            }
            else if(user.Role == "User")
            {
                var roles = new List<string> { "User" };
                // Generate JWT Token
                var token = _jwtTokenService.GenerateJwtToken(request.Username, roles);

                return Ok(new { Token = token });
            }
            return Unauthorized("Invalid credentials");
        }
    }
}
