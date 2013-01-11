using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoDB.DataAccessInterface
{
    public class DataManipulation<T> where T : class
    {
        public static void Add(T value)
        {
            Helper.GetCustomDocument<T>(CurrentDatabase, CurrentCollection)
                .Insert<BsonDocument>(value.ToBsonDocument());
        }

        public static void Edit(ObjectId id, T value)
        {
            var builder = new UpdateBuilder();

            foreach (var item in value.ToBsonDocument())
                if (item.Name != "_id")
                    builder.Set(item.Name, item.Value);

            Helper.GetBsonDocument(CurrentDatabase, CurrentCollection)
                .Update(Query.EQ("_id", id), builder);
        }

        public static void Erase()
        {
            Helper.GetBsonDocument(CurrentDatabase, CurrentCollection)
                .RemoveAll();
        }

        public static void Erase(ObjectId id)
        {
            Helper.GetBsonDocument(CurrentDatabase, CurrentCollection)
                .Remove(Query.EQ("_id", id));
        }

        public static List<T> Get()
        {
            return BsonSerializer.Deserialize<List<T>>(Helper.GetBsonDocument(CurrentDatabase, CurrentCollection)
                    .FindAll()
                        .ToJson<MongoCursor<BsonDocument>>());
        }

        public static T Get(ObjectId id)
        {
            return BsonSerializer.Deserialize<T>(Helper.GetBsonDocument(CurrentDatabase, CurrentCollection)
                        .FindOne(Query.EQ("_id", id))
                            .ToJson<BsonDocument>());
        }

        private static string CurrentCollection
        {
            get
            {
                string discriminator = string.Empty;
                foreach (dynamic attr in typeof(T).GetCustomAttributes(false))
                    discriminator = (string)attr.Discriminator;

                return discriminator;
            }
        }

        private static MongoDatabase CurrentDatabase
        {
            get
            {                
                return ObjectInstance.Instance.GetDatabaseName(); 
            }
        }
    }
}
