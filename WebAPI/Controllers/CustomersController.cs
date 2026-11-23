using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Contracts;
using WebAPI.Data;
using WebAPI.DTOs;

[Route("api/[controller]")]
[ApiController]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    public static List<Customer> Customers { get; set; } = new List<Customer>
    {
        new Customer { CustomerId = 1, Name = "John Doe", Email = "john.doe@example.com" },
        new Customer { CustomerId = 2, Name = "Jane Smith", Email = "jane.smith@example.com" },
        new Customer { CustomerId = 3, Name = "Bob Johnson", Email = "bob.johnson@example.com" }
    };
   

    // GET: api/Customer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetDTOCustomers>>> GetCustomer()
    {
        var customers = await customerService.GetCustomersAsync();
        return Ok(customers);
    }

    // GET: api/Customer/5
    [HttpGet("{customerid}")]
    public async Task<ActionResult<GetDTOCustomer>> GetCustomer(int customerid)
    {
        var customer = await customerService.GetCustomerAsync(customerid);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    // PUT: api/Customer/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{customerid}")]
    public async Task<IActionResult> PutCustomer(int? customerid, UpdateDTOCustomer customerDTO)
    {
        if (customerid != customerDTO.CustomerId)
        {
            return BadRequest();
        }

        await customerService.UpdateCustomerAsync(customerid.Value, customerDTO);
        return NoContent();
    }

    // POST: api/Customer
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer(CreateDTOCustomer customer)
    {
        var newCustomer = await customerService.CreateCustomerAsync(customer);
        return CreatedAtAction("GetCustomer", new { customerid = newCustomer.CustomerId }, newCustomer);
    }

    // DELETE: api/Customer/5
    [HttpDelete("{customerid}")]
    public async Task<IActionResult> DeleteCustomer(int customerid)
    {
        await customerService.DeleteCustomerAsync(customerid);
        return NoContent();
    }

}
