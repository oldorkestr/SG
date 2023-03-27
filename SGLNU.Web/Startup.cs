using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.EF;
using SGLNU.DAL.Entities;
using SGLNU.DAL.Interfaces;
using SuLNU.Web.Models.Interfaces;
using SuLNU.Web.Models.Services;
using SuLNU.Web.Models.Settings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SGLNU.BLL.Services;
using Microsoft.Extensions.Options;
using SGLNU.BLL.Configs;
using SGLNU.BLL.Infrastructure.MapperProfiles;

namespace SGLNU.Web
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
            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SuLnuDbContext>();

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"));

            services.AddControllersWithViews(
            options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddDbContext<SuLnuDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SuLnuDBConnection")));

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new NewsProfile());
                mc.AddProfile(new FacultyProfile());
                mc.AddProfile(new EventProfile());
                mc.AddProfile(new VotingProfile());
                mc.AddProfile(new VoteProfile());
                mc.AddProfile(new CandidateProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IEmailConfirmation, EmailConfirmation>();
            services.Configure<EmailServiceSettings>(Configuration.GetSection("EmailServiceSettings"));
            services.AddRazorPages()
                    .AddMicrosoftIdentityUI();

            services.AddTransient<IUnitOfWork, EFUnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISignInService, SignInService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IFacultyService, FacultyService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IVotingService, VotingService>();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            CreateUserRoles(serviceProvider).Wait();
        }
        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in new string[] { "NotAppliedMember", "ActiveMember", "FacultyMember", "FacultySecretary", "AlternateFacultyAdmin", "FacultyAdmin", "Secretary", "AlternateAdmin", "Admin" })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
