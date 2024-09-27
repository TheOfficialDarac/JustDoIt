using JustDoIt.API.Contracts;
using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Attachments;
using JustDoIt.Service.Abstractions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [Authorize]
    [ApiController, Route(ApiRoutes.Attachments.Controller)]
    public class AttachmentController(IAttachmentService service) : Controller
    {
        #region Properties

        private readonly IAttachmentService _service = service;
        #endregion Properties

        #region Methods

        [HttpGet(ApiRoutes.Attachments.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] GetAttachmentRequest request)
        {
            var response = await _service.GetAll(request);

            return response.Result.IsSuccess ? Ok(new { data = response.ListOfData, result = response.Result }) : BadRequest(new { data = response.ListOfData, result = response.Result });
        }

        [HttpGet(ApiRoutes.Attachments.Get)]
        public async Task<IActionResult> GetSingle([FromQuery] GetSingleItemRequest request)
        {
            var response = await _service.GetSingle(request);

            return response.Result.IsSuccess ? Ok(new { data = response.Data, result = response.Result }) : NotFound(new { data = response.Data, result = response.Result });
        }

        [HttpPut(ApiRoutes.Attachments.Update)]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateAttachmentRequest request)
        {
            //if (request is null) return BadRequest();

            var response = await _service.Update(request);

            return response.Result.IsSuccess ? Ok(new { data = response.Data, result = response.Result }) : Ok(new { data = response.Data, result = response.Result });
        }

        [HttpPost(ApiRoutes.Attachments.Create)]
        public async Task<IActionResult> CreateTask([FromBody] CreateAttachmentRequest request)
        {
            var response = await _service.Create(request);

            if (response.Result.IsFailure) return BadRequest(response.Result);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = $"{baseUrl}/{ApiRoutes.Attachments.Get.Replace("{taskId}", response.Data!.Id.ToString())}";
            return Created(locationUrl, response);
        }

        [HttpDelete(ApiRoutes.Attachments.Delete)]
        public async Task<IActionResult> DeleteTask([FromBody] GetSingleItemRequest request)
        {
            var response = await _service.Delete(request);
            return response.Result.IsSuccess ? NoContent() : NotFound(response);
        }
        #endregion
    }
}
