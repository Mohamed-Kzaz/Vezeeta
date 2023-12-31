using AutoMapper;
using Vezeeta.API.Dtos.Dashboard;
using Vezeeta.Core.Domain;

namespace Vezeeta.API.Helpers
{
    public class DashboardPatientwithBookingsForPatientImageUrlResolver : IValueResolver<Booking, PatientWithBookingDto, string>
    {
        private readonly IConfiguration _configuration;

        public DashboardPatientwithBookingsForPatientImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Booking source, PatientWithBookingDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Patient.ImageUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.Patient.ImageUrl}";

            return string.Empty;
        }
    }
}
