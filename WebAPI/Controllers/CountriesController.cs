using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Contracts;
using WebAPI.Data;
using WebAPI.DTOs;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(ICountriesService countriesService) : ControllerBase
{
    public static List<Country> Countries { get; set; } = new List<Country>
    {
        new Country { CountryId = 1, Name = "Haiti", Code = "HT" },
        new Country { CountryId = 2, Name = "United States", Code = "US" }
    };

    // GET: api/Country
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDTOCountries>>> GetCountry()
    {
        var countries = await countriesService.GetCountriesAsync();

        return Ok(countries);
    }

    // GET: api/Country/5
    [HttpGet("{countryid}")]
    public async Task<ActionResult<GetDTOCountry>> GetCountry(int countryid)
    {
        var country = await countriesService.GetCountryAsync(countryid);
        if (country == null)
        {
            return NotFound();
        }
        return Ok(country);
    }

    // PUT: api/Country/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{countryid}")]
    public async Task<IActionResult> PutCountry(int countryid, UpdateDTOCountry countryDTO)
    {
        
        if (countryid != countryDTO.CountryId)
        {
            return BadRequest();
        }

        await countriesService.UpdateCountryAsync(countryid, countryDTO);
        return NoContent();
    }

    // POST: api/Country
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(CreateDTOCountry country)
    {
        var newCountry = await countriesService.CreateCountryAsync(country);
        return CreatedAtAction("GetCountry", new { countryid = newCountry.CountryId }, newCountry);
    }

    // DELETE: api/Country/5
    [HttpDelete("{countryid}")]
    public async Task<IActionResult> DeleteCountry(int countryid)
    {   
        await countriesService.DeleteCountryAsync(countryid);
        return NoContent();
    }

}
