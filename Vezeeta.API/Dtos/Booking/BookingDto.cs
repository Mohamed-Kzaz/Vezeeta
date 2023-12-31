using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API.Dtos.Booking
{
    public class BookingDto
    {
        public string PatientId { get; set; } //For Patient
        public string DoctorId { get; set; } //For Doctor
        public Days Day { get; set; }
        public string Time { get; set; }
        public decimal Price { get; set; }
        public int? DiscountId { get; set; }
    }
}
