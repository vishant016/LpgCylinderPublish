using LPGCylinderSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LPGCylinderSystem.Data.Stores
{
    public interface Repository<TUser> : IDisposable where TUser : ApplicationUser
    {
        Task<TUser> FindByConnectionIdAsync(string ConnectionId);

    }
}
