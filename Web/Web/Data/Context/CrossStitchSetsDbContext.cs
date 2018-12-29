using MongoDB.Driver;

namespace Web.Data.Context
{
    public class CrossStitchSetsDbContext
    {
        private readonly IMongoDatabase _database;

        public CrossStitchSetsDbContext(string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);
            var dbClient = new MongoClient(connectionString);
            _database = dbClient.GetDatabase(connection.DatabaseName);
        }
        //public IMongoCollection<KitModel> Sets => _database.GetCollection<KitModel>("CrossStitchKits");
        //public IMongoCollection<Fabric> Fabrics => _database.GetCollection<Fabric>("Fabric");
        //public IMongoCollection<Palette> Palettes => _database.GetCollection<Palette>("Palettes");
        //public IMongoCollection<Floss> Flosses => _database.GetCollection<Floss>("Flosses");
    }
}