using crims_api;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;
using Crims.Data.Persistence;

var host = CreateHostBuilder(args).Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occured while migratin or seeding the database");
        throw;
    }
}
await host.RunAsync();

static IHostBuilder CreateHostBuilder(string[] args)
{
    var hostBuilder = Host.CreateDefaultBuilder(args);
    hostBuilder.UseSerilog((context, configuration) =>
    {
        configuration.Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Debug()
            .WriteTo.Console()
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
            .ReadFrom.Configuration(context.Configuration);
    });
    hostBuilder.ConfigureWebHostDefaults(webBuilder =>
    {
        var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
        webBuilder.UseUrls($"http://*:{port}");
        webBuilder.UseStartup<Startup>();
    });
    return hostBuilder;
}