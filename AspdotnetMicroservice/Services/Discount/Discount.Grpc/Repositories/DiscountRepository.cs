using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _config;
        public DiscountRepository(IConfiguration configuration)
        {
            _config = configuration;
        }
        //When you create table DO NOT USE THE INTERFACE TO DO IT
        //CREATE IT USING THE CREATE QUERY
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_config.GetValue<String>("DataBaseSettings:ConnectionString"));
           var affected= await connection.ExecuteAsync("INSERT INTO Coupon (ProductName, Description,Amount) VALUES (@productName, @Description,@Amount)", new { productName = coupon.ProductName, Description = coupon.Description,Amount=coupon.Amount });
            if (affected == 0) {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_config.GetValue<String>("DataBaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName=@productName", new { productName = productName });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }
        
        
        

        public async Task<Coupon> GetDiscount(string productName)
        {
               using var connection = new NpgsqlConnection(_config.GetValue<String>("DataBaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName=@product_name", new { product_name = productName });
           if(coupon==null)
            {
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount", };
            }
                return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_config.GetValue<String>("DataBaseSettings:ConnectionString"));
            var affected =await connection.ExecuteAsync("UPDATE Coupon SET ProductName=@productName, Description=@Description,Amount=@Amount WHERE ProductName=@productName", new { productName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }
    }
}
