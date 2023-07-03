using ApplicationNo1.Trip_;
using ApplicationNo1.User_;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationNo1.Menu_
{
    public class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();
            
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITripService, TripService>();

            using IHost host = builder.Build();
            host.Run();
        }
    }
}