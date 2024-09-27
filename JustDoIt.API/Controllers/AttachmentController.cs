using JustDoIt.API.Contracts;
using JustDoIt.Model.Requests.Abstractions;
using JustDoIt.Model.Requests.Attachments;
using JustDoIt.Service.Abstractions.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

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
        [HttpPost("test")]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var ext = Path.GetExtension(formFile.FileName).ToLowerInvariant();
                    var filePath = $"{Directory.GetCurrentDirectory()}\\Images\\{formFile.FileName}";

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }

        [HttpPost("test-single")]
        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            long size = file.Length;

            if (file.Length > 0)
            {
                var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
                var filePath = $"{Directory.GetCurrentDirectory()}\\Images\\{file.FileName}";

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = 1, size });
        }

        [AllowAnonymous]
        [HttpGet("get-file")]
        public IActionResult Get(string filepath)
        {
            try
            {
                string ext = Path.GetExtension(filepath).ToLowerInvariant();
                Byte[] b = System.IO.File.ReadAllBytes(filepath);   // You can use your own method over here.         
                var fileProvider = new FileExtensionContentTypeProvider();
                // Figures out what the content type should be based on the file name.  
                if (!fileProvider.TryGetContentType(ext, out string contentType))
                {
                    throw new ArgumentOutOfRangeException($"Unable to find Content Type for file name {ext}.");
                }
                return File(b, contentType);

            }
            catch (Exception)
            {

            }
            return BadRequest();
        }

        [HttpPut("task-attachments")]
        public async Task<IActionResult> UpdateTaskAttachments([FromForm] UpdateTaskAttachmentsRequest request)
        {
            var response = await _service.UpdateTaskAttachments(request);
            return Ok(response);
        }
        #endregion
    }
}
