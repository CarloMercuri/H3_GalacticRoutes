using Galactic.Processing.Interfaces;
using Galactic.Processing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace Galactic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoutesController : Controller
    {
        IPublicRequestsProcessor _processor;

        public RoutesController(IPublicRequestsProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet]
        [Route("GetRoute")]
        public IActionResult GetRoute(string routeName)
        {
            try
            {
                string tokenActual = "";
                if (Request.Headers.TryGetValue("RouteRequestToken", out StringValues requestTokenValues))
                {
                    tokenActual = requestTokenValues;
                }
                else
                {
                    return BadRequest("Missing authentication token.");
                }

                RouteRequestOperation result = _processor.GetRoute(routeName, tokenActual);

                if (result.Success)
                {
                    return Ok(result.Route);
                }
                else
                {
                    return BadRequest(result.Message);
                }

            }
            catch (Exception ex)
            {
                return base.StatusCode((int)HttpStatusCode.InternalServerError, "Error");
            }
        }
    }
}
