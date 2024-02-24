using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API.Dtos.Booking
{
    public class BookingDto
    {
        public string DoctorId { get; set; }
        public int DayId { get; set; }
        public int TimeId { get; set; }
        public decimal Price { get; set; }
        public string? DiscountCode { get; set; }
    }
}
