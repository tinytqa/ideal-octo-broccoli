using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EnglishLearningWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetItemByForeignKeyController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=cmcsv.ric.vn,10000;Initial Catalog=duc_dacn;Persist Security Info=True;User ID=cmcsv;Password=cM!@#2025;encrypt=false";

        
        [HttpGet("UpdateCount/{url}/{userid}")]
        public IActionResult UpdateCount(string url,string userid)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_tblViewCount_Update_Add", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vc_url", url);
                cmd.Parameters.AddWithValue("@vc_userId", userid);

                conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();
                return Ok("DONE");
            }
        }

        [HttpGet("GetChapters/{courseId}")]
        public IActionResult GetChapters(string courseId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("GetChaptersByCourse", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@courseId", courseId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var chapters = new List<object>();

                while (reader.Read())
                {
                    chapters.Add(new
                    {
                        ChapterId = reader["chapterId"],
                        ChapterTitle = reader["chapterTitle"]
                    });
                }
                return Ok(chapters);
            }
        }

        [HttpGet("GetLessons/{chapterId}")]
        public IActionResult GetLessons(string chapterId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("GetLessonsByChapter", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@chapterId", chapterId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var lessons = new List<object>();

                while (reader.Read())
                {
                    lessons.Add(new
                    {
                        LessonId = reader["lessonId"],
                        LessonTitle = reader["lessonTitle"]
                    });
                }
                return Ok(lessons);
            }
        }

        [HttpGet("GetLessonContent/{lessonId}")]
        public IActionResult GetLessonContent(string lessonId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("GetLessonContent", conn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@lessonId", lessonId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var contents = new List<object>();

                while (reader.Read())
                {
                    contents.Add(new
                    {
                        ContentType = reader["contentType"],
                        ContentId = reader["contentId"],
                        ContentTitle = reader["contentTitle"],
                        FileUrl = reader["fileUrl"],
                        Description = reader["description"]
                    });
                }
                return Ok(contents);
            }
        }
    }
}
