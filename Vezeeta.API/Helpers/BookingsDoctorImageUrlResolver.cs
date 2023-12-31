using AutoMapper;
using Vezeeta.API.Dtos.Booking;
using Vezeeta.Core.Domain;

namespace Vezeeta.API.Helpers
{
    public class BookingsDoctorImageUrlResolver : IValueResolver<Booking, BookingsDoctorDto, string>
    {
        private readonly IConfiguration _configuration;

        public BookingsDoctorImageUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Booking source, BookingsDoctorDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Patient.ImageUrl))
                return $"{_configuration["ApiBaseUrl"]}{source.Patient.ImageUrl}";

            return string.Empty;
        }
    }
}
