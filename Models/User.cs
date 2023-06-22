using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LNRS.Models
{
    [BsonIgnoreExtraElements]
    public class User : BaseCollection
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
