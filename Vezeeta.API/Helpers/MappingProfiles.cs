using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using Vezeeta.API.Dtos.Admin;
using Vezeeta.API.Dtos.Appointment;
using Vezeeta.API.Dtos.Booking;
using Vezeeta.API.Dtos.Dashboard;
using Vezeeta.API.Dtos.Discount;
using Vezeeta.Core.Domain;

namespace Vezeeta.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Discount, DiscountDto>().ReverseMap();
            CreateMap<Appointment, AppointmentToReturnDto>();
            CreateMap<AppointmentDayDto, AppointmentDay>().ReverseMap();
            CreateMap<AppointmentTimeDto, AppointmentTime>().ReverseMap();
            CreateMap<BookingDto, Booking>();

            CreateMap<AppointmentDto, Appointment>()
                .ForMember(dest => dest.AppointmentDays, opt => opt.MapFrom(src => src.AppointmentDays)).ReverseMap();

            CreateMap<Appointment, AppointmentForPatientDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<AppointmentPatientImageUrlResolver>())
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Doctor.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Doctor.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Doctor.PhoneNumber))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Doctor.Gender))
                .ForMember(dest => dest.SpecializeName, opt => opt.MapFrom(src => src.Doctor.Specialization.SpecializeName));

            // To Get All Doctors For Admin
            CreateMap<ApplicationUser, DoctorDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<DashboardDoctorImageUrlResolver>())
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization.SpecializeName));

            // To Get All Patients For Admin
            CreateMap<ApplicationUser, PatientDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<DashboardPatientImageUrlResolver>());

            // To Get Patient For Admin
            CreateMap<Booking, PatientWithBookingDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<DashboardPatientwithBookingsForPatientImageUrlResolver>())
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Patient.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Patient.LastName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Patient.Gender))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Patient.DateOfBirth))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Patient.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Patient.Email))
                .ForMember(dest => dest.DoctorImageUrl, opt => opt.MapFrom<DashboardPatientwithBookingsForDoctorImageUrlResolver>())
                .ForMember(dest => dest.DoctorFirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
                .ForMember(dest => dest.DoctorLastName, opt => opt.MapFrom(src => src.Doctor.LastName))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Doctor.Specialization.SpecializeName))
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.DiscountCode, opt => opt.MapFrom(src => src.Discount.DiscountCode))
                .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => src.FinalPrice))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<Booking, BookingsPatientDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<BookingsPatientImageUrlResolver>())
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Doctor.LastName))
                .ForMember(dest => dest.SpecializeName, opt => opt.MapFrom(src => src.Doctor.Specialization.SpecializeName))
                .ForMember(dest => dest.DiscountCode, opt => opt.MapFrom(src => src.Discount.DiscountCode));

            CreateMap<Booking, BookingsDoctorDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Patient.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Patient.LastName))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<BookingsDoctorImageUrlResolver>())
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Patient.Gender))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Patient.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Patient.Email))
                .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.Time));
        }
    }
}
