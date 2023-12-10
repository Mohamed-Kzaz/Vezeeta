using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.Core.Domain
{
    public class AppointmentTime : BaseDomain
    {
        public string Time { get; set; }
        public int AppointmentDayId { get; set; }
        public virtual AppointmentDay AppointmentDay { get; set; }
    }
}
