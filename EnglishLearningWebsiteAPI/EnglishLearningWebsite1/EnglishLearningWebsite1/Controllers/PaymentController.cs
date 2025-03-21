using EnglishLearningWebsite1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace LearningWebsiteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly DBCProfessionalProj dbc;
        private readonly string _connectionString;

        public PaymentController(DBCProfessionalProj dbc)
        {
            this.dbc = dbc;
            _connectionString = dbc.Database.GetConnectionString();
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetList()
        {
            return Ok(dbc.Payments.ToList());
        }

        private string TaoPaymentID()
        {
            string id = "";
            var cmd = dbc.Database.GetDbConnection().CreateCommand();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "payment_getID";

            if (cmd.Connection.State != ConnectionState.Open)
                cmd.Connection.Open();

            id = cmd.ExecuteScalar().ToString();

            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();

            return id;
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult AddPayment(string paymentId, string studentId, string courseId, DateTime? paymentDate, decimal amount)
        {
            try
            {
                // Tạo ID mới nếu không có
                if (paymentId == "null" || string.IsNullOrEmpty(paymentId))
                    paymentId = TaoPaymentID();

                // Sử dụng stored procedure thay vì thao tác trực tiếp
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("sp_InsertPayment", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@paymentId", paymentId);
                    command.Parameters.AddWithValue("@studentId", studentId);
                    command.Parameters.AddWithValue("@courseId", courseId);
                    command.Parameters.AddWithValue("@paymentDate", paymentDate ?? DateTime.Now);
                    command.Parameters.AddWithValue("@amount", amount);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return Ok(new { success = true, message = "Insert Payment ID: " + paymentId + " successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult UpdatePayment(string paymentId, string studentId, string courseId, DateTime? paymentDate, decimal amount)
        {
            try
            {
                // Kiểm tra xem payment có tồn tại không
                var paymentExists = dbc.Payments.Any(p => p.PaymentId == paymentId);
                if (!paymentExists)
                {
                    return NotFound(new { success = false, message = "Payment not found" });
                }

                // Kiểm tra thông tin studentId và courseId từ bản ghi hiện tại
                var currentPayment = dbc.Payments.Find(paymentId);
                if (studentId != currentPayment.StudentId || courseId != currentPayment.CourseId)
                {
                    // Nếu có sự thay đổi về studentId hoặc courseId, cập nhật thông tin trong EF
                    currentPayment.StudentId = studentId;
                    currentPayment.CourseId = courseId;
                    currentPayment.PaymentDate = paymentDate ?? currentPayment.PaymentDate;
                    dbc.SaveChanges();
                }

                // Sử dụng stored procedure để cập nhật amount
                using (var connection = new SqlConnection(_connectionString))
                {
                    var command = new SqlCommand("sp_UpdatePayment", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@paymentId", paymentId);
                    command.Parameters.AddWithValue("@amount", amount);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return Ok(new { success = true, message = "Update Payment ID: " + paymentId + " successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult DeletePayment(string paymentId)
        {
            try
            {
                var payment = dbc.Payments.Find(paymentId);
                if (payment == null)
                {
                    return NotFound(new { success = false, message = "Payment not found" });
                }

                dbc.Payments.Remove(payment);
                dbc.SaveChanges();

                return Ok(new { success = true, message = "Delete Payment ID: " + paymentId + " successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error: " + ex.Message });
            }
        }
    }
}