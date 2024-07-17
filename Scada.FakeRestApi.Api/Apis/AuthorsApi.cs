using Scada.FakeRestApi.Api.Models;
using Scada.Framework.Core.Rest;
using Serilog;

namespace Scada.FakeRestApi.Api.Apis;

/// <summary>
/// Represents the "Authors" API.
/// </summary>
public class AuthorsApi : BaseApi
{
    protected override string ApiName => "\"Authors\" API";
    protected override string RelativeUrl => "/api/v1/Authors";
    
    public AuthorsApi(string baseUrl) 
        : base(baseUrl)
    {
    }

    public RestApiResponse<AuthorEntity> CreateAuthor(AuthorEntity author)
    {
        Log.Information("{ApiName}: Creating [{Author}] author.", ApiName, author);

        var client = new RestApiClient(BaseUrl);
        RestApiRequest request = new RestApiRequest(RelativeUrl, HttpMethod.Post);
        request.AddObjectAsJsonBody(author);
        var response = client.Execute<AuthorEntity>(request);
        return response;
    }
    
    public RestApiResponse<AuthorEntity> GetAuthor(int id)
    {
        Log.Information("{ApiName}: Getting [{Id}] author.", ApiName, id);

        var client = new RestApiClient(BaseUrl);
        RestApiRequest request = new RestApiRequest($"{RelativeUrl}/{id}", HttpMethod.Get);
        var response = client.Execute<AuthorEntity>(request);
        return response;
    }
    
    public RestApiResponse<AuthorEntity> DeleteAuthor(int id)
    {
        Log.Information("{ApiName}: Deleting [{Id}] author.", ApiName, id);

        var client = new RestApiClient(BaseUrl);
        RestApiRequest request = new RestApiRequest($"{RelativeUrl}/{id}", HttpMethod.Delete);
        var response = client.Execute<AuthorEntity>(request);
        return response;
    }
}