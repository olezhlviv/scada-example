using Microsoft.Extensions.Configuration;

namespace Scada.FakeRestApi.Tests;

public static class TestConfiguration
{
    private const string JsonFileName = "testConfiguration.json";

    private static readonly Lazy<IConfiguration> LazyConfig = new(() =>
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(JsonFileName);

        var config = builder.Build();
        return config;
    });

    public static string BaseUrl => LazyConfig.Value["BaseUrl"]!;
}