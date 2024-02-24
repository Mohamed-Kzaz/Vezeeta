using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.Core.Domain
{
    public class Discount : BaseDomain
    {
        public string DiscountCode { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Value { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}
