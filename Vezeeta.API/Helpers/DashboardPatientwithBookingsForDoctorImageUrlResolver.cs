using AutoMapper;
using Vezeeta.API.Dtos.Admin;
using Vezeeta.API.Dtos.Dashboard;
using Vezeeta.Core.Domain;

namespace Vezeeta.API.Helpers
{
    public class DashboardPatientwithBookingsForDoctorImageUrlResolver : IValueResolver<Booking, PatientWithBookingDto, string>
    {
        private readonly IConfiguration _configuration;

        public DashboardPatientwithBookingsForDoctorImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Booking source, PatientWithBookingDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Doctor.ImageUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.Doctor.ImageUrl}";

            return string.Empty;
        }
    }
}
