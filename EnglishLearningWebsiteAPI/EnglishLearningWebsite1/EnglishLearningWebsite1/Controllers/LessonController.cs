using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        DBCProfessionalProj dbc;
        public LessonController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Lessons.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddLesson(string lessonId, string title, string chapterId)
        {
            Lesson lesson = new Lesson();
            lesson.LessonId = lessonId;
            lesson.Title = title;
            lesson.ChapterId = chapterId;

            dbc.Lessons.Add(lesson);
            dbc.SaveChanges();

            return Ok("Insert Lesson ID: " + lessonId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult UpdateLesson(string lessonId, string title, string chapterId)
        {
            Lesson lesson = new Lesson();
            lesson.LessonId = lessonId;
            lesson.Title = title;
            lesson.ChapterId = chapterId;

            dbc.Lessons.Update(lesson);
            dbc.SaveChanges();

            return Ok("Update Lesson ID: " + lessonId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteLesson(string lessonId)
        {
            var lesson = dbc.Lessons.Find(lessonId);

            dbc.Lessons.Remove(lesson);
            dbc.SaveChanges();

            return Ok("Delete Lesson ID " + lessonId + " successfully!");
        }
    
}
}
