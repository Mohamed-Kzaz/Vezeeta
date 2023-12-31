using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;

namespace Vezeeta.Core.Specifications
{
    public class BookingsDoctorSpecifications : BaseSpecification<Booking>
    {
        public BookingsDoctorSpecifications(string doctortId, SpecificationsParams specParams)
           : base(B => (B.DoctorId == doctortId) &&
           (string.IsNullOrEmpty(specParams.Search) || B.Day.ToString().Contains(specParams.Search)) // Search By Day
           )
        {
            Includes.Add(B => B.Include(B => B.Patient));
            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
    }
}
