using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;

using System.Linq;
using System.Threading.Tasks;
using WebApp.CustomTokenProviders;
using WebApp.Data;
using WebApp.EmailService;
using Microsoft.EntityFrameworkCore;

using WebApp.Models;
using Microsoft.Extensions.Options;
using WebApp.Models.Repository;
using WebApp.Security;
using WebApp.Helper;

namespace SSLDC
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
            //services.AddControllersWithViews();
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-EG");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-EG");

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;

                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });

            services.AddMvc()
                //.AddViewLocalization(opts => { opts.ResourcesPath = "Resources"; })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                 .AddDataAnnotationsLocalization()
                //.SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                ;
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            
            services.AddControllersWithViews().AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                  //new CultureInfo("en-US"),
                  new CultureInfo("ar-EG"),
                  new CultureInfo("en-US")
                };
                opts.DefaultRequestCulture = new RequestCulture("ar-EG");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;
            });

            //google authentication
            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        options.ClientId = "1013546866271-pc6kefekjf6d7rhhh44bemvluolrdqpc.apps.googleusercontent.com";
            //        options.ClientSecret = "dqwDhoeEtmjKBJKEeqVMoaB9";
            //    });


            //DB
           
            services.AddScoped<IEMailRepository, EMailDbRepository>();
            services.AddScoped<IScientificLevelRepository, ScientificLevelDbRepository>();
            services.AddScoped<ICountryRepository, CountryDbRepository>();
            services.AddScoped<INationalityRepository, NationalityDbRepository>();
            services.AddScoped<IAcademicDegreeRepository, AcademicDegreeDbRepository>();
            services.AddScoped<IPlaceRepository, PlaceDbRepository>();
            services.AddScoped<IPlaceUserRepository, PlaceUserDbRepository>();
            services.AddScoped<ISettingRepository, SettingDbRepository>();

            services.AddScoped<IContactMessageRepository, ContactMessageDbRepository>();
            services.AddScoped<IEventsRepository, EventsDbRepository>();
            services.AddScoped<IEventActivityRepository, EventActivityDbRepository>();
            services.AddScoped<IEventImagesRepository, EventImagesDbRepository>();

            services.AddScoped<INewsRepository, NewsDbRepository>();













            services.AddSingleton<DataProtectionPurposeStrings>();

            services.AddDbContextPool<ApplicationDbContext>(
              options => options.UseSqlServer(Configuration.GetConnectionString("SSLDCConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
             .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconfirmation");
            ;
            //change password complexity use this or add from lamda expression on services.AddIdentity<IdentityUser, IdentityRole>((options =>{options.Password.RequireLowercase = false;}))
            services.Configure<IdentityOptions>(options =>
            {
                //options.Password.RequireLowercase = false;
                //options.Password.RequiredLength = 1;
                //options.Password.RequireDigit = false;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireUppercase = false;
                //options.Password.RequireLowercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
            })
            ;
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
              opt.TokenLifespan = TimeSpan.FromHours(2));

            services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
                opt.TokenLifespan = TimeSpan.FromDays(3));
            var emailConfig = Configuration
            .GetSection("EmailConfiguration")
            .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //page not found
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/PageNotFound";
                    await next();
                }
            });
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();

            app.UseRouting();

            //


            app.UseAuthentication();
            app.UseAuthorization();

            // you may need

            ////app.UseHttpsRedirection();
            ////app.UseStaticFiles();
            ////app.UseCookiePolicy();
            ////app.UseAuthentication();
            //you may need 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Default}/{id?}");
            });
        }
    }
}
