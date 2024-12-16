
using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

using Todo.Application.Interfaces;
using Todo.Core.Repository;
using Todo.Infrastructure.Persistence;

namespace Todo.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                            .AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                            });
            //builder.Services.AddDbContext<TodoContext>(options =>
            //                                           options.UseCosmos(builder.Configuration.GetConnectionString("DefaultConnection") ?? 
            //                                           "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
            //                                           "TodosDB"));
            builder.Services.AddDbContext<TodoContext>(options =>
                                                       options.UseCosmos(
                                                       "https://localhost:8081",
                                                       "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                                                       "TodosDB"));

            builder.Services.AddScoped<ITodoRepository, CosmosDbRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            using (var scopeAsync = app.Services.CreateAsyncScope())
            {
                var dbContext = scopeAsync.ServiceProvider.GetRequiredService<TodoContext>();
                dbContext.Database.EnsureDeletedAsync();
                dbContext.Database.EnsureCreatedAsync();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
