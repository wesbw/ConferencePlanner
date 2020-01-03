using System;
using FrontEnd.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

[assembly: HostingStartup(typeof(FrontEnd.Areas.Identity.IdentityHostingStartup))]
namespace FrontEnd.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityDbContext>(options =>{
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityDbContextConnection"));
                    }
                    else{
                        options.UseSqlite("Data Source=Identities.db");
                    }}
                    );

                services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();
            });
        }
    }
}