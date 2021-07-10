using System;
using System.Collections.Generic;
using something.Entites;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;


//repository 
namespace something.Repository
{
    //creating  mongoDB repository class to perform storing and manupulating data.
    public class MongoDBItemRepositoy : Iitemrepository
    {
        //This string holds the name of the database. 
        private const string databaseName = "somthing";


        //This string holds the name of the database's collectoin.
        private const String Collectionname = "items";


        //This type of variable is mongoDB's defined type which used to determine the structure of the collection.
        //This variable contains several functions which are used to insert,delete,update,find..etc.
        //In this case the collection structure is equal to 'Item' structure.
        private readonly IMongoCollection<Item> itemCollection;


        //This fliter function  is used to filter the data avalible by given condition.
        private readonly FilterDefinitionBuilder<Item> fliterbuilder =Builders<Item>.Filter;


        // connecting database using IMongoClient.
        public MongoDBItemRepositoy(IMongoClient mongoClient){
            
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemCollection= database.GetCollection<Item>(Collectionname);

        }


        //function to create item in database.
        public async Task CreatedItem(Item item)
        {
           await itemCollection.InsertOneAsync(item);
        }


        //function to delete item in database.
        public async Task DeleteItem(Guid id)
        {
            var filter = fliterbuilder.Eq(existingitem => existingitem.Id,id);
            await itemCollection.DeleteOneAsync(filter);
            
        }


        //function to getitem by 'id'from database.
        public async Task<Item> GetItem(Guid id)
        {
            var filter = fliterbuilder.Eq(item => item.Id,id);
            return await itemCollection.Find(filter).SingleOrDefaultAsync();
        }
        
        
        //function to get all item from database.
        public async Task<IEnumerable<Item>> GetItems()
        {
            return await itemCollection.Find(new BsonDocument()).ToListAsync();
        }


        //function to update item in database.
        public async Task UpdateItem(Item item)
        {
            var filter = fliterbuilder.Eq(existingitem => existingitem.Id,item.Id);
            await itemCollection.ReplaceOneAsync(filter,item);
        }
    }
}