using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RaceDayAPI.Models;
using System.Security.Claims;

namespace RaceDayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EnrollmentsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public EnrollmentsController(RaceDayContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEnrollments()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!int.TryParse(userId, out int participantId))
                return Unauthorized();

            var enrollments = await _context.Enrollments
                .Where(e => e.ParticipantID == participantId)
                .ToListAsync();

            return Ok(enrollments);
        }

        [HttpPost]
        [Authorize(Roles = "Participant")]
        public async Task<IActionResult> CreateEnrollment(
            Enrollment enrollment)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!int.TryParse(userId, out int participantId))
                return Unauthorized();

            var eventExists = await _context.Events
                .AnyAsync(e => e.EventID == enrollment.EventID);

            var categoryExists = await _context.Categories
                .AnyAsync(c =>
                    c.CategoryID == enrollment.CategoryID &&
                    c.EventID == enrollment.EventID);

            if (!eventExists || !categoryExists)
            {
                return BadRequest("Invalid event or category.");
            }

            bool alreadyEnrolled = await _context.Enrollments
                .AnyAsync(e =>
                    e.ParticipantID == participantId &&
                    e.EventID == enrollment.EventID);

            if (alreadyEnrolled)
            {
                return BadRequest("You are already enrolled.");
            }

            enrollment.ParticipantID = participantId;
            enrollment.EnrollmentDate = DateTime.Now;
            enrollment.Status = "Registered";

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return Ok(enrollment);
        }
    }
}