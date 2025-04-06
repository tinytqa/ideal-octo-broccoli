using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzController : ControllerBase
    {
        DBCProfessionalProj dbc;

        public QuizzController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Quizzs.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddQuizz(string quizzId, string lessonId, string questionUrl, string title, string keyUrl)
        {
            Quizz quizz = new Quizz();
            quizz.QuizzId = quizzId;
            quizz.LessonId = lessonId;
            quizz.QuestionUrl = questionUrl;
            quizz.Title = title;
            quizz.KeyUrl = keyUrl;

            dbc.Quizzs.Add(quizz);
            dbc.SaveChanges();

            return Ok("Insert Quizz ID: " + quizzId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult UpdateQuizz(string quizzId, string lessonId, string questionUrl, string title, string keyUrl)
        {
            var quizz = dbc.Quizzs.Find(quizzId);
            quizz.QuizzId = quizzId;
            quizz.LessonId = lessonId;
            quizz.QuestionUrl = questionUrl;
            quizz.Title = title;
            quizz.KeyUrl = keyUrl;

            dbc.Quizzs.Update(quizz);
            dbc.SaveChanges();

            return Ok("Update Quizz ID " + quizzId + " successfully!");
        }
        [HttpGet]
        [Route("GetQuizzsByInstructor")]
        public IActionResult GetQuizzsByInstructor(string instructorId)
        {
            // Lấy tất cả các khóa học mà giảng viên này dạy
            var courses = dbc.Courses.Where(c => c.InstructorId == instructorId).ToList();

            // Lấy tất cả các LessonId của các bài học thuộc về các khóa học mà giảng viên dạy
            var lessonIds = dbc.Lessons
                                .Where(l => courses.Select(c => c.CourseId).Contains(l.Chapter.CourseId))
                                .Select(l => l.LessonId)
                                .ToList();

            // Lọc các quizz (quiz) thuộc về các bài học của giảng viên
            var quizzs = dbc.Quizzs
                .Where(q => lessonIds.Contains(q.LessonId))
                .ToList();

            if (quizzs == null || !quizzs.Any())
            {
                return NotFound(new { success = false, message = "No quizz found for this instructor." });
            }

            return Ok(quizzs);
        }

        [HttpPost]
        [Route("Delete")]
        
        public IActionResult DeleteQuizz(string quizzId)
        {
            var quizz = dbc.Quizzs.Find(quizzId);

            dbc.Quizzs.Remove(quizz);
            dbc.SaveChanges();

            return Ok("Delete Quizz ID " + quizzId + " successfully!");
        }
    }
}
