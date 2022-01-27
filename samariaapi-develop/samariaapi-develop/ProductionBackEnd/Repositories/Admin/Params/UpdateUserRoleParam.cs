using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Repositories.Admin.Params
{
    public class UpdateUserRoleParam
    {
        public string UserName { get; set; }
        public string[] Roles { get; set; }
    }
}
