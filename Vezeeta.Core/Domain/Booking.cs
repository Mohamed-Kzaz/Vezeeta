using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.Core.Domain
{
    public class Booking : BaseDomain
    {
        public Status Status { get; set; } = Status.Pending;
        public string PatientId { get; set; } //For Patient
        public int? DiscountId { get; set; }
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual ApplicationUser Patient { get; set; }
        public virtual Discount Discount { get; set; }

        public decimal GetTotal() 
            => Discount != null && Discount.Value > 0 ? Appointment.Price + Discount.Value : Appointment.Price;


    }
}
