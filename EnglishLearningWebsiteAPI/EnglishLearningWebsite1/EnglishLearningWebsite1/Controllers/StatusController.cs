using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnglishLearningWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        DBCProfessionalProj dbc;

        public StatusController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Statuses.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddStatus(string statusId, string studentId, string courseId, string progress, string status, DateTime completionDate)
        {
            Status status1 = new Status();
            status1.StatusId = statusId;
            status1.StudentId = studentId;
            status1.CourseId = courseId;
            status1.Progress = progress;
            status1.Status1 = status;
            status1.CompletionDate = completionDate;

            dbc.Statuses.Add(status1);
            dbc.SaveChanges();

            return Ok("Insert Status ID: " + statusId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult UpdateStatus(string statusId, string studentId, string courseId, string progress, string status, DateTime completionDate)
        {
            var status1 = dbc.Statuses.Find(statusId);

            status1.StatusId = statusId;
            status1.StudentId = studentId;
            status1.CourseId = courseId;
            status1.Progress = progress;
            status1.Status1 = status;
            status1.CompletionDate = completionDate;

            dbc.Statuses.Update(status1);
            dbc.SaveChanges();

            return Ok("Update Status ID " + statusId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteStatus(string statusId)
        {
            var status1 = dbc.Statuses.Find(statusId);

            dbc.Statuses.Remove(status1);
            dbc.SaveChanges();

            return Ok("Delete Status ID " + statusId + " successfully!");
        }
    }
}
