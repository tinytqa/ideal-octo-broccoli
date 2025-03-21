using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        DBCProfessionalProj dbc;
        public CourseController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Courses.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddCourse(String courseId, String courseName, String description, Decimal price, string instructorId)
        {
            Course course = new Course();
            course.CourseId = courseId;
            course.CourseName = courseName;
            course.Description = description;
            course.Price = price;
            course.InstructorId = instructorId;

            dbc.Courses.Add(course);
            dbc.SaveChanges();

            return Ok("Insert Course ID: " + courseId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult UpdateCourse(String courseId, String courseName, String description, Decimal price, string instructorId)
        {
            Course course = new Course();
            course.CourseId = courseId;
            course.CourseName = courseName;
            course.Description = description;
            course.Price = price;
            course.InstructorId = instructorId;

            dbc.Courses.Update(course);
            dbc.SaveChanges();

            return Ok("Update Course ID: " + courseId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteCourse(string courseId)
        {
            var course = dbc.Courses.Find(courseId);

            dbc.Courses.Remove(course);
            dbc.SaveChanges();

            return Ok("Delete Course ID " + courseId + " successfully!");
        }

        [HttpGet]
        [Route("GetCourseById/{courseId}")]
        public IActionResult GetCourseById(string courseId)
        {
            var course = dbc.Courses.Find(courseId);
            if (course == null)
            {
                return NotFound("Course not found with ID: " + courseId);
            }
            return Ok(course);
        }

    }
}
