using Crims.Data.Dtos;
using Crims.Domain.Services;
using crims_api.Attributes;
using crims_api.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace crims_api.Controllers
{
    [Route("[controller]")]
    public class EstablishmentController(IEstablishmentService establishmentService) : BaseController
    {
        private readonly IEstablishmentService establishmentService = establishmentService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await establishmentService.GetAll();
            return Ok(list);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserEstablishment()
        {
            var userId = GetUserId();
            var list = await establishmentService.GetUserEstablishment(userId);
            return Ok(list);
        }

        [RoleAuthorization("Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EstablishmentDto>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<IActionResult> Register([FromBody] EstablishmentDto establishmentDto)
        {

            var saved = await establishmentService.Register(establishmentDto);
            return Created(saved.Id.ToString(), saved);
        }

        [RoleAuthorization("Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<IActionResult> Update(string id, [FromBody] EstablishmentDto establishment)
        {
            ValidateModelState();
            var saved = await establishmentService.Update(id, establishment);
            return Ok(saved);
        }


        [RoleAuthorization("Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorDto))]
        public async Task<IActionResult> Delete(string id)
        {
            await establishmentService.Delete(id);
            return NoContent();
        }
    }
}
