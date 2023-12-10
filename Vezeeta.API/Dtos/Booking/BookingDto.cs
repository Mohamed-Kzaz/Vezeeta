using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API.Dtos.Booking
{
    public class BookingDto
    {
        public Status Status { get; set; } = Status.Pending;
        public string PatientId { get; set; } //For Patient
        public int? DiscountId { get; set; }
        public int AppointmentId { get; set; }
    }
}
