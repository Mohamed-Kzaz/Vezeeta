using AutoMapper;
using Gym.APIs.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vezeeta.API.Dtos.Appointment;
using Vezeeta.API.Dtos.Discount;
using Vezeeta.API.Errors;
using Vezeeta.API.Helpers;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Specifications;
using Vezeeta.Core.UnitOfWork;

namespace Vezeeta.API.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class AppointmentController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

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
