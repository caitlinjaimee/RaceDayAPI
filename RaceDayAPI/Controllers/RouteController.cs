
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Data;
using RouteModel = RaceDayAPI.Models.Route;

namespace RaceDayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public RoutesController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: api/Routes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteModel>>> GetRoutes()
        {
            return await _context.Routes.ToListAsync();
        }

        // GET: api/Routes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RouteModel>> GetRoute(int id)
        {
            var route = await _context.Routes
                .FirstOrDefaultAsync(r => r.RouteID == id);

            if (route == null)
            {
                return NotFound();
            }

            return Ok(route);
        }

        // GET: api/Routes/event/1
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult<RouteModel>> GetRouteByEvent(int eventId)
        {
            var route = await _context.Routes
                .FirstOrDefaultAsync(r => r.EventID == eventId);

            if (route == null)
            {
                return NotFound("No route was found for this event.");
            }

            return Ok(route);
        }

        // POST: api/Routes
        [HttpPost]
        [Authorize(Policy = "OrganiserOnly")]
        public async Task<ActionResult<RouteModel>> AddRoute(RouteModel route)
        {
            var eventExists = await _context.Events
                .AnyAsync(e => e.EventID == route.EventID);

            if (!eventExists)
            {
                return BadRequest("The specified event does not exist.");
            }

            var routeExists = await _context.Routes
                .AnyAsync(r => r.EventID == route.EventID);

            if (routeExists)
            {
                return Conflict("A route already exists for this event.");
            }

            _context.Routes.Add(route);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetRoute),
                new { id = route.RouteID },
                route);
        }
    }
}


