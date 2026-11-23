using System.ComponentModel.DataAnnotations;
using WebAPI.Data;

namespace WebAPI.DTOs
{
    public class CreateDTOCountry
    {
        [MaxLength(30)] public required string Name { get; set; }
        [MaxLength(5)] public required string Code { get; set; }
    }
    public class UpdateDTOCountry : CreateDTOCountry
    {
        [Required] public required int CountryId { get; set; }
    }
    public record GetDTOCountry(int CountryId, string? Name, string? Code, IList<Hotel>? Hotels);
    public record GetDTOCountries(int CountryId,string? Name,string? Code, IList<Hotel>? Hotels);

}
