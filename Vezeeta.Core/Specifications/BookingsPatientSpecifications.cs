using Microsoft.EntityFrameworkCore;
using Vezeeta.Core.Domain;

namespace Vezeeta.Core.Specifications
{
    public class BookingsPatientSpecifications : BaseSpecification<Booking>
    {
        public BookingsPatientSpecifications(string patientId, SpecificationsParams specParams)
            : base(B => (B.PatientId == patientId) &&
            (string.IsNullOrEmpty(specParams.Search) || B.Doctor.FirstName.Contains(specParams.Search)) // Search By Name
            )
        {
            Includes.Add(B => B.Include(B => B.Doctor).ThenInclude(D => D.Specialization));
            Includes.Add(B => B.Include(B => B.Discount));
            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }

        public BookingsPatientSpecifications(string patientId)
           : base(B => B.PatientId == patientId)
        {
            Includes.Add(B => B.Include(B => B.Doctor).ThenInclude(D => D.Specialization));
            Includes.Add(B => B.Include(B => B.Discount));
        }

    }
}
