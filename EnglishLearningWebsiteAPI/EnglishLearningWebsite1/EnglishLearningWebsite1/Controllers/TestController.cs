using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
