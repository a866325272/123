using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductionBackEnd.Dtos.Utils.Results;
using ProductionBackEnd.Interfaces.Utils;
using ProductionBackEnd.Models.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductionBackEnd.Utils
{
    public class TokenHelper : ITokenHelper
    {
        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;
        private readonly UserManager<AppUser> _userManager;

        public TokenHelper(IConfiguration config, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:TokenKey"]));
            _issuer = config["JwtSettings:Issuer"];
        }

        public async Task<TokenResult> CreateTokenAsync(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, appUser.Id.ToString()),
                // 有設定sub可以在寫授權那邊設定將sub給Name
                new Claim(JwtRegisteredClaimNames.Sub, appUser.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName, appUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // 新增角色至Token
            var roles = await _userManager.GetRolesAsync(appUser);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var Expiration = DateTime.Now.AddDays(7);
            var crdes = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _issuer,
                Subject = new ClaimsIdentity(claims),
                Expires = Expiration,
                SigningCredentials = crdes
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResult
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = Expiration
            };
        }
    }
}
