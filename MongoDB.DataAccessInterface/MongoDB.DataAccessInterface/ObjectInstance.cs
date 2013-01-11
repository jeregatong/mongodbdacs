using MongoDB.Driver;

namespace MongoDB.DataAccessInterface
{
    public class ObjectInstance
    {
        private static MongoServer server = MongoServer.Create(Settings.DataSource);
        private static MongoDatabase database = server.GetDatabase(Settings.Database);
        private static volatile ObjectInstance instance;
        private static object syncRoot = new object();

        private ObjectInstance() { }

        public MongoDatabase GetDatabaseName()
        {
            return database;
        }

        public MongoServer GetServerName()
        {
            return server;
        }

        public static ObjectInstance Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ObjectInstance();
                    }
                }
                return instance;
            }
        }
    }
}
