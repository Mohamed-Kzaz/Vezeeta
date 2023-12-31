using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Specifications;

namespace Vezeeta.Core.Service
{
    public interface IDashboardService
    {
        Task<IReadOnlyList<ApplicationUser>> GetAllDoctorsAsync(IList<ApplicationUser> users, SpecificationsParams specParams);
        Task<ApplicationUser> GetDoctorByIdAsync(string id);
        Task<IReadOnlyList<ApplicationUser>> GetAllPatientsAsync(IList<ApplicationUser> users, SpecificationsParams specParams);
        Task<IReadOnlyList<Booking>> GetPatientByIdAsync(string id);
    }
}
