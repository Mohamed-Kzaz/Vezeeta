using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Specifications;

namespace Vezeeta.Core.Service
{
    public interface IBookingService
    {
        Task<IReadOnlyList<Booking>> GetBookingsForPatientAsync(string patientId, SpecificationsParams specParams);
        Task<IReadOnlyList<Booking>> GetBookingsForDoctorAsync(string doctorId, SpecificationsParams specParams);
    }
}
