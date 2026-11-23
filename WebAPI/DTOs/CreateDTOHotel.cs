using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class CreateDTOHotel
    {
        [MaxLength(70)] public required string Name { get; set; }
        [MaxLength(120)] public required string Address { get; set; }
        [Range(1, 5)] public required double Rating { get; set; }
       
        [Required] public required int CountryId { get; set; }
    }
    public class UpdateDTOHotel : CreateDTOHotel
    {
        [Required]public required int Id { get; set; }
    }
    public record GetDTOHotel(int Id, string Name,  string Address, double Rating, string? Country);
    public record GetDTOHotels(int Id, string Name,  string Address, double Rating, int Country);
    

}
