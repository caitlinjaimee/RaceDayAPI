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
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public ResultsController(RaceDayContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetResults()
        {
            return Ok(await _context.Results.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResult(int id)
        {
            var result = await _context.Results.FindAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Organiser")]
        public async Task<IActionResult> CreateResult(Result result)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Event)
                .FirstOrDefaultAsync(
                    e => e.EnrollmentID == result.EnrollmentID);

            if (enrollment == null)
                return BadRequest("Enrollment not found.");

            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!int.TryParse(userId, out int organiserId) ||
                enrollment.Event?.OrganiserID != organiserId)
            {
                return Forbid();
            }

            bool resultExists = await _context.Results
                .AnyAsync(r =>
                    r.EnrollmentID == result.EnrollmentID);

            if (resultExists)
                return BadRequest("A result already exists.");

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }
    }
}