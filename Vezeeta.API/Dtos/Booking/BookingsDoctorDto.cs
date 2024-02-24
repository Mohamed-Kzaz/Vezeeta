using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API.Dtos.Booking
{
    // Patient data that will be presented to the doctor.
    public class BookingsDoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Days Day { get; set; }
        public string Time { get; set; }
    }
}
