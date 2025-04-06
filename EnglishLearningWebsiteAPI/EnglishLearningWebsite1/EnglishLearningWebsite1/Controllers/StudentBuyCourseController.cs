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
        [HttpGet]
        [Route("CountStudentsByInstructor")]
        public IActionResult CountStudentsByInstructor(string instructorId)
        {
            // Lấy danh sách các khóa học mà giảng viên này dạy
            var courseIds = dbc.Courses
                               .Where(c => c.InstructorId == instructorId)
                               .Select(c => c.CourseId)
                               .ToList();

            // Lấy danh sách học viên tham gia các khóa học đó (nếu một học viên học nhiều khóa, tính duy nhất)
            var studentCount = dbc.StudentBuyCourses
                                  .Where(sbc => courseIds.Contains(sbc.CourseId))
                                  .Select(sbc => sbc.StudentId)
                                  .Distinct()
                                  .Count();

            return Ok(new { count = studentCount });
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
