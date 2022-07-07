using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastracture.Presistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext)
        {
            if (!orderContext.orders.Any())
            {
                orderContext.orders.AddRange(GetPreconfiguredOrder());
            }
        }
        private static IEnumerable<Order> GetPreconfiguredOrder()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName   = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
            };
        }
    }
}
