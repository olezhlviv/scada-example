using Scada.FakeRestApi.Api.Models;
using Scada.Framework.Core.Rest;
using Serilog;

namespace Scada.FakeRestApi.Api.Apis;

/// <summary>
/// Represents the "Books" API.
/// </summary>
public class BooksApi : BaseApi
{
    protected override string ApiName => "\"Books\" API";
    protected override string RelativeUrl => "/api/v1/Books";
    
    public BooksApi(string baseUrl) 
        : base(baseUrl)
    {
    }
    
    public RestApiResponse<BookEntity> CreateBook(BookEntity book)
    {
        Log.Information("{ApiName}: Creating [{Book}] book.", ApiName, book);

        var client = new RestApiClient(BaseUrl);
        RestApiRequest request = new RestApiRequest(RelativeUrl, HttpMethod.Post);
        request.AddObjectAsJsonBody(book);
        var response = client.Execute<BookEntity>(request);
        return response;
    }
    
    public RestApiResponse<BookEntity> GetBook(int id)
    {
        Log.Information("{ApiName}: Getting [{Id}] book.", ApiName, id);

        var client = new RestApiClient(BaseUrl);
        RestApiRequest request = new RestApiRequest($"{RelativeUrl}/{id}", HttpMethod.Get);
        var response = client.Execute<BookEntity>(request);
        return response;
    }

    public RestApiResponse<BookEntity> UpdateBook(int id, BookEntity book)
    {
        Log.Information("{ApiName}: Updating [{Book}] book.", ApiName, book);

        var client = new RestApiClient(BaseUrl);
        RestApiRequest request = new RestApiRequest($"{RelativeUrl}/{id}", HttpMethod.Put);
        request.AddObjectAsJsonBody(book);
        var response = client.Execute<BookEntity>(request);
        return response;
    }
}