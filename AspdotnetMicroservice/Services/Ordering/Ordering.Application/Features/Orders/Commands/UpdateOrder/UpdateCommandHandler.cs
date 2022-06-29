using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(IOrderRepository orderRepository,IMapper mapper)
        {
            this._orderRepository = orderRepository;
            this._mapper = mapper;
        }
        
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
            if(orderToUpdate == null)
            {

            }
            _mapper.Map(request, orderToUpdate,typeof(UpdateOrderCommand),typeof(Order));
            await _orderRepository.UpdateAsync(orderToUpdate);
            return Unit.Value;
        }
    }
}
