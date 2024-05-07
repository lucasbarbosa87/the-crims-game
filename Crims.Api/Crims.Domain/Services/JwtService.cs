using Crims.Data.Repository;
using Crims.Data.Dtos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Crims.Data.Entities;
using System.Security.Cryptography;
using static System.Collections.Specialized.BitVector32;
using Crims.Core.Failures;

namespace Crims.Domain.Services
{
    public interface IJwtService
    {
        Task<string> ValidateJwt(string token);
        Task<TokenDto> CreateJwtToken(UserDto userId);
        Task InvalidateToken(string userId);
    }

    internal class JwtService(AuthConfiguration configuration, TokenRepository tokenRepository) : IJwtService
    {
        private readonly string claimKey = "UserId";
        public async Task<string?> ValidateJwt(string token)
        {
            if (token.IsNullOrEmpty())
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration.Secret);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidAudience = configuration.ValidAudience,
                ValidIssuer = configuration.ValidAudience,

                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "unique_name").Value;

            var user = await tokenRepository.GetItem(where => where.UserId.ToString() == userId);
            if (user == null)
            {
                throw new InvalidTokenFailure();
            }
            var compare = DateTime.UtcNow.CompareTo(user.ExpiresAt);
            if (compare < 0)
            {
                throw new InvalidTokenFailure();
            }
            return userId.ToString();
        }
        public async Task<TokenDto> CreateJwtToken(UserDto user)
        {
            await CheckIfHasValidToken(user.Id);
            var accessToken = GenerateJwtToken(user);
            var refreshToken = await GetUniqueRefreshToken();
            await tokenRepository.Add(new TokenEntity()
            {
                UserId = user.Id,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddMinutes(configuration.ExpiresIn)
            });

            return new TokenDto(accessToken, refreshToken);
        }
        public async Task InvalidateToken(string userId)
        {
            var token = await tokenRepository.GetItem(where => where.UserId == Guid.Parse(userId)) ?? throw new InvalidTokenFailure();
            await tokenRepository.Delete(token);
            return;
        }
        private async Task CheckIfHasValidToken(Guid userId)
        {
            var token = await tokenRepository.GetItem(where => where.UserId == userId);
            if(token == null)
            {
                return; 
            }
            await tokenRepository.Delete(token);
        }

        private string GenerateJwtToken(UserDto user)
        {
            var userRole = user.UserRole.Name;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Id.ToString()), new Claim(ClaimTypes.Role, userRole) }),
                Expires = DateTime.UtcNow.AddMinutes(configuration.ExpiresIn),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<string> GetUniqueRefreshToken()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var dbToken = await tokenRepository.GetItem(where => where.RefreshToken == token);
            if (dbToken == null)
            {
                return token;
            }
            return await GetUniqueRefreshToken();
        }
    }


}
