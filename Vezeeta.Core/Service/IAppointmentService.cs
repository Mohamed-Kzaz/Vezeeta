using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Specifications;

namespace Vezeeta.Core.Service
{
    public interface IAppointmentService
    {
        Task<IReadOnlyList<Appointment>> GetAppointmentsForPatientAsync(SpecificationsParams specParams);
        Task<IReadOnlyList<Appointment>> GetAppointmentsForDoctorAsync(string doctorId);
    }
}
