using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API.Dtos.Appointment
{
    public class AppointmentDayDto
    {
        public int Id { get; set; }
        public Days Day { get; set; }
        public List<AppointmentTimeDto> AppointmentTimes { get; set; }
    }
}
