using ProductionBackEnd.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Interfaces.Repositories.User
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByIdAsync(string id);
        Task<bool> SaveAllAsync();
    }
}
