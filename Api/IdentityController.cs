using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api
{
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var claims = User.Claims.Select(c => new {c.Type, c.Value}).ToList();
            return new JsonResult(claims);
        }

        [HttpGet]
        [Authorize(policy:"bob")]
        [Route("Hidden")]
        public IActionResult Hidden()
        {
            return new JsonResult(new {message = "Hi Bob"});
        }
    }
}
