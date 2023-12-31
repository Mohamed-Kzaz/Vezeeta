using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Service;
using Vezeeta.Core.Specifications;
using Vezeeta.Core.UnitOfWork;

namespace Vezeeta.Service
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Booking>> GetBookingsForDoctorAsync(string doctorId, SpecificationsParams specParams)
        {
            var spec = new BookingsDoctorSpecifications(doctorId, specParams);
            var bookings = await _unitOfWork.Repository<Booking>().GetAllWithSpecAsync(spec);
            return bookings;
        }

        public async Task<IReadOnlyList<Booking>> GetBookingsForPatientAsync(string patientId, SpecificationsParams specParams)
        {
            var spec = new BookingsPatientSpecifications(patientId, specParams);
            var bookings = await _unitOfWork.Repository<Booking>().GetAllWithSpecAsync(spec);
            return bookings;
        }
    }
}
