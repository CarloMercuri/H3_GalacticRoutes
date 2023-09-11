using Galactic.Processing.Interfaces;
using Galactic.Processing.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetRoute(string routeName, string token)
        {
            try
            {
                RouteRequestOperation result = _processor.GetRoute(routeName, token);

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
