using RestSharp;
using Serilog;

namespace Scada.Framework.Core.Rest;

/// <summary>
/// Represents a REST API client.
/// </summary>
public sealed class RestApiClient
{
    public static ThreadLocal<string> LastRequest = new ();
    public static ThreadLocal<string> LastResponse = new ();
    
    private RestClient RestClientWrapper { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RestApiClient"/> class with the specified base url.
    /// </summary>
    /// <param name="baseUrl">The base url for the API.</param>
    public RestApiClient(string baseUrl)
    {
        RestClientWrapper = new RestClient(baseUrl);
    }
    
    /// <summary>
    /// Executes a REST API request.
    /// </summary>
    /// <param name="request">The <see cref="RestApiRequest"/> object containing the request details.</param>
    /// <typeparam name="TData">Type of data for JSON deserialization.</typeparam>
    /// <returns>The response received from the REST API.</returns>
    public RestApiResponse<TData> Execute<TData>(RestApiRequest request)
    {
        var responseWrapper = RestClientWrapper.Execute<TData>(request.RestRequestWrapper);
        var response = new RestApiResponse<TData>(responseWrapper); 

        LastRequest.Value = request.ToString();
        LastResponse.Value = response.ToString();
        
        return response;
    }
}