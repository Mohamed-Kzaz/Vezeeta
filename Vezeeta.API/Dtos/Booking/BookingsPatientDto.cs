using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API.Dtos.Booking
{
    // Doctors data that will be presented to the patient.
    public class BookingsPatientDto
    {
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SpecializeName { get; set; }
        public Days Day { get; set; }
        public string Time { get; set; }
        public decimal Price { get; set; }
        public string? DiscountCode { get; set; }
        public decimal FinalPrice { get; set; }
        public Status Status { get; set; }
    }
}
