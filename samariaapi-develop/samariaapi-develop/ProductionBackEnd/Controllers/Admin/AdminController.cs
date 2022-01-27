using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductionBackEnd.Dtos.Admin.Params;
using ProductionBackEnd.Interfaces.Repositories.Admin;
using ProductionBackEnd.Models.User;
using ProductionBackEnd.Repositories.Admin.Results;
using ProductionBackEnd.Repositories.Utils.Results;
using ProductionBackEnd.Utils;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Controllers.Admin
{
    /// <summary>
    /// 權限功能
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _adminRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AdminController(IAdminRepository adminRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// 取得帳號清單
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("Account")]
        public async Task<ActionResult<PaginationResult<AdminAccountListResult>>> GetAccountListAsync([FromQuery]GetAccountListParam param)
        {
            return await _adminRepository.GetAccountListAsync(
                new Repositories.Admin.Params.GetAccountListParam()
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize
                });
        }

        /// <summary>
        /// 取得帳號資訊
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("Account/{username}")]
        public async Task<ActionResult<AdminAccountDetailResult>> GetAccountDetailAsync([Required] string username)
        {
            var userDetail = await _adminRepository.GetAccountDetailAsync(username);

            if (userDetail == null) return BadRequest("找不到使用者資訊");

            return _mapper.Map<AdminAccountDetailResult>(userDetail);
        }

        /// <summary>
        /// 更新使用者權限
        /// </summary>
        /// <param name="username"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("Account/{username}/Role")]
        public async Task<ActionResult> UpdateUserRoleAsync(string username, [FromBody] UpdateUserRoleParam param)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null) NotFound("找不到使用者");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, param.Roles.Except(userRoles));

            if (!result.Succeeded) return BadRequest("新增權限失敗");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(param.Roles));

            if (!result.Succeeded) return BadRequest("移除權限失敗");

            return Ok(await _userManager.GetRolesAsync(user));
        }
    }
}
