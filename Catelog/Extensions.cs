//Extension method extends the definiton of one type by adding a method 
//that can be executed on that type
using Catelog.Dtos;
using Catelog.Entities;

namespace Catalog
{
    public static class Extensions
    {
        //will return ItemDto
        //operates on current item
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}