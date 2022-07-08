using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastracture.Presistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastracture.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext) : base(orderContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUsername(string userName)
        {
           
           
                var orderList = await _dbContext.orders
                             .Where(o => o.UserName == userName)
                              .ToListAsync();
                return orderList;

            
          


           



        }

    
    }
}
