using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Vezeeta.Core.Domain.Enums
{
    public enum DiscountType
    {
        [EnumMember(Value = "Percentage")]
        Percentage,
        [EnumMember(Value = "Value")]
        Value
    }
}
