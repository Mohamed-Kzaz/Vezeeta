using Gym.APIs.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vezeeta.API.Dtos.Account;
using Vezeeta.API.Errors;
using Vezeeta.API.Helpers;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Service;

namespace Vezeeta.API.Controllers
{
    public class AccountController : ApiBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromForm] RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "this email is already in use" } });

            string ImageUrl = null;

            if (model.Image != null)
            {
                ImageUrl = DocumentSettings.UploadFile(model.Image, "images");
            }

            var user = new ApplicationUser()
            {
                ImageUrl = ImageUrl,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            await _userManager.AddToRoleAsync(user, "Patient");

            return Ok(new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register/doctor")]
        public async Task<ActionResult<UserDto>> RegisterDoctor([FromForm] RegisterDoctorDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "this email is already in use" } });

            var ImageUrl = DocumentSettings.UploadFile(model.Image, "images");

            var user = new ApplicationUser()
            {
                ImageUrl = ImageUrl,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                SpecializationId = model.SpecializationId,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            await _userManager.AddToRoleAsync(user, "Doctor");

            return Ok(new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("updatedoctor/{userId}")]
        public async Task<ActionResult<UserDto>> UpdateDoctor(string userId, [FromForm] RegisterDoctorDto model)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new ApiResponse(404, "User not found"));
            }

            user.ImageUrl = DocumentSettings.UploadFile(model.Image, "images");
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;
            user.Gender = model.Gender;
            user.SpecializationId = model.SpecializationId;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse(400, "Failed to update user"));
            }

            return Ok(new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("deletedoctor/{userId}")]
        public async Task<ActionResult> DeleteDoctor(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new ApiResponse(404, "User not found"));
            }

            await _userManager.DeleteAsync(user);

            DocumentSettings.DeleteFile(user.ImageUrl, "images");

            return Ok(new ApiResponse(200, "User deleted successfully"));

        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }
    }
}
