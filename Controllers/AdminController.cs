using Galactic.Processing.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Galactic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : SecureAccessController
    {
        ISecureRequestsProcessor _processor;
        public AdminController(ISecureRequestsProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet]
        [Route("CreateToken")]
        public IActionResult CreateToken(string personName, bool isCaptain)
        {
            try
            {
                string plainText = _processor.CreateToken(personName, isCaptain);
                return Ok(plainText);

            }
            catch (Exception)
            {
                return base.StatusCode((int)HttpStatusCode.InternalServerError, "Internal error");
            }
        }

        [HttpGet]
        [Route("CreateTokenForCadet")]
        public IActionResult CreateTokenForCadet()
        {
            try
            {
                return Ok();

            }
            catch (Exception)
            {
                return base.StatusCode((int)HttpStatusCode.InternalServerError, "Internal error");
            }
        }
    }
}
