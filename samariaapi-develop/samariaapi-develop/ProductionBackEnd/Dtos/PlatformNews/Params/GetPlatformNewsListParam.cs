using ProductionBackEnd.Dtos.Utils.Params;

namespace ProductionBackEnd.Dtos.PlatformNews.Params
{
    public class GetPlatformNewsListParam : PaginationParam
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
