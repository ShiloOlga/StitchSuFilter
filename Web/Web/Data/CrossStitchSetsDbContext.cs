using MongoDB.Driver;
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
    }
}