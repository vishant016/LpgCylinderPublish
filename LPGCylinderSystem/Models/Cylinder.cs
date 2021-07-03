using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPGCylinderSystem.Models
{
    public class Cylinder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Cylinder_Id { get; set; }
        public int Quantity { get; set; }
        public int SubsidyAmount { get; set; }
        public string CylinderType { get; set; }
        public int Price { get; set; }


    }

}
