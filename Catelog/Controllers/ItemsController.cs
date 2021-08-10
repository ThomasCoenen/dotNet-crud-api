using System;
using System.Collections.Generic;
using System.Linq;
using Catalog;
using Catalog.Dtos;
using Catelog.Dtos;
using Catelog.Entities;
using Catelog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catelog.Controllers
{
    [ApiController]  //mark as API controller. brings in additional default behavious
    [Route("items")]  //declare the routes 

    //inherit from Controller base. turns it into a controller class
    public class ItemsController : ControllerBase
    {
        //grab instance of repository
        //readonly, bc it wont be modified after construction
        private readonly IItemsRepository repository;

        //anytime any req is made it will refer to interface of repository
        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        //GET /items
        //GET all itemsController
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            //Instead of doing Item, do AsDto (for client API security). Can do this w/
            //a projection(see Extensions.cs)
            var items = repository.GetItems().Select(item => item.AsDto());
            return items;
        }


        //GET /items/{id}
        //GET one item
        //"{id}"  -> template for placing a param string in route
        //ActionResult: allows u to return MORE THAN ONE TYPE - so we normally
        //will return an Item object but if its not found will return something
        //else
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);

            //return proper status code if item isnt found
            //404 = NotFound status code
            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
            //can do this to:
            //return Ok(item);
        }


        //POST an item /items
        //ActionResult bc u can return more than one thing
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreateItem(item);  //return item that was created

            //return Header that allows u to find info on this newly created item
            //create an anonymous type of id. Then convert item to Dto(this is whats getting returned)
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.AsDto());
        }



        //PUT - update item -> /items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {

            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            //create updated item if found
            //WITH take a COPY of existingItem
            //Take COPY of existingItem WITH the 2 props modified
            Item updatedItem = existingItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            //send the updated Item
            repository.UpdateItem(updatedItem);

            //for Put request dont normally return anything
            return NoContent();  //204 = No Content
        }


        //Delete item = /items/{id}
        //Dont need another DTO bc we just need to supply an ID
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repository.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            repository.DeleteItem(id);
            return NoContent(); //204 = No Content
        }
    }
}