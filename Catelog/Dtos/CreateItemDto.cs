using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos
{
    public record CreateItemDto
    {
        //only need to include Name and Price bc server generates ID and CreatedDate 

        //DATA Anotations to prevent for example a null value showing up as name
        [Required]
        public string Name { get; init; }

        [Required]
        //Range(low, high) allows u to accept a range of value for a param
        [Range(1, 1000)]
        public decimal Price { get; init; }
    }
}