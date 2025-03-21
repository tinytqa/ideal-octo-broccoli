using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        DBCProfessionalProj dbc;
        public ArticleController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Articles.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddArticle(string articleId, string instructorId, string title, string articalContent, DateTime? postedDate, string courseId)
        {
            Article article = new Article();
            article.ArticleId = articleId;
            article.InstructorId = instructorId;
            article.Title = title;
            article.ArticalContent = instructorId;
            article.PostedDate = postedDate;
            article.CourseId = courseId;
            dbc.Articles.Add(article);
            dbc.SaveChanges();

            return Ok("Insert Article ID: " + articleId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult UpdateArticle(string articleId, string instructorId, string title, string articalContent, DateTime? postedDate, string courseId)
        {
            Article article = new Article();
            article.ArticleId = articleId;
            article.InstructorId = instructorId;
            article.Title = title;
            article.ArticalContent = instructorId;
            article.PostedDate = postedDate;
            article.CourseId = courseId;
            dbc.Articles.Update(article);
            dbc.SaveChanges();

            return Ok("Update Article ID: " + articleId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteArticle(string articleId)
        {
            var article = dbc.Articles.Find(articleId);

            dbc.Articles.Remove(article);
            dbc.SaveChanges();

            return Ok("Delete Article ID " + articleId + " successfully!");
        }
    
}
}
