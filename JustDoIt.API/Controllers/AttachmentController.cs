using JustDoIt.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [Authorize]
    [ApiController, Route(ApiRoutes.Attachments.Controller)]
    public class AttachmentController : Controller
    {

    }
}
