using Microsoft.AspNetCore.Identity;
using ProductionBackEnd.Models.PlatformNews;
using System.Collections.Generic;

namespace ProductionBackEnd.Models.User
{
    public class AppUser : IdentityUser
    {
        public string RealName { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
