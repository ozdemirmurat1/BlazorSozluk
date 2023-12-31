﻿using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Infrastructure.Persistence.Context;
using BlazorSozluk.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSozluk.Infrastructure.Persistence.Extensions
{
    public static class Registration
    {
        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<BlazorSozlukContext>(conf =>
            {
                var connStr = configuration["BlazorSozlukDbConnectionString"].ToString();
                conf.UseSqlServer(connStr, opt =>
                {
                    opt.EnableRetryOnFailure();
                });
            });

            // Uygulama ilk kez ayağa kalkarken çalıştırdık. Sonra comment e aldık
            //var seedData = new SeedData();
            //seedData.SeedAsync(configuration).GetAwaiter().GetResult();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
            services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();
            services.AddScoped<IEntryRepository, EntryRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            return services;
        }
    }
}
