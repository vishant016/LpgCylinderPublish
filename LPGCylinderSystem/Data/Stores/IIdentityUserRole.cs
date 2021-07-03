using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LPGCylinderSystem.Data.Stores
{
   public interface IIdentityUserRole
    {
          List<string> Roles { get; set; }
         void AddRole(string role);
        void RemoveRole(string role);    
    }
}
