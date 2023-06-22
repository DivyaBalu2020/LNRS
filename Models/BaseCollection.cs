using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LNRS.Models
{
    public class BaseCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
