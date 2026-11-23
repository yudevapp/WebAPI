using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Contracts;

namespace WebAPI.Services
{
    public class CountriesService(HotelListingDbContext context) : ICountriesService
    {
        public async Task<GetDTOCountry> CreateCountryAsync(CreateDTOCountry createDTOCountry)
        {
            var country = new Country
            {
                Name = createDTOCountry.Name,
                Code = createDTOCountry.Code
            };
            context.Countries.Add(country);
            await context.SaveChangesAsync();
            return new GetDTOCountry(country.CountryId, country.Name, country.Code, country.Hotels);
        }
        public async Task<List<GetDTOCountries>> GetCountriesAsync()
        {
            return await context.Countries
                .Include(c => c.Hotels)
                .Select(c => new GetDTOCountries(c.CountryId, c.Name, c.Code, c.Hotels.ToList()))
                .ToListAsync();
        }
        public async Task<GetDTOCountry> GetCountryAsync(int countryid)
        {
            var country = await context.Countries
            .Where(c => c.CountryId == countryid)
            .Select(c => new GetDTOCountry(c.CountryId, c.Name, c.Code, c.Hotels
            .Select(h => new Hotel { Id = h.Id, Name = h.Name, Address = h.Address, Rating = h.Rating})
            .ToList()))
            .FirstOrDefaultAsync();

            if (country == null)
            {
                throw new Exception($"Country with id {countryid} not found");
            }
            return new GetDTOCountry(country.CountryId, country.Name, country.Code, country.Hotels);
        }
        public async Task UpdateCountryAsync(int countryid, UpdateDTOCountry updateDTOCountry)
        {
            var country = await context.Countries.FindAsync(countryid);
            if (country == null)
            {
                throw new Exception($"Country with id {countryid} not found");
            }
            country.Name = updateDTOCountry.Name;
            country.Code = updateDTOCountry.Code;
            context.Countries.Update(country);
            await context.SaveChangesAsync();
            
        }
        public async Task DeleteCountryAsync(int id)
        {
            var country = await context.Countries.FindAsync(id);
            if (country == null)
            {
                throw new Exception($"Country with id {id} not found");
            }
            context.Countries.Remove(country);
            await context.SaveChangesAsync();
     
        }
        public async Task<bool> CountryExistAsync(int? countryid)
        {
            return await context.Countries.AnyAsync(c => c.CountryId == countryid);
        }

        
    }
}
