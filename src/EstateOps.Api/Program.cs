using EstateOps.Infrastructure;
using EstateOps.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
  var databaseProvisioner = scope.ServiceProvider.GetRequiredService<DatabaseProvisioner>();
  await databaseProvisioner.EnsureDatabaseExistsAsync();
}

app.MapGet("/api/health", () => TypedResults.Ok(new HealthResponse("ok", "EstateOps.Api")))
  .WithName("GetHealth");

app.MapGet("/api/health/ready", async Task<Results<Ok<HealthResponse>, ProblemHttpResult>> (
    EstateOpsDbContext dbContext,
    CancellationToken cancellationToken) =>
  {
    var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);
    return canConnect
      ? TypedResults.Ok(new HealthResponse("ready", "EstateOps.Api"))
      : TypedResults.Problem("Database connection check failed.", statusCode: StatusCodes.Status503ServiceUnavailable);
  })
  .WithName("GetReadiness");

app.Run();

public sealed record HealthResponse(string Status, string Service);
