using AutoMapper;
using Gym.APIs.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using Vezeeta.API.Dtos.Admin;
using Vezeeta.API.Dtos.Appointment;
using Vezeeta.API.Dtos.Discount;
using Vezeeta.API.Errors;
using Vezeeta.API.Helpers;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Service;
using Vezeeta.Core.Specifications;
using Vezeeta.Core.UnitOfWork;
using Vezeeta.Repository.Data;
using Vezeeta.Service;

namespace Vezeeta.API.Controllers
{
    public class AppointmentController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentController(IUnitOfWork unitOfWork,IAppointmentService appointmentService ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("all/appointments")]
        public async Task<ActionResult<Pagination<AppointmentForPatientDto>>> GetAllPatients([FromQuery] SpecificationsParams specParams)
        {
            var appointmentsWithSpec = await _appointmentService.GetAppointmentsForPatientAsync(specParams);

            var mappedAppointments = _mapper.Map<IReadOnlyList<AppointmentForPatientDto>>(appointmentsWithSpec);

            var paginatedData = new Pagination<AppointmentForPatientDto>(specParams.PageIndex, specParams.PageSize, mappedAppointments.Count, mappedAppointments);

            return Ok(paginatedData);
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppointmentToReturnDto>>> GetAppointmentsForDoctor()
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appointments = await _appointmentService.GetAppointmentsForDoctorAsync(doctorId);
            var mappedappointments = _mapper.Map<IReadOnlyList<AppointmentToReturnDto>>(appointments);
            return Ok(mappedappointments);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<ActionResult> AddAppointment([FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null || appointmentDto.AppointmentDays == null || !appointmentDto.AppointmentDays.Any())
            {
                return BadRequest(new ApiResponse(400));
            }

            var appointment = _mapper.Map<Appointment>(appointmentDto);

            _unitOfWork.Repository<Appointment>().Add(appointment);

            await _unitOfWork.Complete();

            return Ok(new ApiResponse(200, "Added successfully"));

        }

        [Authorize(Roles = "Doctor")]
        [HttpPut]
        public async Task<ActionResult> UpdateAppointment(int Id, [FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto == null || appointmentDto.AppointmentDays == null || !appointmentDto.AppointmentDays.Any())
            {
                return BadRequest(new ApiResponse(400));
            }

            var existingAppointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(Id);

            if (existingAppointment == null)
            {
                return NotFound(new ApiResponse(404, "Appointment not found"));
            }

            _mapper.Map(appointmentDto, existingAppointment);

            _unitOfWork.Repository<Appointment>().Update(existingAppointment);

            await _unitOfWork.Complete();

            return Ok(new ApiResponse(200, "Updated successfully"));
        }

        [Authorize(Roles = "Doctor")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            var appointment = await _unitOfWork.Repository<Appointment>().GetByIdAsync(id);

            if (appointment == null)
                return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Appointment>().Delete(appointment);

            await _unitOfWork.Complete();

            return Ok(new ApiResponse(200, "Deleted Successfully"));
        }

    }
}
