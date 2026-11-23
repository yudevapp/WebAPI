using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Contracts;
using WebAPI.Data;
using WebAPI.DTOs;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrdersService orderService) : ControllerBase
{
    public static List<Order> Orders { get; set; } = new List<Order>
    {
        new Order { OrderId = 1, Name = "Order 1", CustomerId = 1 },
        new Order { OrderId = 2, Name = "Order 2", CustomerId = 2 },
        new Order { OrderId = 3, Name = "Order 3", CustomerId = 1 },
        new Order { OrderId = 4, Name = "Order 4", CustomerId = 3 },
        new Order { OrderId = 5, Name = "Order 5", CustomerId = 2 },
        new Order { OrderId = 6, Name = "Order 6", CustomerId = 3 }
    };
   

    // GET: api/Order
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDTOOrders>>> GetOrders()
    {
        var orders = await orderService.GetOrdersAsync();
        return Ok(orders);
    }

    // GET: api/Order/5
    [HttpGet("{orderid}")]
    public async Task<ActionResult<GetDTOOrder>> GetOrder(int orderid)
    {
        var order = await orderService.GetOrderAsync(orderid);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    // PUT: api/Order/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{orderid}")]
    public async Task<IActionResult> PutOrder(int orderid, UpdateDTOOrder orderDTO)
    {
        await orderService.UpdateOrderAsync(orderid, orderDTO);
        return NoContent();
    }

    // POST: api/Order
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Order>> PostOrder(CreateDTOOrder order)
    {
        var newOrder = await orderService.CreateOrderAsync(order);

        return CreatedAtAction("GetOrder", new { orderid = newOrder.OrderId }, newOrder);
    }

    // DELETE: api/Order/5
    [HttpDelete("{orderid}")]
    public async Task<IActionResult> DeleteOrder(int orderid)
    {
        await orderService.DeleteOrderAsync(orderid);
        return NoContent();
    }

  
}
