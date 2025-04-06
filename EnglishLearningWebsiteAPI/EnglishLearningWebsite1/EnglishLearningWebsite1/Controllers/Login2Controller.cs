using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnglishLearningWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login2Controller : ControllerBase
    {
        private readonly DBCProfessionalProj _context;
        IConfiguration _cfg;
        DBCProfessionalProj dbc;
        public Login2Controller(IConfiguration configuration, DBCProfessionalProj dbc1)
        {
            _cfg = configuration;
            dbc = dbc1;
        }


        private string GetKey(string username, string password, string role, string id)
        {
            var jwtHandle = new JwtSecurityTokenHandler();
            var key = _cfg["jwtSetting:key"]; // Lấy key từ cấu hình
            var keybytes = Encoding.UTF8.GetBytes(key);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, username),
            new Claim("tokenid", Guid.NewGuid().ToString()),
            new Claim("permission", role),
            new Claim(ClaimTypes.NameIdentifier, id)
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keybytes),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = jwtHandle.CreateToken(tokenDescription);
            return jwtHandle.WriteToken(token);
        }
        //[HttpGet]
        //[Route("login")]
        //public IActionResult Login([FromQuery] string username, [FromQuery] string password)
        //{
        //    // Tìm người dùng trong bảng User
        //    User user = dbc.Users.FirstOrDefault(p => p.Username == username && p.Password == password);

        //    if (user == null)
        //    {
        //        return BadRequest("Wrong username or password");
        //    }

        //    // Nếu tìm thấy người dùng, tạo JWT token với role
        //    string token = GetKey(username, password, user.Role, user.UserId);

        //    // Trả về kết quả login với JWT token và thông tin người dùng
        //    return Ok(new
        //    {
        //        code = 100,
        //        msg = "Login Successfully!",
        //        token = token,
        //        user = new
        //        {
        //            user.UserId,
        //            user.Username,
        //            user.Role  // Bao gồm thông tin role ở đây
        //        }
        //    });
        //}

        [HttpGet]
        [Route("login")]
        public IActionResult Login([FromQuery] string username, [FromQuery] string password)
        {
            // Tìm người dùng trong bảng User
            User user = dbc.Users.FirstOrDefault(p => p.Username == username && p.Password == password);

            if (user == null)
            {
                return BadRequest("Wrong username or password");
            }

            // Kiểm tra nếu người dùng là giảng viên, lấy thông tin giảng viên
            Instructor instructor = null;
            if (user.Role == "Instructor")
            {
                instructor = dbc.Instructors.FirstOrDefault(i => i.UserId == user.UserId);
            }

            // Tạo JWT token với role và trả về thông tin người dùng
            string token = GetKey(username, password, user.Role, user.UserId);

            return Ok(new
            {
                code = 100,
                msg = "Login Successfully!",
                token = token,
                userFound = new
                {
                    user.UserId,
                    user.Username,
                    user.FullName,
                    user.Email,
                    user.PhoneNumber,
                    user.Role,
                    InstructorId = instructor?.InstructorId,  // Trả về InstructorId nếu người dùng là giảng viên
                    instructor?.Bio,
                    instructor?.Expertise
                }
            });
        }


    }
}