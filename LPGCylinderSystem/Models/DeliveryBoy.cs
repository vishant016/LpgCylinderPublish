using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPGCylinderSystem.Models
{
    public class DeliveryBoy
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DeliveryBoy_Id { get; set; }
        public string DeliveryBoyName { get; set; }
        public string Password { get; set; }

    }

}
