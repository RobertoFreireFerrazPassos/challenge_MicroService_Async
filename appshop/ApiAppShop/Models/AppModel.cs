using MongoDB.Bson;

namespace ApiAppShop.Models
{
    public class AppModel
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
