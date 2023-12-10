using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.Core.Domain
{
    public class AppointmentDay : BaseDomain
    {
        public Days Day { get; set; }
        public ICollection<AppointmentTime> AppointmentTimes { get; set; } 
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
