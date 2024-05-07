using Crims.Data.Dtos;
using Crims.Domain.Services;
using crims_api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace crims_api.Controllers
{
    [Route("/[controller]")]
    public class UserController(IUserService userService) : BaseController
    {
        private readonly IUserService userService = userService;

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RegisterDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            ValidateModelState();
            var savedUser = await userService.Register(register);
            return Ok(savedUser);
        }
    }
}
