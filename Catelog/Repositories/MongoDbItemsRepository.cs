//to interact w/ MongoDB - need a MongoDB client. This is done by adding a new get package
//In term:
//dotnet add package MongoDB.Driver

//Will run a MongoDB public Docker Image for the DB
//In Term:
//-d -> so u dont have to attach to the process
//--rm -> container is destoryed after u close the process
//port = 27017
//-v -> so u dont lose the data stored in MongoDB when u stop docker container
//name of image (end part) = mongo
//This build the mongoDB Docker image
//docker run -d --rm --name mongo -p 27017:27017 -v mongodbdata:/data/db mongo

//docker ps  -> shows the docker image status

//Point to Configuration of MongoDB in appsettings.json
//appsettings.json
// "MongoDbSettings": {
//     "Host": "localhost",
//     "Port": "27017"
//   }

//Register MongoDB Repo into Startup.cs



using System;
using System.Collections.Generic;
using Catelog.Entities;
using Catelog.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "catelog";
        private const string collectionName = "items";

        //Filter Definition Builder for MongoDB filtering on Item. Builders Object of MongoDB
        //of type Item w/ Fitler
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        //Our Collection, itemsCollection
        private readonly IMongoCollection<Item> itemsCollection;

        //Recieve instance of MongoDB client here
        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            //reference to DB connection
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);

            //reference to the collection
            itemsCollection = database.GetCollection<Item>(collectionName);
        }

        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            //item.id == id(what u passed in)
            var filter = filterBuilder.Eq(item => item.Id, id);
            itemsCollection.DeleteOne(filter);
        }

        //GET single item -> /item/id
        public Item GetItem(Guid id)
        {
            //item.id == id(what u passed in)
            var filter = filterBuilder.Eq(item => item.Id, id);
            //dont want all items just want matching item
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        //GET all items -> /items
        public IEnumerable<Item> GetItems()
        {
            //want all elements
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        //PUT update item -> /items/id
        public void UpdateItem(Item item)
        {
            //existingItem.Id == item.I(what u passed in)
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            //ReplaceOne(update) the item in DB
            itemsCollection.ReplaceOne(filter, item);
        }
    }
}