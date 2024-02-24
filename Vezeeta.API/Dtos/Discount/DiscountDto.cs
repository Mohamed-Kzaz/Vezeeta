using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API.Dtos.Discount
{
    public class DiscountDto
    {
        public string DiscountCode { get; set; }
        public DiscountType DiscountType { get; set; }
        public decimal Value { get; set; }
    }
}
