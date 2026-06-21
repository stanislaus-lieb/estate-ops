namespace EstateOps.Api.Tests;

using Xunit;

public sealed class HealthEndpointTests
{
  [Fact]
  public void Scaffold_UsesApiAssembly()
  {
    var apiAssembly = typeof(global::HealthResponse).Assembly;

    Assert.Equal("EstateOps.Api", apiAssembly.GetName().Name);
  }
}
