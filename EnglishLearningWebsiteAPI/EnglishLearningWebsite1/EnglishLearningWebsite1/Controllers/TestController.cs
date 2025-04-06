using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        DBCProfessionalProj dbc;

        public TestController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Tests.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddTest(string testId, string courseId, string field, string testContent, string type)
        {
            Test test = new Test();
            test.TestId = testId;
            test.CourseId = courseId;
            test.Field = field;
            test.TestContent = testContent;
            test.Type = type;

            dbc.Tests.Add(test);
            dbc.SaveChanges();

            return Ok("Insert Test ID: " + testId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult UpdateTest(string testId, string courseId, string field, string testContent, string type)
        {
            var test = dbc.Tests.Find(testId);
            test.TestId = testId;
            test.CourseId = courseId;
            test.Field = field;
            test.TestContent = testContent;
            test.Type = type;

            dbc.Tests.Update(test);
            dbc.SaveChanges();

            return Ok("Update Test ID " + testId + " successfully!");
        }
        [HttpGet]
        [Route("GetTestsByInstructor")]
        public IActionResult GetTestsByInstructor(string instructorId)
        {
            // Lấy tất cả các khóa học của giảng viên
            var courses = dbc.Courses.Where(c => c.InstructorId == instructorId).ToList();

            if (courses == null || !courses.Any())
            {
                return NotFound(new { success = false, message = "No courses found for this instructor." });
            }

            // Lọc các bài kiểm tra của các khóa học mà giảng viên đang dạy
            var tests = dbc.Tests
                .Include(t => t.Course)  // Bao gồm thông tin khóa học
                .Where(t => courses.Any(c => c.CourseId == t.CourseId))  // Lọc bài kiểm tra thuộc về các khóa học giảng viên đang dạy
                .ToList();

            if (tests == null || !tests.Any())
            {
                return NotFound(new { success = false, message = "No tests found for the instructor's courses." });
            }

            return Ok(tests);  // Trả về danh sách bài kiểm tra
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteTest(string testId)
        {
            var test = dbc.Tests.Find(testId);

            dbc.Tests.Remove(test);
            dbc.SaveChanges();

            return Ok("Delete Test ID " + testId + " successfully!");
        }
    }
}
