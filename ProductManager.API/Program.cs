using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductManager.BLL.Interfaces;
using ProductManager.BLL.Services;
using ProductManager.DAL;

namespace ProductManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("ProductDb"));
            //builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=products.db"));



            builder.Services.AddScoped<IProductService, ProductService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();


            app.MapControllers();


            app.Run();
        }
    }
}
