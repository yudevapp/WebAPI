using WebAPI.Data;
using WebAPI.DTOs;

namespace WebAPI.Contracts
{
    public interface IHotelsService
    {
        Task<GetDTOHotel> CreateHotelAsync(CreateDTOHotel createDTOHotel);
        Task<List<GetDTOHotels>> GetHotelsAsync();
        Task<GetDTOHotel> GetHotelAsync(int hotelid);
        Task UpdateHotelAsync(int hotelid, UpdateDTOHotel updateDTOHotel);
        Task DeleteHotelAsync(int id);
        Task<bool> HotelExistAsync(int? hotelid);
    }
}