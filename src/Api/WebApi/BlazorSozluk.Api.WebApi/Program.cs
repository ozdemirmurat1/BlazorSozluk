using BlazorSozluk.Api.Application.Extensions;
using BlazorSozluk.Api.WebApi.Infrastructure.ActionFilters;
using BlazorSozluk.Api.WebApi.Infrastructure.Extensions;
using BlazorSozluk.Infrastructure.Persistence.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BlazorSozluk.Api.WebApi
{
    public class Program
    {
        [Obsolete]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
                .AddControllers(opt=>opt.Filters.Add<ValidateModelStateFilter>())
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                })
                .AddFluentValidation(); 

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.ConfigureAuth(builder.Configuration);

            builder.Services.AddApplicationRegistration();
            builder.Services.AddInfrastructureRegistration(builder.Configuration);

            //AddCors

            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.ConfigureExceptionHandling(app.Environment.IsDevelopment());


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors("MyPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}