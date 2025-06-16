using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        DBCProfessionalProj dbc;
        public InstructorController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
        }

        [HttpGet]
        [Route("GetList")]

        public IActionResult GetList()
        {
            return Ok(dbc.Instructors.ToList());
        }

        [HttpPost]
        [Route("Insert")]

        public IActionResult AddInstructor(string instructorId, string userId, string bio, string expertise)
        {
            Instructor instructor = new Instructor();
            instructor.InstructorId = instructorId;
            instructor.UserId = userId;
            instructor.Bio= bio;
            instructor.Expertise = expertise;
           
            dbc.Instructors.Add(instructor);
            dbc.SaveChanges();

            return Ok("Insert Instructor ID: " + instructorId + " successfully!");
        }

        [HttpPost]
        [Route("Update")]

        public IActionResult UpdateInstructor(string instructorId, string userId, string bio, string expertise)
        {
            Instructor instructor = new Instructor();
            instructor.InstructorId = instructorId;
            instructor.UserId = userId;
            instructor.Bio = bio;
            instructor.Expertise = expertise;

            dbc.Instructors.Update(instructor);
            dbc.SaveChanges();

            return Ok("UpdateInstructor ID: " + instructorId + " successfully!");
        }

        [HttpPost]
        [Route("Delete")]

        public IActionResult DeleteInstructor(string instructorId)
        {
            var instructor = dbc.Instructors.Find(instructorId);

            dbc.Instructors.Remove(instructor);
            dbc.SaveChanges();

            return Ok("Delete Instructor ID " + instructorId + " successfully!");
        }

        [HttpGet]
        [Route("GetInstructorById/{instructorId}")]
        public IActionResult GetInstructorById(string instructorId)
        {
            var instructor = dbc.Instructors.Find(instructorId);
            if (instructor == null)
            {
                return NotFound("Instructor not found with ID: " + instructorId);
            }
            return Ok(instructor);
        }

    }
}
