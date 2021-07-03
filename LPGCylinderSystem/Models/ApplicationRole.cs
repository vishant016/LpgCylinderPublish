using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//git now
namespace LPGCylinderSystem.Models
{
    public class ApplicationRole : IdentityRole<ObjectId>
    {
        public override ObjectId Id { get; set; }

        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName)
            : this()
        {
            Name = roleName;
        }
    }
}
