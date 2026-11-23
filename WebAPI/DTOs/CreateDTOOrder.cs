using System.ComponentModel.DataAnnotations;
using WebAPI.Data;

namespace WebAPI.DTOs
{
    public class CreateDTOOrder
    {
        [MaxLength(130)] public required string Name { get; set; }

        [Required] public required int CustomerId { get; set; }
    }
    public class UpdateDTOOrder : CreateDTOOrder
    {
        [Required] public required int OrderId { get; set; }
    }
    public record GetDTOOrder(int OrderId, string? Name, int CustomerId, string? Customer);
    public record GetDTOOrders(int OrderId, string? Name, int CustomerId, string? Customer);
    
}
