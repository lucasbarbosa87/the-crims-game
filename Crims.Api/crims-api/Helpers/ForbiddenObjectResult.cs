using Crims.Data.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crims_api.Helpers
{
    public class ForbiddenObjectResult : ObjectResult
    {
        public ForbiddenObjectResult(ApiErrorDto value) : base(value)
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }

        public override void OnFormatting(ActionContext context)
        {
            base.OnFormatting(context);
            context.HttpContext.Response.ContentType = "application/json";
        }

        public override void ExecuteResult(ActionContext context)
        {
            base.ExecuteResult(context);
        }
    }
}
