using System;

namespace Catelog.Entities
{
    public record Item
    {
        //init instead of set -> good fit for property initilizer where u want to only
        //allow setting a value during initialization (IMMUTABLE PROPERTY for Init)
        public Guid Id { get; init; }

        //this would also make it immutable, but u would have to introduce a contructor here:
        //public Guid Id { get; private set; }

        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}



