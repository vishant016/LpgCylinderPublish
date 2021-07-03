using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LPGCylinderSystem.Models
{
    public class Netbanking
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        [Required]
        [StringLength(10)]
        
        public string Username { get; set; }

        [Required]
        [StringLength(12)]
        public string Password { get; set; }

        [Required]
        public int balance { get; set; }
        [Required]

        public string Bank { get; set; }
    }
}
