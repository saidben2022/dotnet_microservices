using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand,int>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        private readonly IEmailService _mailService;
        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,IEmailService emailService)
        {

            this._orderRepository = orderRepository;
            this._mapper = mapper;
            this._mailService = emailService;

        }

        async Task<int>  IRequestHandler<CheckoutOrderCommand, int>.Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
          var orderentity=  _mapper.Map<Order>(request);
          var neworder=   await _orderRepository.AddAsync(orderentity);
            await SendMail(neworder);
            return neworder.Id;
         }
        private async Task SendMail(Order order)
        {
            var email = new Email() { To = "ezozkme@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

            try
            {
                await _mailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
