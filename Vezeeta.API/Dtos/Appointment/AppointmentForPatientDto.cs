using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API.Dtos.Appointment
{
    public class AppointmentForPatientDto
    {
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SpecializeName { get; set; }
        public decimal Price { get; set; }
        public Gender Gender { get; set; }
        public List<AppointmentDayDto> AppointmentDays { get; set; }
    }
}
