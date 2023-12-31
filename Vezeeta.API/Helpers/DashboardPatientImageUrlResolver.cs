using AutoMapper;
using Microsoft.Extensions.Configuration;
using Vezeeta.API.Dtos.Admin;
using Vezeeta.API.Dtos.Dashboard;
using Vezeeta.Core.Domain;

namespace Vezeeta.API.Helpers
{
    public class DashboardPatientImageUrlResolver : IValueResolver<ApplicationUser, PatientDto, string>
    {
        private readonly IConfiguration _configuration;

        public DashboardPatientImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(ApplicationUser source, PatientDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.ImageUrl}";

            return string.Empty;
        }

    }
}
