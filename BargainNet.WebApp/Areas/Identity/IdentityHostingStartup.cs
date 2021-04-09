using Bargain.Infra.SQL.Repositories;
using BargainNet.Core.Contracts.Repositories;
using BargainNet.Core.Contracts.Services;
using BargainNet.Core.Entities;
using BargainNet.Core.Services;
using BargainNet.Infra.SQL.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BargainNet.WebApp.Areas.Identity.IdentityHostingStartup))]
namespace BargainNet.WebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BargainContextConnection"), assembly => assembly.MigrationsAssembly("BargainNet.WebApp")));
                
                services.AddDefaultIdentity<User>(options => 
                {
                    options.SignIn.RequireConfirmedAccount = false;                    
                }).AddEntityFrameworkStores<DataContext>();

                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IUserProfileService, UserProfileService>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            });
        }
    }
}