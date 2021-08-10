//Repository - in charge of storing the items in the system
//Will only use IN MEMORY Repository for now then use a DB

using System;
using System.Collections.Generic;
using System.Linq;
using Catelog.Entities;

namespace Catelog.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        //ITEMS is our COLLECTION name
        //define list of items that will be initializers we will work with
        //ReadOnly bc the instance of the list shouldnt change after u 
        //construct repository object
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow }, //means UTC time right now}
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        };

        //GET items
        //IEnumerable is basic interface where u can return collection of items
        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        //Get Single Item
        public Item GetItem(Guid id)
        {
            //where Id = param Id
            //Returns collection but u only want the one item it finds
            //default will be null in case it returns nothing
            return items.Where(item => item.Id == id).SingleOrDefault();
        }

        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            //find index of the item to update
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);

            //this updates item in right location
            items[index] = item;
        }

        public void DeleteItem(Guid id)
        {
            //find index of the item to delete
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
        }
    }
}


