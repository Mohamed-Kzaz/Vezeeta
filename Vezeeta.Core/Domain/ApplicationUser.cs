using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? ImageUrl { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string DateOfBirth { get; set; }
        public int? SpecializationId { get; set; } //For Doctor
        public virtual Specialization Specialization { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>(); //For Doctor
    }
}
