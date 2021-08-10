//This is the INTERFACE method
using System;
using System.Collections.Generic;
using Catelog.Entities;

namespace Catelog.Repositories
{
    //use DEPENDENCY INJECTION to implement an ITERFACE interface
    public interface IItemsRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();

        //POST Item
        void CreateItem(Item item);

        //Update Item
        void UpdateItem(Item item);

        //Delete item
        void DeleteItem(Guid id);
    }

}