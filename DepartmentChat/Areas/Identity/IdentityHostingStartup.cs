using System;
using DepartmentChat.Areas.Identity.Data;
using DepartmentChat.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DepartmentChat.Areas.Identity.IdentityHostingStartup))]
namespace DepartmentChat.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<ChatIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ChatIdentityContextConnection")));
                services.ConfigureApplicationCookie(options =>
                {
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    options.Cookie.Name = "departchat";
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(4320);
                    options.LoginPath = "/account/signin";
                    options.SlidingExpiration = true;
                });
                services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opt =>
                {
                    opt.LoginPath = "/account/signin";
                });

                services.AddDefaultIdentity<DepartmentUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ChatIdentityContext>();
            });
        }
    }
}