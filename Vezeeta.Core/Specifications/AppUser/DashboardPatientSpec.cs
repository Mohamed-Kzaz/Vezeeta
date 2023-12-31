using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;

namespace Vezeeta.Core.Specifications.AppUser
{
    public class DashboardPatientSpec : SpecificationAppUser<ApplicationUser>
    {
        // This Constructor is Used For Get All Doctors
        public DashboardPatientSpec(IList<ApplicationUser> users, SpecificationsParams specParams)
            : base(A => (users.Contains(A)) &&
            (string.IsNullOrEmpty(specParams.Search) || A.FirstName.ToLower().Contains(specParams.Search))
            )
        {

            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
    }
}
