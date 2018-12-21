using MongoDB.Driver;
using Web.Domain;
using Web.Models;

namespace Web.Data
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
        public IMongoCollection<Kit> Sets => _database.GetCollection<Kit>("CrossStitchKits");
        public IMongoCollection<Fabric> Fabrics => _database.GetCollection<Fabric>("Fabrics");
        public IMongoCollection<Palette> Palettes => _database.GetCollection<Palette>("Palettes");
        public IMongoCollection<Floss> Flosses => _database.GetCollection<Floss>("Flosses");
    }
}