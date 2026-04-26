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

 // GET ALL
 [HttpGet]
 public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
 {
     return await _context.Rooms.ToListAsync();
 }

 // GET BY ID
 [HttpGet("{id}")]
 public async Task<ActionResult<Room>> GetRoom(int id)
 {
     var room = await _context.Rooms.FindAsync(id);
     if (room == null) return NotFound();
     return room;
 }

 // CREATE
 [HttpPost]
 public async Task<ActionResult<Room>> CreateRoom(Room room)
 {
     _context.Rooms.Add(room);
     await _context.SaveChangesAsync();

     return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
 }

 // UPDATE
 [HttpPut("{id}")]
 public async Task<IActionResult> UpdateRoom(int id, Room room)
 {
     if (id != room.Id) return BadRequest();

     _context.Entry(room).State = EntityState.Modified;
     await _context.SaveChangesAsync();

     return NoContent();
 }

 // DELETE
 [HttpDelete("{id}")]
 public async Task<IActionResult> DeleteRoom(int id)
 {
     var room = await _context.Rooms.FindAsync(id);
     if (room == null) return NotFound();

     _context.Rooms.Remove(room);
     await _context.SaveChangesAsync();

     return NoContent();
 }
    }
}
