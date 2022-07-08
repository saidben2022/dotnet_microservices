using MediatR;
using Ordering.Api.Extensions;
using Ordering.Application;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using Ordering.Infrastracture;
using Ordering.Infrastracture.Mail;
using Ordering.Infrastracture.Presistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add service to container from Application and infrastracture layers using dependency injection


builder.Services.AddApplicationServices();//warning if you can not migrate remove these these line and go to AddInfrastractureServices() and remove everthing exept the database configuration
builder.Services.AddInfrastractureServices(builder.Configuration);

var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//migrate before runing the app*/

app.MigrateDatabase<OrderContext>((context, service) =>
{
    OrderContextSeed
                         .SeedAsync(context)
                         .Wait();
});


app.Run();
