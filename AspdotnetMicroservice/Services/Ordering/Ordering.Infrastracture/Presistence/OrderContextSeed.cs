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
                orderContext.SaveChanges();//never forget these one
            }
        }
        private static IEnumerable<Order> GetPreconfiguredOrder()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName   = "Mehmet", LastName = "Ozkaya", EmailAddress = "ezozkme@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350,CVV="5550",PaymentMethod=1,CardName="Said ben",CardNumber="XXX XXX",Expiration="12/02/2022" ,ZipCode="60300",State="Oriental",CreatedBy="Said",CreatedDate=DateTime.Now,UpdatedBy="Said",UpdatedDate=DateTime.Now}
            };
        }
    }
}
