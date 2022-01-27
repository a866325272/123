using ProductionBackEnd.Repositories.Utils.Params;

namespace ProductionBackEnd.Repositories.PlatformNews.Params
{
    public class GetPlatformNewsListParam : PaginationParam
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
