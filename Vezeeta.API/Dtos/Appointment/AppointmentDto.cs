using Vezeeta.Core.Domain;

namespace Vezeeta.API.Dtos.Appointment
{
    public class AppointmentDto
    {
        public decimal Price { get; set; }
        public List<AppointmentDayDto> AppointmentDays { get; set; }
        public string DoctorId { get; set; } //For Doctor
    }
}
