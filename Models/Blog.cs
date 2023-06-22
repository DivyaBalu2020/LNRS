using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LNRS.Models
{
    [BsonIgnoreExtraElements]
    public class Blog : BaseCollection
    {        
        public User AuthorId { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
    }  

}
