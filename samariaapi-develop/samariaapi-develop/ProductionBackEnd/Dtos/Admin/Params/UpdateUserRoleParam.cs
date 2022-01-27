using System.ComponentModel.DataAnnotations;

namespace ProductionBackEnd.Dtos.Admin.Params
{
    public class UpdateUserRoleParam
    {
        public string[] Roles { get; set; }
    }
}
