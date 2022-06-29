using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrderListQuery:IRequest<List<OrderVm>>
    {
        public  string Username { get; set; }

        public GetOrderListQuery(string username)
        {
            Username = username;
        }
    }
}
