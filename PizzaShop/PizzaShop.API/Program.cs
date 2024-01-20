
using Microsoft.EntityFrameworkCore;
using PizzaShop.API.Calculations;
using PizzaShop.API.Services;
using PizzaShop.Data.Database;
using PizzaShop.Data.Repositories;

namespace PizzaShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PizzaShopContext>(options => 
            {
                options.UseInMemoryDatabase("PizzaShop");
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPizzaOrderRepository, PizzaOrderRepository>();
            builder.Services.AddScoped<IPizzaOrderService, PizzaOrderService>();

            var corsOptionsName = "PizzaShopCORS";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: corsOptionsName,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader();
                                  });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(corsOptionsName);

            app.MapControllers();

            app.Run();
        }
    }
}