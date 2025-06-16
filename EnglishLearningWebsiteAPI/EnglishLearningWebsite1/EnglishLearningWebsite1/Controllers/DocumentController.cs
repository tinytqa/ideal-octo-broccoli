using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController: ControllerBase
    {
        DBCProfessionalProj dbc;
        public DocumentController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Documents.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddDocument(string documentId, string lessonId, string title, string fileUrl)
        {
            Document document = new Document();
            document.DocumentId = documentId;
            document.LessonId = lessonId;
            document.Title = title;
            document.FileUrl = fileUrl;
           
            dbc.Documents.Add(document);
            dbc.SaveChanges();

            return Ok("Insert Document ID: " + documentId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult UpdateDocument(string documentId, string lessonId, string title, string fileUrl)
        {
            Document document = new Document();
            document.DocumentId = documentId;
            document.LessonId = lessonId;
            document.Title = title;
            document.FileUrl = fileUrl;

            dbc.Documents.Update(document);
            dbc.SaveChanges();

            return Ok("Update Document ID: " + documentId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteDocument(string documentId)
        {
            var document = dbc.Documents.Find(documentId);

            dbc.Documents.Remove(document);
            dbc.SaveChanges();

            return Ok("Delete Document ID " + documentId + " successfully!");
        }
    
}
}
