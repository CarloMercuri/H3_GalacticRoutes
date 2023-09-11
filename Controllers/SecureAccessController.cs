using Galactic.Data.Interfaces;
using Galactic.Processing.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Net;
using System.Reflection;

namespace Galactic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SecureAccessController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var header = context.HttpContext.Request.Headers;
                if (header.TryGetValue("Request-Token", out StringValues requestTokenValue))
                {
                    
                }
                else
                {
                   // context.Result = new UnauthorizedObjectResult("Missing correct Request Token");
                }
            }
            catch (Exception ex)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
            finally
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
