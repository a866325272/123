using ProductionBackEnd.Models.PlatformNews;
using ProductionBackEnd.Repositories.PlatformNews.Params;
using ProductionBackEnd.Repositories.PlatformNews.Results;
using ProductionBackEnd.Repositories.Utils.Results;
using System.Threading.Tasks;

namespace ProductionBackEnd.Interfaces.Repositories.PlatformNews
{
    public interface IPlatformNewsRepository
    {
        /// <summary>
        /// 取得平台消息列表
        /// </summary>
        /// <returns></returns>
        Task<PaginationResult<PlatformNewsListResult>> GetPlatformNewsListAsync(GetPlatformNewsListParam param);
        /// <summary>
        /// 取得平台消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PlatformNewsModel> GetPlatformNewsByIdAsync(int id);
        /// <summary>
        /// 新增平台消息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task InsertNewsAsync(PlatformNewsModel param);
        /// <summary>
        /// 更新平台消息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        void UpdatePlatformNews(PlatformNewsModel param);
        Task<bool> SaveAllAsync();
    }
}
