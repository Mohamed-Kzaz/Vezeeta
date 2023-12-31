using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Domain
{
    public class Appointment : BaseDomain
    {
        public decimal Price { get; set; }
        public string DoctorId { get; set; } //For Doctor
        public virtual ApplicationUser Doctor { get; set; }
        public virtual ICollection<AppointmentDay> AppointmentDays { get; set; } = new HashSet<AppointmentDay>();
    }
}
