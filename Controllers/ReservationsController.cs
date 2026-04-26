using HotelAPI.Data;
using HotelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
       private readonly AppDbContext _context;

public ReservationsController(AppDbContext context)
{
    _context = context;
}

[HttpGet]
public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
{
    return await _context.Reservations
        .Include(r => r.Client)
        .Include(r => r.Room)
        .ToListAsync();
}

[HttpGet("{id}")]
public async Task<ActionResult<Reservation>> GetReservation(int id)
{
    var reservation = await _context.Reservations
        .Include(r => r.Client)
        .Include(r => r.Room)
        .FirstOrDefaultAsync(r => r.Id == id);

    if (reservation == null) return NotFound();
    return reservation;
}

[HttpPost]
public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
{
    _context.Reservations.Add(reservation);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
}

[HttpPut("{id}")]
public async Task<IActionResult> UpdateReservation(int id, Reservation reservation)
{
    if (id != reservation.Id) return BadRequest();

    _context.Entry(reservation).State = EntityState.Modified;
    await _context.SaveChangesAsync();

    return NoContent();
}

[HttpDelete("{id}")]
public async Task<IActionResult> DeleteReservation(int id)
{
    var reservation = await _context.Reservations.FindAsync(id);
    if (reservation == null) return NotFound();

    _context.Reservations.Remove(reservation);
    await _context.SaveChangesAsync();

    return NoContent();
}
    }
}
