using AutoMapper;
using Gym.APIs.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vezeeta.API.Dtos.Admin;
using Vezeeta.API.Dtos.Appointment;
using Vezeeta.API.Dtos.Booking;
using Vezeeta.API.Dtos.Dashboard;
using Vezeeta.API.Dtos.Discount;
using Vezeeta.API.Errors;
using Vezeeta.API.Helpers;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Domain.Enums;
using Vezeeta.Core.Service;
using Vezeeta.Core.Specifications;
using Vezeeta.Core.UnitOfWork;
using Vezeeta.Service;

namespace Vezeeta.API.Controllers
{
    public class BookingController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookingService _bookingService;
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;

        public BookingController(IUnitOfWork unitOfWork, IBookingService bookingService, IDiscountService discountService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _bookingService = bookingService;
            _discountService = discountService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public async Task<ActionResult> Add([FromForm] BookingDto model)
        {
            if (!ModelState.IsValid)
                return NotFound(new ApiResponse(400));

            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(model.DiscountCode))
            {
                var discountId = await _discountService.GetDiscountIdByCodeAsync(model.DiscountCode);

                var discount = await _unitOfWork.Repository<Discount>().GetByIdAsync(discountId);

                if (discount.IsActive == false)
                    return Ok(new ApiResponse(404, "Sorry, Discount Code Not Found"));

                decimal finalPrice;

                if (discount.DiscountType == DiscountType.Value)
                    finalPrice = model.Price - discount.Value;
                else
                    finalPrice = model.Price - (model.Price * discount.Value / 100);

                var booking = new Booking()
                {
                    PatientId = patientId,
                    DoctorId = model.DoctorId,
                    DayId = model.DayId,
                    TimeId = model.TimeId,
                    Price = model.Price,
                    DiscountCode = model.DiscountCode,
                    FinalPrice = finalPrice
                };

                _unitOfWork.Repository<Booking>().Add(booking);
            }
            else
            {
                var booking = new Booking()
                {
                    PatientId = patientId,
                    DoctorId = model.DoctorId,
                    DayId = model.DayId,
                    TimeId = model.TimeId,
                    Price = model.Price,
                };

                _unitOfWork.Repository<Booking>().Add(booking);
            }

            await _unitOfWork.Complete();

            return Ok(new ApiResponse(200, "Added Successfully"));
        }

        [Authorize(Roles = "Patient")]
        [HttpPut("cancelbooking/{id}")]
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
        [HttpPut("confirmbooking/{id}")]
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

        [Authorize(Roles = "Patient")]
        [HttpGet("bookings/patient")]
        public async Task<ActionResult<Pagination<BookingsPatientDto>>> GetAllPatientBookings([FromQuery] SpecificationsParams specParams)
        {
            var patientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookings = await _bookingService.GetBookingsForPatientAsync(patientId, specParams);
            var mappedBookings = _mapper.Map<IReadOnlyList<BookingsPatientDto>>(bookings);
            var paginatedData = new Pagination<BookingsPatientDto>(specParams.PageIndex, specParams.PageSize, mappedBookings.Count, mappedBookings);
            return Ok(paginatedData);
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("bookings/doctor")]
        public async Task<ActionResult<Pagination<BookingsDoctorDto>>> GetAllDoctorBookings([FromQuery] SpecificationsParams specParams)
        {
            var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookings = await _bookingService.GetBookingsForDoctorAsync(doctorId, specParams);
            var mappedBookings = _mapper.Map<IReadOnlyList<BookingsDoctorDto>>(bookings);
            var paginatedData = new Pagination<BookingsDoctorDto>(specParams.PageIndex, specParams.PageSize, mappedBookings.Count, mappedBookings);
            return Ok(paginatedData);
        }
    }
}
