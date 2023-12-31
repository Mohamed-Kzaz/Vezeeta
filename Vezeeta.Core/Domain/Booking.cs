using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Enums;
using Vezeeta.Core.UnitOfWork;

namespace Vezeeta.Core.Domain
{
    public class Booking : BaseDomain
    {
        public string PatientId { get; set; } //For Patient
        public string DoctorId { get; set; } //For Doctor
        public Days Day { get; set; }
        public string Time { get; set; }
        public decimal Price { get; set; }
        public int? DiscountId { get; set; }
        public decimal FinalPrice { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public virtual ApplicationUser Patient { get; set; }
        public virtual ApplicationUser Doctor { get; set; }
        public virtual Discount? Discount { get; set; }
    }
}
