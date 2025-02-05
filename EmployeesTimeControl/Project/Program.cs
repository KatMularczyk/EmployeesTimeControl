

using EmployeesTimeControl.Repositories;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Web.Http.ExceptionHandling;

namespace EmployeesTimeControl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,    
                        ValidateIssuerSigningKey = true,    
                        ValidIssuer = "http://localhost:5126",
                        ValidAudience = "http://localhost:5126",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5BF956B8D2846F1A71482627EDACF778356E099253F0EACCF6BEA49DA1EE310A6E071E0477A85D74D9E184C639477AC3DA7BA609A160F06BA0F1BB6A520CCD76"))
                    };
                });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<ITimeEntriesRepository, TimeEntriesRepository>();
            builder.Services.AddScoped<ILoginInfoRepository, LoginInfoRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           app.UseExceptionHandler(appError => //global error handler
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null) 
                    { 
                        Console.WriteLine($"Error: { contextFeature.Error}");

                        await context.Response.WriteAsJsonAsync(new
                        {
                            StatusCodeContext = context.Response.StatusCode,
                            Message = "Something went wrong"
                        });
                    }
                });
            });
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
