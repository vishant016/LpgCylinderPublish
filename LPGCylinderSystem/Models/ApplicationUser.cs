using LPGCylinderSystem.Data.Stores;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



//git testing
//git testing 2
namespace LPGCylinderSystem.Models
{
    public class ApplicationUser: IdentityUser<ObjectId>,IIdentityUserRole
    {
        [PersonalData]
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeliveryBoy { get; set; }
        public string MobileNo { get; set; }
        public string Connection_Id { get; set; }
        public string AadharNo { get; set; }
        public string AccountNo { get; set; }
        public string IFSC { get; set; }
        public bool SubsidyOpted { get; set; }
        public string PreferedDeliveryTime { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string Agency_Id { get; set; }
        public string Income { get; set; }


        public  List<string> Roles { get; set; }
        public   List<Booking> Bookings { get; set; }
        public  List<ServiceRequest> ServiceRequests { get; set; }
        public  List<Complaint> Complaints { get; set; }


        public ApplicationUser()
        {
          Roles = new List<string>();
        //    Bookings = new List<Booking>();
        //    ServiceRequests = new List<ServiceRequest>();
        //    Complaints = new List<Complaint>();
        }

        public virtual void AddRole(string role)
        {
            Roles.Add(role);
        }

        public virtual void RemoveRole(string role)
        {
            Roles.Remove(role);
        }

       
    }
}
