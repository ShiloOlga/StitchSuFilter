using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Web.Domain
{
    public class Floss
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string FlossId { get; set; }
        public string Name { get; set; }
        public string Set { get; set; }
        public string Rgb { get; set; }
        public string Material { get; set; }
        public int Length { get; set; }
        public ObjectId PaletteId { get; set; }

    }
}