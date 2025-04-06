using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnglishLearningWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        DBCProfessionalProj dbc;

        public VideoController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Videos.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddVideo(string videoId, string lessonId, string title, string fileUrl, string description)
        {
            Video video = new Video();
            video.VideoId = videoId;
            video.LessonId = lessonId;
            video.Title = title;
            video.FileUrl = fileUrl;
            video.Description = description;

            dbc.Videos.Add(video);
            dbc.SaveChanges();

            return Ok("Insert Video ID: " + videoId + " successfully!");
        }
        [HttpGet("GetByInstructor")]
        public IActionResult GetVideosByInstructor(string userId)
        {
            var videos = dbc.Videos
                .Include(v => v.Lesson)
                    .ThenInclude(l => l.Chapter)
                        .ThenInclude(c => c.Course)
                .Where(v => v.Lesson != null &&
                            v.Lesson.Chapter != null &&
                            v.Lesson.Chapter.Course != null &&
                            v.Lesson.Chapter.Course.Instructor != null &&
                            v.Lesson.Chapter.Course.Instructor.UserId == userId)
                .Select(v => new {
                    v.VideoId,
                    v.Title,
                    v.FileUrl,
                    v.Description,
                    LessonTitle = v.Lesson.Title
                })
                .ToList();

            return Ok(videos);
        }

        [HttpPost]
        [Route("Update")]
        
        public IActionResult UpdateVideo(string videoId, string lessonId, string title, string fileUrl, string description)
        {
            var video = dbc.Videos.Find(videoId);

            video.VideoId = videoId;
            video.LessonId = lessonId;
            video.Title = title;
            video.FileUrl = fileUrl;
            video.Description = description;

            dbc.Videos.Update(video);
            dbc.SaveChanges();

            return Ok("Update Video ID " + videoId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteVideo(string videoId)
        {
            var video = dbc.Videos.Find(videoId);

            dbc.Videos.Remove(video);
            dbc.SaveChanges();

            return Ok("Delete Video ID " + videoId + " successfully!");
        }
    }
}
