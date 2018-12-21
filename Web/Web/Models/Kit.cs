﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Web.Models
{
    public enum KitType
    {
        ManufacturerKit,
        DesignerPattern
    }

    public class Kit
    {
        [JsonIgnore]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Manufacturer { get; set; }
        public string Item { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public KitType KitType { get; set; }
    }
}