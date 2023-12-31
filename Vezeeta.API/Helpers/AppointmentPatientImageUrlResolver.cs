using AutoMapper;
using Vezeeta.API.Dtos.Appointment;
using Vezeeta.API.Dtos.Booking;
using Vezeeta.Core.Domain;

namespace Vezeeta.API.Helpers
{
    public class AppointmentPatientImageUrlResolver : IValueResolver<Appointment, AppointmentForPatientDto, string>
    {
        private readonly IConfiguration _configuration;

        public AppointmentPatientImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Appointment source, AppointmentForPatientDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Doctor.ImageUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.Doctor.ImageUrl}";

            return string.Empty;
        }
    }
}
