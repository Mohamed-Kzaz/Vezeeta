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
        public int DayId { get; set; }
        public int TimeId { get; set; }
        public decimal Price { get; set; }
        public string? DiscountCode { get; set; }
        public int? DiscountId { get; set; }
        public decimal FinalPrice { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public virtual Discount? Discount { get; set; }
        public virtual ApplicationUser Patient { get; set; }
        public virtual ApplicationUser Doctor { get; set; } 
    }
}
