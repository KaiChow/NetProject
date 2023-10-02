using Admin.Model.Dto.User;
using Admin.Model.Other;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Admin.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Admin.Service.Jwt
{
    public class CustomJWTService : ICustomJWTService
    {
        private readonly JWTTokenOptions _jwtOptions;
        public CustomJWTService(IOptionsMonitor<JWTTokenOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.CurrentValue;
        }
        public string GetToken(UserRes user)
        {
            #region 有效荷载，尽量避免敏感信息
            var claims = new[] {
                new Claim("Id",user.Id.ToString()),
                new Claim("NickName",user.NickName),
                new Claim("Name",user.Name),
                new Claim("UserType",user.UserType.ToString()),

            };
            // 需要加密和加密的key 包 Tokens
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 需要引入jwt
            JwtSecurityToken token = new JwtSecurityToken(

                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials

                );
            return new JwtSecurityTokenHandler().WriteToken(token);

            #endregion
        }
    }
}
