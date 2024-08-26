using JustDoIt.Model;
using JustDoIt.Service.Definitions.Common;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CommentController : Controller
    {
        #region Properties

        private IService _service { get; set; }
        #endregion Properties

        #region Constructors

        public CommentController(IService service)
        {
            _service = service;
        }
        #endregion Constructors

        #region Methods

        [HttpGet("comments", Name = "GetComments")]
        public async Task<ActionResult> GetComments(
            string? text,
            int? taskID,
            string? userID,
            int page = 1,
            int pageSize = 5
        )
        {

            //TODO(Dario)   sanitize possible input scenarios

            try
            {

                var response = await _service.GetComments(
                    text: text,
                    taskID: taskID,
                    userID: userID,
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

        [HttpGet("comments/{id:int}", Name = "GetComment")]
        public async Task<ActionResult> GetComment(int id)
        {
            try
            {

                var result = await _service.GetComment(id);

                return result is null ? NotFound() : Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("comments/update", Name = "UpdateComment")]
        public async Task<ActionResult> UpdateComment([FromBody] Comment comment)
        {
            try
            {

                if (comment == null)
                {
                    return NotFound(comment);
                }

                var success = await _service.UpdateComment(comment);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("comments/create", Name = "CreateComment")]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment)
        {
            try
            {
                if (comment is null)
                {
                    return NotFound(comment);
                }

                var success = await _service.CreateComment(comment);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("comments/delete", Name = "DeleteComment")]
        public async Task<IActionResult> DeleteComment(int commentID)
        {
            try
            {
                // if (comment is null)
                // {
                //     return NotFound(comment);
                // }

                var success = await _service.DeleteComment(commentID);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion Comments
    }
}
