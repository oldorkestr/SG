using SGLNU.DAL.EF;
using SGLNU.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SGLNU.BLL.Infrastructure.MapperProfiles;

namespace SGLNU.BLL.Infrastructure
{
    public static class IServiceCollectionDIExtension
    {
        public static void AddDALDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SuLnuDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SuLnuDbContext>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
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

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}