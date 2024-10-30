using Microsoft.Extensions.Diagnostics.HealthChecks;
using StudentBloggAPI.Data;

namespace StudentBloggAPI.Health;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly StudentBloggDbContext _dbContext;

    public DatabaseHealthCheck(StudentBloggDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            if (await _dbContext.Database.CanConnectAsync(cancellationToken))
            {
                return HealthCheckResult.Healthy("Database connection is healthy");
            }
            else
            {
                return HealthCheckResult.Unhealthy("Database connection is not healthy");
            }
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy("Database connection failed!", e);
        }
    }
}