﻿using System.Reflection.Emit;
using JustDoIt.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route("api/tasks")]
    public class TaskController : Controller
    {
        #region Properties

        private IService _service { get; set; }
        #endregion Properties

        #region Constructors

        public TaskController(IService service)
        {
            _service = service;
        }
        #endregion Constructors

        #region Methods

        #region Tasks

        [HttpGet("", Name = "GetTasks")]
        public async Task<ActionResult> GetTasks(
            string? title,
            string? description,
            string? pictureURL,
            DateTime? deadlineStart,
            DateTime? deadlineEnd,
            string? state,
            int? adminID,
            int? projectID,
            int page = 1,
            int pageSize = 5
        ) { 

            //TODO(Dario)   sanitize possible input scenarios
            
            try {

            var response = await _service.GetTasks(
                title: title,
                description: description,
                pictureURL: pictureURL,
                deadlineStart: deadlineStart,
                deadlineEnd: deadlineEnd,
                state: state,
                adminID: adminID,
                projectID: projectID,
                page: page,
                pageSize: pageSize
            );

            return response is null ? NotFound() : Ok(response);
            } catch (Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetTask")]
        public async Task<ActionResult> GetTask(int id) {
            try {

            var task = await _service.GetTask(id);
            
            return task is null ? NotFound() : Ok(task);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update", Name = "UpdateTask")]
        public async Task<ActionResult> UpdateTask([FromBody]Model.Task task) {
            try {

                if (task == null) {
                    return NotFound();
                }

                var success = await _service.UpdateTask(task);
                return Ok(success);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create", Name = "CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] Model.Task task) {
            try
            {
                if(task is null) {
                    return NotFound();
                }

                var success = await _service.CreateTask(task);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete", Name = "DeleteTask")]
        public async Task<IActionResult> DeleteTask(Model.Task task)
        {
            try
            {
                if(task is null) {
                    return NotFound(task);
                }

                var success = await _service.DeleteTask(task);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion Tasks

        #region Labels
        [HttpGet("labels", Name = "GetLabels")]
        public async Task<ActionResult> GetLabels(
            string? title,
            string? description,
            int? taskID,
            int page = 1,
            int pageSize = 5
        ) { 

            //TODO(Dario)   sanitize possible input scenarios
            
            try {

            var response = await _service.GetLabels(
                title: title,
                description: description,
                taskID: taskID,
                page: page,
                pageSize: pageSize
            );

            return response is null ? NotFound() : Ok(response);
            } catch (Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpGet("labels/{id:int}", Name = "GetLabel")]
        public async Task<ActionResult> GetLabel(int id) {
            try {

            var result = await _service.GetLabel(id);
            
            return result is null ? NotFound() : Ok(result);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("labels/update", Name = "UpdateLabel")]
        public async Task<ActionResult> UpdateLabel([FromBody]Model.Label label) {
            try {

                if (label == null) {
                    return NotFound(label);
                }

                var success = await _service.UpdateLabel(label);
                return Ok(success);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("labels/create", Name = "CreateLabel")]
        public async Task<IActionResult> CreateLabel([FromBody] Model.Label label) {
            try
            {
                if(label is null) {
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
        #endregion Labels

        #region Comments
        [HttpGet("labels", Name = "GetLabels")]
        public async Task<ActionResult> GetLabels(
            string? title,
            string? description,
            int? taskID,
            int page = 1,
            int pageSize = 5
        ) { 

            //TODO(Dario)   sanitize possible input scenarios
            
            try {

            var response = await _service.GetLabels(
                title: title,
                description: description,
                taskID: taskID,
                page: page,
                pageSize: pageSize
            );

            return response is null ? NotFound() : Ok(response);
            } catch (Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpGet("labels/{id:int}", Name = "GetLabel")]
        public async Task<ActionResult> GetLabel(int id) {
            try {

            var result = await _service.GetLabel(id);
            
            return result is null ? NotFound() : Ok(result);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("labels/update", Name = "UpdateLabel")]
        public async Task<ActionResult> UpdateLabel([FromBody]Model.Label label) {
            try {

                if (label == null) {
                    return NotFound(label);
                }

                var success = await _service.UpdateLabel(label);
                return Ok(success);
            } catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("labels/create", Name = "CreateLabel")]
        public async Task<IActionResult> CreateLabel([FromBody] Model.Label label) {
            try
            {
                if(label is null) {
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
        #endregion Comments

        #endregion Methods
    }
}