using JustDoIt.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route("api/projects")]
    public class ProjectController : Controller {
        
        #region Properties
        
        private readonly IService _service;
        #endregion Properties

        #region Contructors

        public ProjectController(IService service) {
            _service = service;
        }
        #endregion Contructors

        #region Methods\
        
        [HttpGet("", Name = "GetProjects")]
        public async Task<IActionResult> GetProjects(
            string? title,
            string? description,
            string? pictureURL,
            int? adminID,
            int page = 1,
            int pageSize = 5
        ) {
            try
            {
                
                var result = await _service.GetProjects(
                    title:title,
                    description: description,
                    pictureURL: pictureURL,
                    adminID:adminID,
                    page:page,
                    pageSize: pageSize
                );

                return (result is null) ? NotFound() : Ok(result);
            }
            catch (Exception ex)
            {
                 return BadRequest(ex.Message);
            }
        }
        #endregion Methods
    }
}