using System.ComponentModel.DataAnnotations;
using WebAPI.Data;

namespace WebAPI.DTOs
{
    public class CreateDTOCustomer
    {
        [MaxLength(25)] public required string Name { get; set; }
        [MaxLength(25)] public required string Email { get; set; }
    }
    public class UpdateDTOCustomer : CreateDTOCustomer
    {
        public required int CustomerId { get; set; }
    }
    public record GetDTOCustomer(int CustomerId, string? Name, string? Email, IList<Order>? Orders);
    public record GetDTOCustomers(int CustomerId, string? Name, string? Email, int? OrderCount);
    
}
