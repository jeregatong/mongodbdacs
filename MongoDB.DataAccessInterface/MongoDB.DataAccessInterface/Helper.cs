using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB.DataAccessInterface
{
    public class Helper
    {
        public static MongoCollection<BsonDocument> GetBsonDocument(MongoDatabase db, string name)
        {
            return db.GetCollection<BsonDocument>(name);
        }

        public static MongoCollection<T> GetCustomDocument<T>(MongoDatabase db, string name) where T : class
        {
            return db.GetCollection<T>(name);
        }
    }
}
