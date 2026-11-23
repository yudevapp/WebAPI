using WebAPI.Data;
using WebAPI.DTOs;

namespace WebAPI.Contracts
{
    public interface ICustomerService
    {
        Task<GetDTOCustomer> CreateCustomerAsync(CreateDTOCustomer createDTOCustomer);
        Task<List<GetDTOCustomers>> GetCustomersAsync();
        Task<GetDTOCustomer> GetCustomerAsync(int customerid);
        Task UpdateCustomerAsync(int customerid, UpdateDTOCustomer updateDTOCustomer);
        Task DeleteCustomerAsync(int id);
        Task<bool> CustomerExistsAsync(int id); 
    }
}