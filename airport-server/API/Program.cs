using BL.Interfaces;
using BL.Models.Airportproject1.Services;
using BL.Repositories;
using DAL;
using DAL.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Serialization;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSignalR(op => op.EnableDetailedErrors = true);

		var connectionString = builder.Configuration.GetConnectionString("MAIN");

		builder.Services.AddDbContext<AirportDbContext>(b => b.UseSqlServer(connectionString));
		builder.Services.AddSingleton<IFlightRepository<Flight>,FlightRepository>();

		builder.Services.AddSingleton<IHubService,HubService>();
		builder.Services.AddSingleton<IRoute,Route>();
		builder.Services.AddSingleton<ISimulator,Simulator>(s =>
		{
			var signal = s.GetRequiredService<IHubService>();
			var route = s.GetRequiredService<IRoute>();
			var repo = s.GetRequiredService<IFlightRepository<Flight>>();
			return new Simulator(signal,route ,repo);
		});

		builder.Services.AddCors();
		var app = builder.Build();

		using (var scope = app.Services.CreateScope())
		{
			var ctx = scope.ServiceProvider.GetRequiredService<AirportDbContext>();
			ctx.Database.EnsureDeleted();
			ctx.Database.EnsureCreated();
		}


		app.UseCors(s => s.WithOrigins("http://localhost:3000")
		.AllowAnyHeader().WithMethods("GET", "POST").AllowCredentials());

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.MapHub<AirportHub>("/airporthub");
		app.Run();
	}
}