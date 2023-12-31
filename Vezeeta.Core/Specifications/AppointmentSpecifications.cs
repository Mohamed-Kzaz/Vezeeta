using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;

namespace Vezeeta.Core.Specifications
{
    public class AppointmentSpecifications : BaseSpecification<Appointment>
    {
        public AppointmentSpecifications(SpecificationsParams specParams)
            : base(B =>
            (string.IsNullOrEmpty(specParams.Search) || B.Doctor.Specialization.SpecializeName.Contains(specParams.Search)) // Search By Specialize Name
            )
        {
            Includes.Add(B => B.Include(B => B.Doctor).ThenInclude(D => D.Specialization));
            Includes.Add(A => A.Include(d => d.AppointmentDays).ThenInclude(d => d.AppointmentTimes));
            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
        public AppointmentSpecifications(string doctorId)
            : base(A => A.DoctorId == doctorId)
        {
            Includes.Add(A => A.Include(d => d.AppointmentDays).ThenInclude(d => d.AppointmentTimes));
        }
    }
}
