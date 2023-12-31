using AutoMapper;
using Vezeeta.API.Dtos.Booking;
using Vezeeta.API.Dtos.Dashboard;
using Vezeeta.Core.Domain;

namespace Vezeeta.API.Helpers
{
    public class DashboardDoctorImageUrlResolver : IValueResolver<ApplicationUser, DoctorDto, string>
    {
        private readonly IConfiguration _configuration;

        public DashboardDoctorImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(ApplicationUser source, DoctorDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.ImageUrl}";

            return string.Empty;
        }
    }
}
