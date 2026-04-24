using HotelAPI.Data;
using HotelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class RoomsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            return Ok(await _context.Rooms.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return Ok(room);
        }
    }
}
