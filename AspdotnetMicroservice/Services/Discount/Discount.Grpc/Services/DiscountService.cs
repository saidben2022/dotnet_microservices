using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService:DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        //private readonly Logger<DiscountService> logger; Removed since it was not working and was a massive pain

        public DiscountService(IDiscountRepository discountRepository,  IMapper mapper/*, Logger<DiscountService> logger*/) 
        {
            _discountRepository = discountRepository;
          //  this.logger = logger;//
            _mapper = mapper;//I FORGOT THESE ON

        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var discount = await _discountRepository.GetDiscount(request.ProductName);
            if (discount == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
            }
            return _mapper.Map<CouponModel>(discount);
        
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var discount = _mapper.Map<Coupon>(request.Coupon);
            var createdDiscount = await _discountRepository.CreateDiscount(discount);
         //   logger.LogInformation($"Discount {discount.ProductName} created");
            return _mapper.Map<CouponModel>(createdDiscount);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var discount = _mapper.Map<Coupon>(request.Coupon);
            var updatedDiscount = await _discountRepository.UpdateDiscount(discount);
          //  logger.LogInformation($"Discount {discount.ProductName} updated");
            return _mapper.Map<CouponModel>(updatedDiscount);
        }
        
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var discount = await _discountRepository.DeleteDiscount(request.ProductName);
            //logger.LogInformation($"Discount {request.ProductName} deleted");
            return new DeleteDiscountResponse { Success = discount.ToString() };


        }
    }
}
