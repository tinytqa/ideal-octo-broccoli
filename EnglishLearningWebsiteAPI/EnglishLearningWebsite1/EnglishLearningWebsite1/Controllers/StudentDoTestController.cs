using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDoTestController : ControllerBase
    {
        DBCProfessionalProj dbc;

        public StudentDoTestController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.StudentDoTests.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddStudentDoTest(string doId, string testId, string studentId, DateTime testDate, string result)
        {
            StudentDoTest studentDoTest = new StudentDoTest();
            studentDoTest.DoId = doId;
            studentDoTest.TestId = testId;
            studentDoTest.StudentId = studentId;
            studentDoTest.TestDate = testDate;
            studentDoTest.Result = result;

            dbc.StudentDoTests.Add(studentDoTest);
            dbc.SaveChanges();

            return Ok("Insert StudentDoTest ID: " + doId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]
        
        public IActionResult UpdateStudentDoTest(string doId, string testId, string studentId, DateTime testDate, string result)
        {
            var studentDoTest = dbc.StudentDoTests.Find(doId);
            studentDoTest.DoId = doId;
            studentDoTest.TestId = testId;
            studentDoTest.StudentId = studentId;
            studentDoTest.TestDate = testDate;
            studentDoTest.Result = result;

            dbc.StudentDoTests.Update(studentDoTest);
            dbc.SaveChanges();

            return Ok("Update StudentDoTest ID " + doId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteStudentDoTest(string doId)
        {
            var studentDoTest = dbc.StudentDoTests.Find(doId);

            dbc.StudentDoTests.Remove(studentDoTest);
            dbc.SaveChanges();

            return Ok("Delete StudentDoTest ID " + doId + " successfully!");
        }
    }
}
