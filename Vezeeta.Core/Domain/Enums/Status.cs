using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Domain.Enums
{
    public enum Status
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Completed")]
        Completed,
        [EnumMember(Value = "canceled")]
        canceled,
    }
}
