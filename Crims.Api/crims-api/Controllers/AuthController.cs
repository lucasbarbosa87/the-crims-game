using Crims.Data.Dtos;
using Crims.Domain.Services;
using crims_api.Attributes;
using crims_api.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace crims_api.Controllers
{
    [Route("/[controller]")]
    public class AuthController(IUserService userService, IJwtService jwtService) : BaseController
    {
        private readonly IUserService userService = userService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await userService.Login(loginDto);
            var userToken = await jwtService.CreateJwtToken(user);
            user.AccessToken = userToken.AccessToken;
            user.RefreshToken = userToken.RefreshToken;
            return Ok(user);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            string userId = GetUserId();
            await jwtService.InvalidateToken(userId);

            return NoContent();
        }
    }
}
