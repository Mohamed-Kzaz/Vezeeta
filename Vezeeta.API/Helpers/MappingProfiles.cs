using AutoMapper;
using Vezeeta.API.Dtos.Appointment;
using Vezeeta.API.Dtos.Booking;
using Vezeeta.API.Dtos.Discount;
using Vezeeta.Core.Domain;

namespace Vezeeta.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Discount, DiscountDto>().ReverseMap();

            CreateMap<AppointmentDto, Appointment>()
            .ForMember(dest => dest.AppointmentDays, opt => opt.MapFrom(src => src.AppointmentDays)).ReverseMap();
            CreateMap<AppointmentDayDto, AppointmentDay>().ReverseMap();
            CreateMap<AppointmentTimeDto, AppointmentTime>().ReverseMap();

            CreateMap<BookingDto, Booking>();
        }
    }
}
