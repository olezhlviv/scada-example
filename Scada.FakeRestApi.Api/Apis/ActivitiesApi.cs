using Scada.FakeRestApi.Api.Models;
using Scada.Framework.Core.Rest;
using Serilog;

namespace Scada.FakeRestApi.Api.Apis;

/// <summary>
/// Represents the "Activities" API.
/// </summary>
public class ActivitiesApi : BaseApi
{
    protected override string ApiName => "\"Activities\" API";
    protected override string RelativeUrl => "/api/v1/Activities";
    
    public ActivitiesApi(string baseUrl) 
        : base(baseUrl)
    {
    }

    public RestApiResponse<List<ActivityEntity>> GetActivities()
    {
        Log.Information("{ApiName}: Getting all activities.", ApiName);

        var client = new RestApiClient(BaseUrl);
        RestApiRequest request = new RestApiRequest(RelativeUrl, HttpMethod.Get);
        var response = client.Execute<List<ActivityEntity>>(request);
        return response;
    }
}