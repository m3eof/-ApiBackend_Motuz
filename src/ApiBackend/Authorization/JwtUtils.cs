using ApiBackend.Entities;
using ApiBackend.Helpers;
using ApiBackend.Models;
using ApiBackend.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private readonly recensiiContext _wrapper;
        private readonly AppSettings _appSettings;

        public JwtUtils(recensiiContext wrapper, IOptions<AppSettings> appSettings)
        {
            _wrapper = wrapper;
            _appSettings = appSettings.Value;
        }

        public string GenerateJwtToken(User account)
        {
           var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.UsersId.ToString()) , new Claim(ClaimTypes.Role, account.Role.ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<RefreshToken> GenerateRefreshToken(string ipAddress)
        {
            
               
                var refreshToken = new RefreshToken
                {
                    Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)), 
                    Expires = DateTime.UtcNow.AddDays(7), 
                    Created = DateTime.UtcNow, 
                    CreatedByIp = ipAddress 
                };

                
                var tokenIsUnique= await _wrapper.Users.AnyAsync(a =>
                    a.RefreshTokens.Any(t => t.Token == refreshToken.Token)); 

                
                if (!tokenIsUnique)
                {
                    return await GenerateRefreshToken(ipAddress);
                }

                return refreshToken;
            
        }

        public int? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return accountId;
            }
            catch
            {
                return null;
            }

        }
    }
}
