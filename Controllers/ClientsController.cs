using HotelAPI.Data;
using HotelAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return Ok(client);
        }
    }
}
