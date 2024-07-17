using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace Scada.Framework.Core.Rest;

/// <summary>
/// Represents a REST API response.
/// </summary>
public class RestApiResponse<TData>
{
    private RestResponse<TData> RestResponseWrapper { get; }

    internal RestApiResponse(RestResponse<TData> restResponseWrapper)
    {
        RestResponseWrapper = restResponseWrapper;
    }
    
    /// <summary>
    /// Gets the status code of the REST response.
    /// </summary>
    public bool IsResponseCompleted => RestResponseWrapper.ResponseStatus == ResponseStatus.Completed;

    /// <summary>
    /// Gets the deserialized JSON data.
    /// </summary>
    public TData? JsonData => RestResponseWrapper.Data;

    /// <summary>
    /// Gets the status code of the REST response.
    /// </summary>
    public HttpStatusCode StatusCode => RestResponseWrapper.StatusCode;

    public override string ToString()
    {
        return JsonConvert.SerializeObject(RestResponseWrapper, Formatting.Indented);
    }
}