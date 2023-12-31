using AutoMapper;
using Gym.APIs.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vezeeta.API.Dtos.Admin;
using Vezeeta.API.Dtos.Dashboard;
using Vezeeta.API.Errors;
using Vezeeta.API.Helpers;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Domain.Enums;
using Vezeeta.Core.Service;
using Vezeeta.Core.Specifications;
using Vezeeta.Core.UnitOfWork;
using Vezeeta.Repository.Data;

namespace Vezeeta.API.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class DashboardController : ApiBaseController
    {
        private readonly IDashboardService _dashboardService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(
            IDashboardService dashboardService, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            _dashboardService = dashboardService;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("doctor/count")]
        public async Task<ActionResult<int>> CountOfDoctors()
        {
            var doctorRole = await _roleManager.FindByNameAsync("Doctor");

            if (doctorRole is null)
                return BadRequest(new ApiResponse(404, "There is No Doctors"));

            var usersInDoctorRole = await _userManager.GetUsersInRoleAsync(doctorRole.Name);

            return Ok(usersInDoctorRole.Count);
        }

        [HttpGet("patient/count")]
        public async Task<ActionResult<int>> CountOfPatients()
        {
            var patientRole = await _roleManager.FindByNameAsync("Patient");

            if (patientRole is null)
                return BadRequest(new ApiResponse(404, "There is No Patients"));

            var usersInPatientRole = await _userManager.GetUsersInRoleAsync(patientRole.Name);

            return Ok(usersInPatientRole.Count);
        }

        [HttpGet("bookings/count")]
        public async Task<ActionResult<int>> CountOfBookings()
        {
            var bookings = await _unitOfWork.Repository<Booking>().GetAllAsync();

            if (bookings is null)
                return BadRequest(new ApiResponse(404, "There is No Bookings"));

            int pendingBookingsCount = bookings.Count(b => b.Status == Status.Pending);
            int completedBookingsCount = bookings.Count(b => b.Status == Status.Completed);
            int canceledBookingsCount = bookings.Count(b => b.Status == Status.canceled);

            var counts = new
            {
                PendingBookingsCount = pendingBookingsCount,
                CompletedBookingsCount = completedBookingsCount,
                CanceledBookingsCount = canceledBookingsCount
            };

            return Ok(counts);
        }


        [HttpGet("all/doctors")]
        public async Task<ActionResult<Pagination<DoctorDto>>> GetAllDoctors([FromQuery] SpecificationsParams specParams)
        {
            var doctorRole = await _roleManager.FindByNameAsync("Doctor");

            if (doctorRole is null)
                return BadRequest(new ApiResponse(404, "There is No Doctors"));

            var usersInDoctorRole = await _userManager.GetUsersInRoleAsync(doctorRole.Name);

            var doctorsWithSpecialization = await _dashboardService.GetAllDoctorsAsync(usersInDoctorRole, specParams);

            var mappedDoctors = _mapper.Map<IReadOnlyList<DoctorDto>>(doctorsWithSpecialization);

            var paginatedData = new Pagination<DoctorDto>(specParams.PageIndex, specParams.PageSize, mappedDoctors.Count, mappedDoctors);

            return Ok(paginatedData);
        }

        [HttpGet("doctor/{id}")]
        public async Task<ActionResult<DoctorDto>> GetDoctorById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return BadRequest(new ApiResponse(404, "There is No Doctor With This Id"));

            var doctorWithSpecialization = await _dashboardService.GetDoctorByIdAsync(id);

            var mappedDoctor = _mapper.Map<DoctorDto>(doctorWithSpecialization);

            return Ok(mappedDoctor);
        }

        [HttpGet("all/patients")]
        public async Task<ActionResult<Pagination<PatientDto>>> GetAllPatients([FromQuery] SpecificationsParams specParams)
        {
            var patientRole = await _roleManager.FindByNameAsync("Patient");

            if (patientRole is null)
                return BadRequest(new ApiResponse(404, "There is No Patients"));

            var usersInPatientRole = await _userManager.GetUsersInRoleAsync(patientRole.Name);

            var patientsWithSpec = await _dashboardService.GetAllPatientsAsync(usersInPatientRole, specParams);

            var mappedPatients = _mapper.Map<IReadOnlyList<PatientDto>>(patientsWithSpec);

            var paginatedData = new Pagination<PatientDto>(specParams.PageIndex, specParams.PageSize, mappedPatients.Count, mappedPatients);

            return Ok(paginatedData);
        }

        [HttpGet("patient/{id}")]
        public async Task<ActionResult<IReadOnlyList<PatientWithBookingDto>>> GetPatientById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return BadRequest(new ApiResponse(404, "There is No Patient With This Id"));

            var patientWithSpec = await _dashboardService.GetPatientByIdAsync(id);

            var mappedPatient = _mapper.Map<IReadOnlyList<PatientWithBookingDto>>(patientWithSpec);

            return Ok(mappedPatient);
        }
    }
}
