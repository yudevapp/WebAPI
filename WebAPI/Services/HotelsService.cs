using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Contracts;

namespace WebAPI.Services
{
    public class HotelsService(HotelListingDbContext context) : IHotelsService
    {
        public async Task<GetDTOHotel> CreateHotelAsync(CreateDTOHotel createDTOHotel)
        {
            var hotel = new Hotel
            {
                Name = createDTOHotel.Name,
                Address = createDTOHotel.Address,
                Rating = createDTOHotel.Rating,
                CountryId = createDTOHotel.CountryId
            };
            context.Hotels.Add(hotel);
            await context.SaveChangesAsync();
            return new GetDTOHotel(hotel.Id, hotel.Name, hotel.Address, hotel.Rating, hotel.Country.Name);
        }
        public async Task<List<GetDTOHotels>> GetHotelsAsync()
        {
            return await context.Hotels
                .Include(h => h.Country)
                .Select(h => new GetDTOHotels(h.Id, h.Name, h.Address, h.Rating, h.CountryId)) 
                .ToListAsync();
        }
        public async Task<GetDTOHotel> GetHotelAsync(int hotelid)
        {
            var hotel = await context.Hotels
            .Where(h => h.Id == hotelid)
            .Select(h => new GetDTOHotel(h.Id, h.Name, h.Address, h.Rating, h.Country!.Name))
            .FirstOrDefaultAsync();
            if (hotel == null)
            {
                throw new Exception ($"Hotel with id {hotelid} not found");
            }
            return new GetDTOHotel(hotel.Id, hotel.Name, hotel.Address, hotel.Rating, hotel.Country);
        }
        public async Task UpdateHotelAsync(int hotelid, UpdateDTOHotel updateDTOHotel)
        {
            var hotel = await context.Hotels.FindAsync(hotelid);
            if (hotel == null)
            {
                throw new Exception($"Hotel with id {hotelid} not found");
            }
            hotel.Name = updateDTOHotel.Name;
            hotel.Address = updateDTOHotel.Address;
            hotel.Rating = updateDTOHotel.Rating;
            hotel.CountryId = updateDTOHotel.CountryId;
            context.Hotels.Update(hotel);
            await context.SaveChangesAsync();
        }
        public async Task DeleteHotelAsync(int id)
        {
            var hotel = await context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                throw new Exception($"Hotel with id {id} not found");
            }
            context.Hotels.Remove(hotel);
            await context.SaveChangesAsync();
        }
        public async Task<bool> HotelExistAsync(int? hotelid)
        {
            return await context.Hotels.AnyAsync(h => h.Id == hotelid);
             
        }
    }
}
