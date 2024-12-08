using ApiBackend.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace ApiBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<recensiiContext>(
                options => options.UseSqlServer(builder.Configuration["ConnectionString"]));
          // Add services to the container.

          builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<recensiiContext>();
                context.Database.Migrate();
            }
               


                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

			// app.UseHttpsRedirection();

            app.UseCors(builder => builder.WithOrigins(new[] {"https://localhost:7054/", })
            .AllowAnyHeader() 
            .AllowAnyMethod());



		app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
