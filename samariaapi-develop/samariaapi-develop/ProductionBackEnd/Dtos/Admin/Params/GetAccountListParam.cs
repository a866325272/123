using ProductionBackEnd.Dtos.Utils.Params;

namespace ProductionBackEnd.Dtos.Admin.Params
{
    public class GetAccountListParam : PaginationParam
    {
        public string RealName { get; set; }
    }
}
