using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;

namespace Vezeeta.Core.Specifications.AppUser
{
    public class DashboardDoctorSpec : SpecificationAppUser<ApplicationUser>
    {
        // This Constructor is Used For Get All Doctors
        public DashboardDoctorSpec(IList<ApplicationUser> users, SpecificationsParams specParams)
            : base(A => (users.Contains(A)) &&
            (string.IsNullOrEmpty(specParams.Search) || A.FirstName.ToLower().Contains(specParams.Search)) 
            )
        { 
            Includes.Add(A => A.Include(A => A.Specialization));
            ApplyPagination(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }


        // This Constructor is Used For Get a Specific Doctor
        public DashboardDoctorSpec(string id)
            : base(P => P.Id == id)
        {
            Includes.Add(A => A.Include(A => A.Specialization));
        }
    }
}
