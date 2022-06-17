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
        private readonly Logger<DiscountService> logger;

        public DiscountService(IDiscountRepository discountRepository, Logger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            this.logger = logger;
            
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
            var coupon = _mapper.Map<Coupon>(request);
            var discount = await _discountRepository.CreateDiscount(coupon);
            return _mapper.Map<CouponModel>(coupon);
        }


        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            var discount = await _discountRepository.UpdateDiscount(coupon);
            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            var discount = await _discountRepository.DeleteDiscount(coupon.ProductName);
            return new DeleteDiscountResponse
            {
                Success = discount.ToString()
            };
        }
    }
}
