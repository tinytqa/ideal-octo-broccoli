using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentBuyCourseController : ControllerBase
    {
        DBCProfessionalProj dbc;

        public StudentBuyCourseController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.StudentBuyCourses.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddStudentBuyCourse(string enrollmentId, string studentId, string courseId, DateTime enrollmentDate, string status)
        {
            StudentBuyCourse studentBuyCourse = new StudentBuyCourse();
            studentBuyCourse.EnrollmentId = enrollmentId;
            studentBuyCourse.StudentId = studentId;
            studentBuyCourse.CourseId = courseId;
            studentBuyCourse.EnrollmentDate = enrollmentDate;
            studentBuyCourse.Status = status;

            dbc.StudentBuyCourses.Add(studentBuyCourse);
            dbc.SaveChanges();

            return Ok("Insert StudentBuyCourse ID: " + enrollmentId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]
        
        public IActionResult UpdateStudentBuyCourse(string enrollmentId, string studentId, string courseId, DateTime enrollmentDate, string status)
        {
            var studentBuyCourse = dbc.StudentBuyCourses.Find(enrollmentId);
            studentBuyCourse.EnrollmentId = enrollmentId;
            studentBuyCourse.StudentId = studentId;
            studentBuyCourse.CourseId = courseId;
            studentBuyCourse.EnrollmentDate = enrollmentDate;
            studentBuyCourse.Status = status;

            dbc.StudentBuyCourses.Update(studentBuyCourse);
            dbc.SaveChanges();

            return Ok("Update StudentBuyCourse ID " + enrollmentId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteStudentBuyCourse(string enrollmentId)
        {
            var studentBuyCourse = dbc.StudentBuyCourses.Find(enrollmentId);

            dbc.StudentBuyCourses.Remove(studentBuyCourse);
            dbc.SaveChanges();

            return Ok("Delete StudentBuyCourse ID " + studentBuyCourse + " successfully!");
        }
    }
}
