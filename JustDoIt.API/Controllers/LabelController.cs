using JustDoIt.Service.Definitions.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        #region Properties

        private IService _service { get; set; }
        #endregion

        public LabelController(IService service)
        {
            _service = service;
        }

        #region Methods
        [HttpGet("labels", Name = "GetLabels")]
        public async Task<ActionResult> GetLabels(
            string? title,
            string? description,
            int? taskID,
            int page = 1,
            int pageSize = 5
        )
        {

            //TODO(Dario)   sanitize possible input scenarios

            try
            {

                var response = await _service.GetLabels(
                    title: title,
                    description: description,
                    taskID: taskID,
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

        [HttpGet("labels/{id:int}", Name = "GetLabel")]
        public async Task<ActionResult> GetLabel(int id)
        {
            try
            {

                var result = await _service.GetLabel(id);

                return result is null ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("labels/update", Name = "UpdateLabel")]
        public async Task<ActionResult> UpdateLabel([FromBody] Model.Label label)
        {
            try
            {

                if (label == null)
                {
                    return NotFound(label);
                }

                var success = await _service.UpdateLabel(label);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("labels/create", Name = "CreateLabel")]
        public async Task<IActionResult> CreateLabel([FromBody] Model.Label label)
        {
            try
            {
                if (label is null)
                {
                    return NotFound(label);
                }

                var success = await _service.CreateLabel(label);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("labels/delete", Name = "DeleteLabel")]
        public async Task<IActionResult> DeleteLabel(int labelID)
        {
            try
            {
                // if (label is null)
                // {
                //     return NotFound(label);
                // }

                var success = await _service.DeleteLabel(labelID);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion Methods
    }
}
