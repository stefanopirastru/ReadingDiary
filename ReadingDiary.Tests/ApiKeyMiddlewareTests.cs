using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using ReadingDiary.Api.Middleware;

namespace ReadingDiary.Tests;

public class ApiKeyMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_ShouldReturn401_WhenApiKeyIsMissing()
    {
        var context = new DefaultHttpContext();

        var inMemorySettings =
            new Dictionary<string, string?>
            {
                { "ApiKey", "secret-key" }
            };

        IConfiguration configuration =
            new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

        var middleware =
            new ApiKeyMiddleware(
                (innerHttpContext) => Task.CompletedTask,
                configuration);

        await middleware.InvokeAsync(context);

        Assert.Equal(
            StatusCodes.Status401Unauthorized,
            context.Response.StatusCode);
    }
}