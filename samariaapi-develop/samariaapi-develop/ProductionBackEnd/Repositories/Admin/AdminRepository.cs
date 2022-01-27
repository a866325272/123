using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductionBackEnd.Interfaces.Repositories.Admin;
using ProductionBackEnd.Models.User;
using ProductionBackEnd.Repositories.Admin.Params;
using ProductionBackEnd.Repositories.Admin.Results;
using ProductionBackEnd.Repositories.Utils.Results;
using ProductionBackEnd.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Repositories.Admin
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AdminRepository(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PaginationResult<AdminAccountListResult>> GetAccountListAsync(GetAccountListParam param)
        {
            var query = _userManager.Users.AsQueryable();

            var total = await query.CountAsync();
            var info = await query.PaginationData(param);

            var result = _mapper.Map<List<AdminAccountListResult>>(info);

            return new PaginationResult<AdminAccountListResult>(result, total, param);
        }

        public async Task<AppUser> GetAccountDetailAsync(string username)
        {
            return await _userManager.Users.Include(r => r.UserRoles).ThenInclude(r => r.Role)
                .FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
