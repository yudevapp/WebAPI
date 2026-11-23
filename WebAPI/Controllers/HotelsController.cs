using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Contracts;
using WebAPI.Data;
using WebAPI.DTOs;

[Route("api/[controller]")]
[ApiController]
public class HotelsController(IHotelsService hotelsService) : ControllerBase
{
    public static List<Hotel> Hotels { get; set; } = new List<Hotel>
    {
        new Hotel { Id = 1, Name = "Hotel 1", Address = "Address 1", CountryId = 1, Rating = 4.5 },
        new Hotel { Id = 2, Name = "Hotel 2", Address = "Address 2", CountryId = 2, Rating = 3.8 }
    };
    // GET: api/Hotel
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDTOHotels>>> GetHotels()
    {
        // SELECT * FROM Hotels LEFT JOIN Countries ON Hotels. CountryId= Countries. CountryId
        var hotels = await hotelsService.GetHotelsAsync();
        return Ok((hotels));
    }

    // GET: api/Hotel/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetDTOHotel>> GetHotel(int id)
    {
        var hotel = await hotelsService.GetHotelAsync(id);
        if (hotel == null)
        {
            return NotFound();
        }
        return hotel;
    }

    // PUT: api/Hotel/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotel(int id, UpdateDTOHotel hotelDTO)
    {
        if (id != hotelDTO.Id)
        {
            return BadRequest();
        }

        await hotelsService.UpdateHotelAsync(id, hotelDTO);
        return NoContent();
    }

    // POST: api/Hotel
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Hotel>> PostHotel(CreateDTOHotel hotel)
    {
        var newHotel = await hotelsService.CreateHotelAsync(hotel);
        return CreatedAtAction("GetHotel", new { id = newHotel.Id }, newHotel);
    }

    // DELETE: api/Hotel/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotel(int id)
    {
        await hotelsService.DeleteHotelAsync(id);
        return NoContent();
    }

   
}
