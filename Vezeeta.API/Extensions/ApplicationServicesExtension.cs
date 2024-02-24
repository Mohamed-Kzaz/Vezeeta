using Microsoft.AspNetCore.Mvc;
using Vezeeta.API.Errors;
using Vezeeta.API.Helpers;
using Vezeeta.Core.Repository;
using Vezeeta.Core.Service;
using Vezeeta.Core.UnitOfWork;
using Vezeeta.Repository;
using Vezeeta.Service;

namespace Vezeeta.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IDiscountService, DiscountService>();
            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                     .SelectMany(P => P.Value.Errors)
                                     .Select(E => E.ErrorMessage)
                                     .ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });

            return services;
        }
    }
}