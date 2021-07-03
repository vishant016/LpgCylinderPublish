using Microsoft.Extensions.Primitives;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPGCylinderSystem.Models
{
    public class Booking
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Booking_Id { get; set; }
        public string PaymentMethod { get; set; }
        public int SubsidyAmount { get; set; }
        public string OrderStatus { get; set; }
        public string DAC { get; set; }
        public string DeliveryBoy_Id { get; set; }
        public string Cylinder_Id { get; set; }
        public int Amount { get; set; }
        public DateTime BookingDate { get; set; }
        public Booking()
        {

        }

        public static implicit operator Booking(StringValues v)
        {
            throw new NotImplementedException();
        }
    }
}
