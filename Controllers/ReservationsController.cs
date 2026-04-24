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
        public async Task<IActionResult> GetReservations()
        {
            return Ok(await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Room)
                .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(Reservation reservation)
        {
            bool isBooked = await _context.Reservations.AnyAsync(r =>
                r.RoomId == reservation.RoomId &&
                (
                    reservation.CheckIn <= r.CheckOut &&
                    reservation.CheckOut >= r.CheckIn
                ));

            if (isBooked)
                return BadRequest("Room already booked!");

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return Ok(reservation);
        }
    }
}
