
using app.Application.Services;
using app.Domain.Interfaces;
using app.Infrastructure.Clients;
using app.Infrastructure.Repositories;

namespace app.Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.Logging.AddConsole();

			// Add services to the container.
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddMemoryCache();

			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IUserService, UserService>();

			builder.Services.AddHttpClient<IHighfieldClient, HighfieldClient>(client =>
			{
				client.BaseAddress = new Uri(builder.Configuration["HighfieldAPI:BaseAddress"] ?? throw new InvalidOperationException());
			});

			var app = builder.Build();

			app.UseDefaultFiles();
			app.UseStaticFiles();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.MapFallbackToFile("/index.html");

			app.Run();
		}
	}
}
