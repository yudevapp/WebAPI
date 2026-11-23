using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.DTOs;

namespace WebAPI.Contracts
{
    public interface ICountriesService
    {
        Task<GetDTOCountry> CreateCountryAsync(CreateDTOCountry createDTOCountry);
        Task<List<GetDTOCountries>> GetCountriesAsync();
        Task<GetDTOCountry> GetCountryAsync(int countryid);
        Task UpdateCountryAsync(int countryid, UpdateDTOCountry updateDTOCountry);
        Task DeleteCountryAsync(int id);
        Task<bool> CountryExistAsync(int? countryid);
    }
}