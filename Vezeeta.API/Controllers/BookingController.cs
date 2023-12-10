using AutoMapper;
using Gym.APIs.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.API.Dtos.Admin;
using Vezeeta.API.Dtos.Booking;
using Vezeeta.API.Dtos.Discount;
using Vezeeta.API.Errors;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Domain.Enums;
using Vezeeta.Core.Specifications;
using Vezeeta.Core.UnitOfWork;

namespace Vezeeta.API.Controllers
{
    public class BookingController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public async Task<ActionResult> Add([FromForm] BookingDto model)
        {
            if (!ModelState.IsValid)
                return NotFound(new ApiResponse(400));

            var booking = _mapper.Map<Booking>(model);

            _unitOfWork.Repository<Booking>().Add(booking);

            await _unitOfWork.Complete();

            return Ok(new ApiResponse(200, "Added Successfully"));
        }

        [Authorize(Roles = "Patient")]
        [HttpPut("CancelBooking/{id}")]
        public async Task<ActionResult> CancelBooking(int id)
        {
            var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(id);

            if (booking == null)
                return NotFound(new ApiResponse(404));

            booking.Status = Status.canceled;

            _unitOfWork.Repository<Booking>().Update(booking);

            await _unitOfWork.Complete();

            return Ok(new ApiResponse(200));
        }


        [Authorize(Roles = "Doctor")]
        [HttpPut("ConfirmBooking/{id}")]
        public async Task<ActionResult> ConfirmBooking(int id)
        {
            var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(id);

            if (booking == null)
                return NotFound(new ApiResponse(404));

            booking.Status = Status.Completed;

            _unitOfWork.Repository<Booking>().Update(booking);

            await _unitOfWork.Complete();

            return Ok(new ApiResponse(200));
        }
    }
}
