using Microsoft.EntityFrameworkCore;
using Cores.ApplicationDbContext;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<ApplicationDb>(options =>
            options.UseSqlServer(
                hostContext.Configuration.GetConnectionString("DefaultConnection")));

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
