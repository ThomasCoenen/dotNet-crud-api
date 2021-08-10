namespace Catalog.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string ConnectionString
        {
            //grab the connection string
            get
            {
                return $"mongodb://{Host}:{Port}";
            }
        }
    }
}