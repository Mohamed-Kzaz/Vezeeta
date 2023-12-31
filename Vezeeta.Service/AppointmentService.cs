using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Service;
using Vezeeta.Core.Specifications;
using Vezeeta.Core.UnitOfWork;

namespace Vezeeta.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<Appointment>> GetAppointmentsForDoctorAsync(string doctorId)
        {
            var spec = new AppointmentSpecifications(doctorId);
            var appointments = await _unitOfWork.Repository<Appointment>().GetAllWithSpecAsync(spec);
            return appointments;
        }

        public async Task<IReadOnlyList<Appointment>> GetAppointmentsForPatientAsync(SpecificationsParams specParams)
        {
            var spec = new AppointmentSpecifications(specParams);
            var appointments = await _unitOfWork.Repository<Appointment>().GetAllWithSpecAsync(spec);
            return appointments;
        }
    }
}
