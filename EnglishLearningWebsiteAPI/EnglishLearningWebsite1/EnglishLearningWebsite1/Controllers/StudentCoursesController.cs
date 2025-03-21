using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCoursesController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=cmcsv.ric.vn,10000;Initial Catalog=duc_dacn;Persist Security Info=True;User ID=cmcsv;Password=cM!@#2025;encrypt=false";

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetStudentCourses(string userId)
        {
            var courses = new List<object>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetStudentCoursesAndVideos", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", userId);

                    await conn.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            courses.Add(new
                            {
                                CourseId = reader["courseId"].ToString(),
                                CourseName = reader["courseName"].ToString(),
                                Description = reader["description"].ToString(),
                                Price = Convert.ToDecimal(reader["price"]),
                                ChapterId = reader["chapterId"].ToString(),
                                ChapterTitle = reader["chapterTitle"].ToString(),
                                LessonId = reader["lessonId"].ToString(),
                                LessonTitle = reader["lessonTitle"].ToString(),
                                VideoId = reader["videoId"]?.ToString(),
                                VideoTitle = reader["videoTitle"]?.ToString(),
                                VideoUrl = reader["videoUrl"]?.ToString(),
                                DocumentId = reader["documentId"]?.ToString(),
                                DocumentTitle = reader["documentTitle"]?.ToString(),
                                DocumentUrl = reader["documentUrl"]?.ToString(),
                                QuizzId = reader["quizzId"]?.ToString(),
                                QuizzTitle = reader["quizzTitle"]?.ToString(),
                                QuizzUrl = reader["quizzUrl"]?.ToString()
                            });
                        }
                    }
                }
            }

            return Ok(courses);
        }
    }
}
