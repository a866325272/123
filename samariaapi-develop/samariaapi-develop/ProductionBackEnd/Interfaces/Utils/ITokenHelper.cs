using ProductionBackEnd.Dtos.Utils.Results;
using ProductionBackEnd.Models.User;
using System.Threading.Tasks;

namespace ProductionBackEnd.Interfaces.Utils
{
    public interface ITokenHelper
    {
        Task<TokenResult> CreateTokenAsync(AppUser appUser);
    }
}
