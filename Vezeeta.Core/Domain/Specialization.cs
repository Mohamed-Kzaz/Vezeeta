using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Domain
{
    public class Specialization : BaseDomain
    {
        public string SpecializeName { get; set; }
        public virtual ICollection<ApplicationUser> Doctors { get; set; } = new HashSet<ApplicationUser>();
    }
}
