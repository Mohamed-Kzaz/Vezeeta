using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Core.Domain;
using Vezeeta.Core.Service;
using Vezeeta.Core.UnitOfWork;

namespace Vezeeta.Service
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> GetDiscountIdByCodeAsync(string discountCode)
        {
            var matchingDiscount = await _unitOfWork.Repository<Discount>().GetByStringAsync(discountCode);
            return matchingDiscount.Id;
        }
    }
}
