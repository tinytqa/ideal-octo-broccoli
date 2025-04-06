using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearningWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        DBCProfessionalProj dbc;
        public ChapterController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Chapters.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddChapter(string chapterId, string courseId, string title)
        {
            Chapter chapter = new Chapter();
            chapter.ChapterId = chapterId;
            chapter.Title = title;
            chapter.CourseId = courseId;

            dbc.Chapters.Add(chapter);
            dbc.SaveChanges();

            return Ok("Insert Chapter ID: " + chapterId + " successfully!");
        }
        [HttpGet("GetChaptersByInstructor")]
        public IActionResult GetChaptersByInstructor(string instructorId)
        {
            var chapters = dbc.Chapters
                .Include(c => c.Course) // Include để lấy thông tin Course
                .Where(c => c.Course.InstructorId == instructorId) // Lọc theo Instructor
                .Select(c => new {
                    c.ChapterId,
                    c.Title,
                    c.CourseId,
                    InstructorId = c.Course.InstructorId // thêm vào để client dễ xử lý nếu cần
                })
                .ToList();

            return Ok(chapters);
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult UpdateChapter(string chapterId, string courseId, string title)
        {
            Chapter chapter = new Chapter();
            chapter.ChapterId = chapterId;
            chapter.Title = title;
            chapter.CourseId = courseId;

            dbc.Chapters.Update(chapter);
            dbc.SaveChanges();

            return Ok("Update Chapter ID: " + chapterId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteChapter(string chapterId)
        {
            var chapter = dbc.Chapters.Find(chapterId);

            dbc.Chapters.Remove(chapter);
            dbc.SaveChanges();

            return Ok("Delete Article ID " + chapterId + " successfully!");
        }
    
}
}
