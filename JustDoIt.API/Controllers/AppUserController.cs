using System.Drawing;
using System.Net;
using System.Net.Mime;
using Azure.Core;
using JustDoIt.API.ViewModel;
using JustDoIt.Model;
using JustDoIt.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JustDoIt.API.Controllers
{
    [ApiController, Route("api/user")]
    public class AppUserController : Controller
    {
        #region Properties

        private IService _service { get; set; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        #endregion Properties

        #region Constructors

        public AppUserController(IService service, UserManager<AppUser> userManager,
                                  SignInManager<AppUser> signInManager)
        {
            _service = service;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        #endregion Constructors

        #region Methods

        #region testing

        [HttpPost("logout"), Authorize]
        public async Task<ActionResult> Logout(SignInManager<AppUser> signInManager)
        {
            await _signInManager.SignOutAsync().ConfigureAwait(false);
            return Ok();
        }

        [HttpPost("upload-image")]
        public async Task<ActionResult> uploadImage(IFormFile file)
        {
            if (!file.ContentType.Contains("image/"))
            {
                return BadRequest("You have not uploaded an image");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                using (var img = Image.FromStream(memoryStream))
                {
                    // TODO: ResizeImage(img, 100, 100);
                    // img.set
                    img.Save(Directory.GetCurrentDirectory() + "/testFile.png", System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            return Ok(Directory.GetCurrentDirectory());
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(AppUserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.UserName,
                    Email = model.Email,
                    // PictureUrl = model.PictureURL
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return Ok();
                }

            }
            return BadRequest();
        }

        // [HttpPost("login")]
        // public async Task<ActionResult> Login(LoginViewModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var user = new AppUser
        //         {
        //             Email = model.Email,
        //             Password = model.Password,
        //         };

        //         var result = await _userManager.FindByEmailAsync(user.Email);
        //         if (result.Succeeded)
        //         {
        //             await _signInManager.UserManager.FindByEmailAsync(model.Email);
        //             return Ok();
        //         }

        //     }
        //     return BadRequest();
        // }
        #endregion testing



        [HttpGet("all", Name = "GetUsers")]
        public async Task<ActionResult> GetUsers(
           string? username,
            string? firstName,
            string? lastName,
            string? email,
            string? pictureURL,
            int page = 1,
            int pageSize = 5)
        {

            //TODO(Dario)   sanitize possible input scenarios

            try
            {

                var response = await _service.GetUsers(
                    username: username,
                    firstName: firstName,
                    lastName: lastName,
                    email: email,
                    pictureURL: pictureURL,
                    page: page,
                    pageSize: pageSize
                );

                return (response is null) ? NotFound() : Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult> GetUser(string id)
        {
            try
            {

                var task = await _service.GetUser(id);

                return task is null ? NotFound() : Ok(task);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update", Name = "UpdateUser")]
        public async Task<ActionResult> UpdateUser([FromBody] AppUser user)
        {
            try
            {

                if (user == null)
                {
                    return NotFound();
                }

                var success = await _service.UpdateUser(user);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create", Name = "CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] AppUser user)
        {
            try
            {
                if (user is null)
                {
                    return NotFound();
                }

                var success = await _service.CreateUser(user);
                return Ok(success);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete", Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUser(string userID)
        {
            try
            {
                if (string.IsNullOrEmpty(userID))
                {
                    return NotFound();
                }

                var success = await _service.DeleteUser(userID);
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