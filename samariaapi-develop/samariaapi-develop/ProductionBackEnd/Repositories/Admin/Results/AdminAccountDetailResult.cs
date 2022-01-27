using System.Collections.Generic;

namespace ProductionBackEnd.Repositories.Admin.Results
{
    public class AdminAccountDetailResult
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
