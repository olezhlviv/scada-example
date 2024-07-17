using System.Net;
using NUnit.Framework;
using Scada.FakeRestApi.Api.Apis;
using Scada.FakeRestApi.Api.Models;

namespace Scada.FakeRestApi.Tests.Activities;

public class GetActivitiesTests : BaseTest<GetActivitiesRepository>
{
    private readonly ActivitiesApi _activitiesApi = new ActivitiesApi(TestConfiguration.BaseUrl);
    
    protected override string JsonFileName => "Activities/GetActivitiesData.json";

    [TestCase(TestName = "Verify amount of activities count with no specified date.")]
    public void VerifyAmountOfActivitiesCountWithNoSpecifiedDueDate()
    {
        var dateWithOffset = DateTime.UtcNow.AddDays(Repository.DayOffsetFromToday).Date;
        
        var response = _activitiesApi.GetActivities();
        
        Assert.Multiple(() =>
        {
            AssertThatResponseIsSuccessful(response, HttpStatusCode.OK);
            Assert.That(response.JsonData, Has.Count.EqualTo(Repository.NumberOfActivities), "Incorrect count of activities.");
            Assert.That(response.JsonData, Has.None.Matches<ActivityEntity>(activity => activity.DueDate.Date.Equals(dateWithOffset)), "Response contains activity from specified date.");
        });
    }
}

[Serializable]
public class GetActivitiesRepository
{
    public int DayOffsetFromToday { get; set; }
    public int NumberOfActivities { get; set; }
}