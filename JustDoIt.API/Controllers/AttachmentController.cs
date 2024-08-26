using JustDoIt.Model;
using JustDoIt.Service.Definitions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        #region Properties

        private IService _service { get; set; }
        #endregion

        public AttachmentController(IService service)
        {
            _service = service;
        }

        #region Methods

        [HttpGet("attachments", Name = "GetAttachments")]
        public async Task<ActionResult> GetAttachments(
            string? filepath,
            int? taskID,
            int? projectID,
            int page = 1,
            int pageSize = 5)
        {

            //TODO(Dario)   sanitize possible input scenarios

            try
            {

                var response = await _service.GetAttachments(
                    filepath: filepath,
                    taskID: taskID,
                    projectID: projectID,
                    page: page,
                    pageSize: pageSize
                );

                return response is null ? NotFound() : Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("attachments/{id:int}", Name = "GetAttachment")]
        public async Task<ActionResult> GetAttachment(int id)
        {
            try
            {

                var result = await _service.GetAttachment(id);

                return result is null ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("attachments/update", Name = "UpdateAttachments")]
        public async Task<ActionResult> UpdateAttachments([FromBody] Attachment attachment)
        {
            try
            {

                if (attachment == null)
                {
                    return NotFound(attachment);
                }

                var success = await _service.UpdateAttachment(attachment);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("attachments/create", Name = "CreateAttachment")]
        public async Task<IActionResult> CreateComment([FromBody] Attachment attachment)
        {
            try
            {
                if (attachment is null)
                {
                    return NotFound(attachment);
                }

                var success = await _service.CreateAttachment(attachment);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("attachments/delete", Name = "DeleteAttachment")]
        public async Task<IActionResult> DeleteAttachment(int attachmentID)
        {
            try
            {
                // if (attachment is null)
                // {
                //     return NotFound(attachment);
                // }

                var success = await _service.DeleteAttachment(attachmentID);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
#endregion 
    }
}
