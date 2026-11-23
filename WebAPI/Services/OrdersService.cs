using Microsoft.EntityFrameworkCore;
using WebAPI.Contracts;
using WebAPI.Data;
using WebAPI.DTOs;

namespace WebAPI.Services
{
    public class OrdersService(HotelListingDbContext context) : IOrdersService
    {
        public async Task<GetDTOOrder> CreateOrderAsync(CreateDTOOrder createDTOOrder)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(c => c.CustomerId == createDTOOrder.CustomerId);

            if (customer == null)
                throw new Exception("Customer not found.");

            var order = new Order
            {
                CustomerId = createDTOOrder.CustomerId,
                Customer = customer,
                Name = createDTOOrder.Name
            };

            context.Orders.Add(order);
            await context.SaveChangesAsync();

            return new GetDTOOrder(
                order.OrderId,
                order.Name,
                order.CustomerId,
                customer.Name
            );
        }
        public async Task<List<GetDTOOrders>> GetOrdersAsync()
        {
            return await context.Orders
                .Include(o => o.Customer)
                .Select(o => new GetDTOOrders(o.OrderId, o.Name, o.CustomerId, o.Customer.Name))
                .ToListAsync();
        }
        public async Task<GetDTOOrder> GetOrderAsync(int orderid)
        {
            var order = await context.Orders
                .Where(o => o.OrderId == orderid)
                .Select(o => new GetDTOOrder(o.OrderId, o.Name, o.CustomerId, o.Customer.Name))
                .FirstOrDefaultAsync();
            if (order == null)
            {
                throw new Exception($"Order with id {orderid} not found");
            }
            return new GetDTOOrder(order.OrderId, order.Name, order.CustomerId, order.Customer);
        }
        public async Task UpdateOrderAsync(int orderid, UpdateDTOOrder updateDTOOrder)
        {
            var order = await context.Orders.FindAsync(orderid);
            if (order == null)
            {
                throw new Exception($"Order with id {orderid} not found");
            }
            order.Name = updateDTOOrder.Name;
            order.CustomerId = updateDTOOrder.CustomerId;
            context.Orders.Update(order);
            await context.SaveChangesAsync();
        }
        public async Task DeleteOrderAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception($"Order with id {id} not found");
            }
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }
        public async Task<bool> OrderExistsAsync(int id)
        {
            return await context.Orders.AnyAsync(o => o.OrderId == id);
        }
    }
}
