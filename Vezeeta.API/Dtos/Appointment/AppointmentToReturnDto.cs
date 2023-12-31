namespace Vezeeta.API.Dtos.Appointment
{
    public class AppointmentToReturnDto
    {
        public decimal Price { get; set; }
        public List<AppointmentDayDto> AppointmentDays { get; set; }
    }
}
