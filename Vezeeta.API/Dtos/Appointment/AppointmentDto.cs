using Vezeeta.Core.Domain;

namespace Vezeeta.API.Dtos.Appointment
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public List<AppointmentDayDto> AppointmentDays { get; set; }
        public string DoctorId { get; set; } //For Doctor
    }
}
