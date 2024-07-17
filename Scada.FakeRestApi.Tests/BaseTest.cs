using System.Net;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Bogus;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Scada.Framework.Core.Rest;
using Serilog;

[assembly: LevelOfParallelism(3)]

namespace Scada.FakeRestApi.Tests;

[AllureNUnit]
[Parallelizable(ParallelScope.Fixtures)]
public abstract class BaseTest<TRepository>
{
    protected abstract string JsonFileName { get; }

    protected Faker DataFaker { get; } = new();
    protected Randomizer DataRandomizer { get; } = new();
    protected static TRepository Repository { get; private set; }
    
    [OneTimeSetUp]
    [AllureBefore]
    public void BeforeAllTests()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        Repository = typeof(TRepository) == typeof(EmptyRepository)
            ? default!
            : GetTestDataFromJson();
    }

    [TearDown]
    public void AfterTest()
    {
        if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure))
        {
            Log.Debug("Request: {Request}", RestApiClient.LastRequest.Value);
            Log.Debug("Response: {Response}", RestApiClient.LastResponse.Value);
        }
        Log.Information("----------------------------------------------------------------------------------------------------");
        Log.Information("{TestName} Test Status: [{TestStatus}]", TestContext.CurrentContext.Test.MethodName, TestContext.CurrentContext.Result.Outcome);
        Log.Information("====================================================================================================");
    }

    protected void AssertThatResponseIsSuccessful<T>(RestApiResponse<T> response, HttpStatusCode statusCode)
    {
        Assert.That(response.IsResponseCompleted, Is.True, "Response is not completed.");
        Assert.That(response.StatusCode, Is.EqualTo(statusCode), "Incorrect status code.");
    }

    private TRepository GetTestDataFromJson()
    {
        var jsonText = File.ReadAllText(JsonFileName);
        var repository = JsonConvert.DeserializeObject<TRepository>(jsonText);
        return repository!;
    }
}

public sealed class EmptyRepository {
}