using Microsoft.EntityFrameworkCore;
using WebAPI.Contracts;
using WebAPI.Data;
using WebAPI.DTOs;

namespace WebAPI.Services
{
    public class CustomerService(HotelListingDbContext context) : ICustomerService
    {
        public async Task<GetDTOCustomer> CreateCustomerAsync(CreateDTOCustomer createDTOCustomer)
        {
            var newCustomer = new Customer
            {
                Name = createDTOCustomer.Name,
                Email = createDTOCustomer.Email
            };
            context.Customers.Add(newCustomer);
            await context.SaveChangesAsync();
            return new GetDTOCustomer(newCustomer.CustomerId, newCustomer.Name, newCustomer.Email, newCustomer.Orders);

        }
        public async Task<List<GetDTOCustomers>> GetCustomersAsync()
        {
            return await context.Customers
                .Include(c => c.Orders)
                .Select(c => new GetDTOCustomers(c.CustomerId, c.Name, c.Email, c.Orders.Count))
                .ToListAsync();
        }
        public async Task<GetDTOCustomer> GetCustomerAsync(int customerid)
        {
            var customer = await context.Customers
                .Where(c => c.CustomerId == customerid)
                .Select(c => new GetDTOCustomer(c.CustomerId, c.Name, c.Email, c.Orders.ToList()))
                .FirstOrDefaultAsync();
            if (customer == null)
            {
                throw new Exception($"Customer with id {customerid} not found");
            }
            return new GetDTOCustomer(customer.CustomerId, customer.Name, customer.Email, customer.Orders);
        }
        public async Task UpdateCustomerAsync(int customerid, UpdateDTOCustomer updateDTOCustomer)
        {
            var customer = await context.Customers.FindAsync(customerid);
            if (customer == null)
            {
                throw new Exception($"Customer with id {customerid} not found");
            }
            customer.Name = updateDTOCustomer.Name;
            customer.Email = updateDTOCustomer.Email;
            context.Customers.Update(customer);
            await context.SaveChangesAsync();
        }
        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception($"Customer with id {id} not found");
            }
            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
        }
        public async Task<bool> CustomerExistsAsync(int id)
        {
            return await context.Customers.AnyAsync(c => c.CustomerId == id);
        }
    }
}
