using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DBCProfessionalProj _context;

        public LoginController(DBCProfessionalProj context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromQuery] string username, [FromQuery] string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Username và password không được để trống" });
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                return Unauthorized(new { message = "Thông tin đăng nhập không hợp lệ" });
            }

            return Ok(new
            {
                message = "Đăng nhập thành công",
                userFound = user
            });
        }
    }
}
