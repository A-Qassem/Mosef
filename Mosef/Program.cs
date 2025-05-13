using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;

namespace Mosef
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });



            // Register DbContext before Build()
            builder.Services.AddDbContext<MosefDbContext>(options =>
options.UseSqlServer("Server=db19546.public.databaseasp.net; Database=db19546; User Id=db19546; Password=Lm7+t2=J_T6j; Encrypt=False; MultipleActiveResultSets=True;"));

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
