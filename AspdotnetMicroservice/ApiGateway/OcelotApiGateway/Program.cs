using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
//{
//    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true).AddEnvironmentVariables();
//});
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath).AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional:false,reloadOnChange:true).AddEnvironmentVariables();

////////////////////       Services       ///////////////////////////

builder.Services.AddOcelot(builder.Configuration);                            

var app = builder.Build();









//app.MapGet("/", () => "Hello World!");=>these would not work for now

app.UseOcelot().Wait();

app.Run();
