using Vezeeta.API.Dtos.Booking;
using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API.Dtos.Dashboard
{
    public class PatientWithBookingDto
    {
        public string? ImageUrl { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string? DoctorImageUrl { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string Specialization { get; set; }
        public Days Day { get; set; }
        public string Time { get; set; }
        public decimal Price { get; set; } 
        public string? DiscountCode { get; set; } 
        public decimal FinalPrice { get; set; }
        public Status Status { get; set; }
    }
}
