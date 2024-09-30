using JustDoIt.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [Authorize, ApiController]
    [Route(ApiRoutes.Comments.Controller)]
    public class CommentsController : Controller
    {
        [HttpPost(ApiRoutes.Comments.Create)]
        public async Task<IActionResult> PostComment()
        {
            return Ok();
        }
    }
}
