using ApplicationNo1.Trip_;
using ApplicationNo1.User_;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ApplicationNo1.Menu_;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();


builder.Services.AddHostedService<Menu>();


//Register
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITripService, TripService>();



using IHost host = builder.Build();
host.Run();