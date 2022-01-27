using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionBackEnd.Dtos.User.Params;
using ProductionBackEnd.Dtos.User.Results;
using ProductionBackEnd.Dtos.Utils.Results;
using ProductionBackEnd.Extensions;
using ProductionBackEnd.Interfaces.Utils;
using ProductionBackEnd.Models.User;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Controllers.User
{
    /// <summary>
    /// 帳號相關
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, IMapper mapper, ITokenHelper tokenHelper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
        }

        /// <summary>
        /// 使用者註冊
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<TokenResult>> Register(RegisterParam param)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == param.UserName))
                return BadRequest("已存在相同帳號");
                
            if (!await _roleManager.Roles.AnyAsync(x => x.Name == param.Role))
                return BadRequest("錯誤的角色權限");

            if (param.Password.Length > 20) return BadRequest("密碼長度超過20個字元");

            var user = _mapper.Map<AppUser>(param);
            var result = await _userManager.CreateAsync(user, param.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, param.Role);

            return await _tokenHelper.CreateTokenAsync(user);
        }

        /// <summary>
        /// 使用者登入
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<TokenResult>> Login(LoginParam param)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == param.UserName);

            if (user == null) return Unauthorized("該帳號不存在");

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, param.Password, false);

            if (!result.Succeeded) return Unauthorized("帳號密碼錯誤");

            return await _tokenHelper.CreateTokenAsync(user);
        }

        /// <summary>
        /// 更新使用者資訊
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateAccountAsync(UpdateAccountParam param)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == User.GetUserId());
            if (user == null) return NotFound();

            user.RealName = param.RealName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return NoContent();
        }

        /// <summary>
        /// 更新使用者密碼
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("Password")]
        public async Task<ActionResult> UpdateAccountPasswordAsync(UpdateAccountPasswordParam param)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == User.GetUserId());
            if (user == null) return NotFound();

            var result = await _userManager.ChangePasswordAsync(user, param.OldPassword, param.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return NoContent();
        }

        /// <summary>
        /// 刪除帳號
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteAccountAsync()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == User.GetUserId());
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return NoContent();
        }

        /// <summary>
        /// 取得使用者資訊
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AccountDetailResult>> GetAccountAsync()
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == User.GetUserId());
            if (user == null) return NotFound();

            return _mapper.Map<AccountDetailResult>(user);
        }
    }
}
