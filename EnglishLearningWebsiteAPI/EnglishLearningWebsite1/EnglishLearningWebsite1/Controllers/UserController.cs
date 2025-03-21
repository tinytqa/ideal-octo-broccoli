using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        DBCProfessionalProj dbc;

        public UserController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Users.ToList());
        }

        string TaoUserID()
        {
            string id = "";
            var cmd = dbc.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "user_getID";
            cmd.Connection.Open();
            id = cmd.ExecuteScalar().ToString();

            //var kq = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Load(kq);
            //id = dt.Rows[0][0].ToString();
            return id;
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddUser(string userId, string username, string password, string fullName, string email, string phoneNumber, string role)
        {
            User user = new User();
            // tao log_id khi ko goi them truc  => ""
            if (userId == "null") userId = TaoUserID();
            user.UserId = userId;
            user.Username = username;
            user.Password = password;
            user.FullName = fullName;
            user.Email = email;
            user.PhoneNumber = phoneNumber;
            user.Role = role;

            dbc.Users.Add(user);
            dbc.SaveChanges();

            return Ok("Insert User ID: " + userId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult UpdateUser(string userId, string username, string password, string fullName, string email, string phoneNumber, string role)
        {
            var user = dbc.Users.Find(userId);
            user.UserId = userId;
            user.Username = username;
            user.Password = password;
            user.FullName = fullName;
            user.Email = email;
            user.PhoneNumber = phoneNumber;
            user.Role = role;

            dbc.Users.Update(user);
            dbc.SaveChanges();

            return Ok("Update User ID " + userId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeleteUser(string userId)
        {
            var user = dbc.Users.Find(userId);

            dbc.Users.Remove(user);
            dbc.SaveChanges();

            return Ok("Delete User ID " + userId + " successfully!");
        }

        [HttpGet]
        [Route("GetById/{userId}")]
        public IActionResult GetUserById(string userId)
        {
            var user = dbc.Users.Find(userId);
            if (user == null)
            {
                return NotFound(new { success = false, message = "User not found" });
            }
            return Ok(user);
        }
    }
}
