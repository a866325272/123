using ProductionBackEnd.Models.User;
using ProductionBackEnd.Repositories.Admin.Params;
using ProductionBackEnd.Repositories.Admin.Results;
using ProductionBackEnd.Repositories.Utils.Results;
using System.Threading.Tasks;

namespace ProductionBackEnd.Interfaces.Repositories.Admin
{
    public interface IAdminRepository
    {
        /// <summary>
        /// 取得帳號清單
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<PaginationResult<AdminAccountListResult>> GetAccountListAsync(GetAccountListParam param);
        /// <summary>
        /// 取得帳號資訊
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<AppUser> GetAccountDetailAsync(string username);
    }
}
