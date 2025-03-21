using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        DBCProfessionalProj dbc;

        public StudentController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Students.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddStudent(string studentId, string userId, DateTime dob, string address)
        {
            Student student = new Student();
            student.StudentId = studentId;
            student.UserId = userId;
            student.Dob = dob;
            student.Address = address;

            dbc.Students.Add(student);
            dbc.SaveChanges();

            return Ok("Insert Student ID: " + studentId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult UpdateStudent(string studentId, string userId, DateTime dob, string address)
        {
            var student = dbc.Students.Find(studentId);
            student.StudentId = studentId;
            student.UserId = userId;
            student.Dob = dob;
            student.Address = address;

            dbc.Students.Update(student);
            dbc.SaveChanges();

            return Ok("Update Student ID " + studentId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteStudent(string studentId)
        {
            var student = dbc.Students.Find(studentId);

            dbc.Students.Remove(student);
            dbc.SaveChanges();

            return Ok("Delete Student ID " + studentId + " successfully!");
        }
    }
}
