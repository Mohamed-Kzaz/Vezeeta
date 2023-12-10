using Gym.APIs.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Vezeeta.API.Errors;
using Vezeeta.Core.Domain;

namespace Vezeeta.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : ApiBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationUser> _roleManager;

        public DashboardController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUser> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("doctorCount")]
        public async Task<ActionResult<int>> CountOfDoctors()
        {
            var doctorRole = await _roleManager.FindByNameAsync("Doctor");

            if (doctorRole is null)
                return BadRequest(new ApiResponse(404, "There is No Doctors"));

            var usersInDoctorRole = await _userManager.GetUsersInRoleAsync(doctorRole.Id);

            return Ok(usersInDoctorRole.Count);
        }

        [HttpGet("patientCount")]
        public async Task<ActionResult<int>> CountOfPatients()
        {
            var patientRole = await _roleManager.FindByNameAsync("Patient");

            if (patientRole is null)
                return BadRequest(new ApiResponse(404, "There is No Patients"));

            var usersInPatientRole = await _userManager.GetUsersInRoleAsync(patientRole.Id);

            return Ok(usersInPatientRole.Count);
        }


    }
}
