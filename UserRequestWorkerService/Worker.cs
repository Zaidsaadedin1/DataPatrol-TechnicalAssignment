using Microsoft.EntityFrameworkCore;
using Cores.ApplicationDbContext;
using Cores.Enums;

public class Worker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Worker> _logger;

    public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDb>();

            var request = await db.UserRequests
                .Include(r => r.RequestedByUser)
                .Where(r => r.RequestDateTime <= DateTime.UtcNow && r.Status == (int)RequestStatus.Pending)
                .OrderBy(r => r.RequestDateTime)
                .FirstOrDefaultAsync(stoppingToken);

            if (request != null)
            {
                try
                {
                    request.Status = (int)RequestStatus.InReview;
                    await db.SaveChangesAsync(stoppingToken);

                    if (!request.RequestedByUser.IsEnabled)
                    {
                        request.Status = (int)RequestStatus.Rejected;
                    }
                    else if (request.RequestCode % 4 == 0)
                    {
                        request.Status = (int)RequestStatus.Rejected;
                    }
                    else
                    {
                        request.Status = (int)RequestStatus.Approved;
                    }

                    request.CompletionDateTime = DateTime.UtcNow;
                    await db.SaveChangesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing request ID: {Id}", request.RequestId);
                    request.Status = (int)RequestStatus.Error;
                    request.CompletionDateTime = DateTime.UtcNow;
                    await db.SaveChangesAsync(stoppingToken);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
