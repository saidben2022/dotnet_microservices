using AutoMapper;
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
            //return new CouponModel
            //{
               
            //    ProductName = discount.ProductName,
            //    Description = discount.Description,
            //    Amount = discount.Amount,
            
            //};
        }
    }
}
