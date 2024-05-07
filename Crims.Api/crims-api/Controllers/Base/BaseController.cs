using Crims.Core.Failures;
using Crims.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace crims_api.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public string GetUserId()
        {
            return (string)(Request.HttpContext.Items["User"] ?? throw new Exception("Deu erro"));
        }

        [NonAction]
        public void ValidateModelState()
        {
            if (ModelState.IsValid)
            {
                return;
            }
            var list = new List<ErrorValidationDto>();
            ModelState.ToList().ForEach(x =>
            {
                list.Add(new ErrorValidationDto(Field: x.Key, Message: x.Value?.Errors.First().ErrorMessage ?? ""));
            });
            throw new BadRequestFailure(JsonConvert.SerializeObject(list));
        }
    }
}
