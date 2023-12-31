using AutoMapper;
using Vezeeta.API.Dtos.Booking;
using Vezeeta.Core.Domain;

namespace Vezeeta.API.Helpers
{
    public class BookingsPatientImageUrlResolver : IValueResolver<Booking, BookingsPatientDto, string>
    {
        private readonly IConfiguration _configuration;

        public BookingsPatientImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Booking source, BookingsPatientDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Doctor.ImageUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.Doctor.ImageUrl}";

            return string.Empty;
        }
    }
}
