using WebAPI.Data;
using WebAPI.DTOs;

namespace WebAPI.Contracts
{
    public interface IOrdersService
    {
        Task<GetDTOOrder> CreateOrderAsync(CreateDTOOrder createDTOOrder);
        Task DeleteOrderAsync(int id);
        Task<GetDTOOrder> GetOrderAsync(int orderid);
        Task<List<GetDTOOrders>> GetOrdersAsync();
        Task<bool> OrderExistsAsync(int id);
        Task UpdateOrderAsync(int orderid, UpdateDTOOrder updateDTOOrder);
    }
}