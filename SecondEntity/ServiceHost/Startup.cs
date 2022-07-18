using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _0_Framework.Infrastructure;
using _01_Query.Contract;
using _01_Query.Query;
using AccountManagement.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Bootstrapper;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using ShopManagement.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace ServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionstring = Configuration.GetConnectionString("SecondEntityDb");
            ShopManagementBootstrapper.Configure(services, connectionstring);
            DiscountManagementBootstrapper.Configure(services, connectionstring);
            InventoryManagementBootstrapper.Configure(services, connectionstring);
            BlogManagementBootstrapper.Configure(services, connectionstring);
            CommentManagementBootstrapper.Configure(services, connectionstring);
            AccountManagementBootstrapper.Configure(services,connectionstring);
            services.AddTransient<IFileUploader, FileUploader>();
            services.AddScoped<IFileUploader, FileUploader>();

            services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
            services.AddScoped<IZarinPalFactory, ZarinPalFactory>();

            services.AddSingleton(HtmlEncoder
                .Create(UnicodeRanges.BasicLatin,
                UnicodeRanges.Arabic));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IFileUploader, FileUploader>();
            services.AddScoped<IFileUploader, FileUploader>();
            services.AddTransient<IAuthHelper, AuthHelper>();
            services.AddScoped<IAuthHelper, AuthHelper>();
            //services.AddTransient<IZarinPalFactory, ZarinPalFactory>();
            //services.AddScoped<IZarinPalFactory, ZarinPalFactory>();
            //services.AddTransient<ISmsService, SmsService>();
            //services.AddTransient<IEmailService, EmailService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                    o.LoginPath = new PathString("/Account");
                    o.LogoutPath = new PathString("/Account");
                    o.AccessDeniedPath = new PathString("/AccessDenied");
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminArea",
                    builder => builder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }));

                options.AddPolicy("Shop",
                    builder => builder.RequireRole(new List<string> { Roles.Administrator }));

                options.AddPolicy("Discount",
                    builder => builder.RequireRole(new List<string> { Roles.Administrator }));

                options.AddPolicy("Account",
                    builder => builder.RequireRole(new List<string> { Roles.Administrator }));
            });

            services.AddCors(options => options.AddPolicy("MyPolicy", builder =>
                builder
                    .WithOrigins("https://localhost:5002")
                    .AllowAnyHeader()
                    .AllowAnyMethod()));

            services.AddRazorPages();
                //.AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
                //.AddRazorPagesOptions(options =>
                //{
                //    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
                //    options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
                //    options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
                //    options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
                //})
                //.AddApplicationPart(typeof(ProductController).Assembly)
                //.AddApplicationPart(typeof(InventoryController).Assembly)
                //.AddNewtonsoftJson();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();



            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
