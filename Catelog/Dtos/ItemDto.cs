//DTO - DATA TRANSFER OBJECT 
//this is the contract enabled between our client and our API service
//Dont want to directly expose the Item
//SAME AS OUR ITEM

using System;

namespace Catelog.Dtos
{
    public record ItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}