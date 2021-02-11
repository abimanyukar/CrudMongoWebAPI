using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrudWithMongoDBWebAPI.Models
{
    public class Employee
    {
        //[BsonId]
        //[BsonIgnoreIfNull]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Age { get; set; }
        public string Department { get; set; }
        public string Technology { get; set; }
    }
}
