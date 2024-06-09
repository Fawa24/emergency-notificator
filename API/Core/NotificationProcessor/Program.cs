using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationProcessor;
using RabbitMQ.Client;

public static class Program
{
	public static void Main()
	{
		CreateHostBuider().Build().Run();
	}

	private static IHostBuilder CreateHostBuider() =>
		Host.CreateDefaultBuilder()
		.ConfigureServices((hostContext, services) =>
		{
			services.AddSingleton<ConnectionFactory>();
			services.AddDbContext<ApplicationDbContext>();
			services.AddScoped<NotificationRepository>();
			services.AddHostedService<NotificationProcessorService>();
		});
}