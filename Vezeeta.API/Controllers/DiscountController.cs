using AutoMapper;
using Gym.APIs.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.API.Dtos.Discount;
using Vezeeta.API.Errors;
using Vezeeta.Core.Domain;
using Vezeeta.Core.UnitOfWork;

namespace Vezeeta.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DiscountController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiscountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<DiscountDto>> GetAllAsync()
        {
            var discounts = await _unitOfWork.Repository<Discount>().GetAllAsync();

            var mappeddiscounts = _mapper.Map<IReadOnlyList<DiscountDto>>(discounts);

            return Ok(mappeddiscounts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountDto>> GetByIdAsync(int id)
        {
            var discount = await _unitOfWork.Repository<Discount>().GetByIdAsync(id);

            if (discount == null)
                return NotFound(new ApiResponse(404));

            var mappeddiscount = _mapper.Map<IReadOnlyList<DiscountDto>>(discount);

            return Ok(mappeddiscount);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromForm] DiscountDto model)
        {
            if (!ModelState.IsValid)
                return NotFound(new ApiResponse(400));

            var discount = _mapper.Map<Discount>(model);

            _unitOfWork.Repository<Discount>().Add(discount);

            await _unitOfWork.Complete();

            return Ok(new ApiResponse(200, "Added Successfully"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] DiscountDto model)
        {
            var discount = await _unitOfWork.Repository<Discount>().GetByIdAsync(id);

            if (discount == null)
                return NotFound(new ApiResponse(404));

            discount.DiscountCode = model.DiscountCode;
            discount.DiscountType = model.DiscountType;
            discount.Value = model.Value;

            _unitOfWork.Repository<Discount>().Update(discount);

            await _unitOfWork.Complete();

            return Ok(new ApiResponse(200, "Updated Successfully"));
        }

        [HttpPut("DeActivate/{id}")]
        public async Task<ActionResult> DeActivate(int id)
        {
            var discount = await _unitOfWork.Repository<Discount>().GetByIdAsync(id);

            if (discount == null)
                return NotFound(new ApiResponse(404));

            if (discount.IsActive == true)
                discount.IsActive = false;
            else
                discount.IsActive = true;

            _unitOfWork.Repository<Discount>().Update(discount);

            await _unitOfWork.Complete();

            return Ok(new ApiResponse(200));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var discount = await _unitOfWork.Repository<Discount>().GetByIdAsync(id);

            if (discount == null)
                return NotFound(new ApiResponse(404));

            _unitOfWork.Repository<Discount>().Delete(discount);

            await _unitOfWork.Complete();

            return Ok(new ApiResponse(200, "Deleted Successfully"));
        }


    }
}
