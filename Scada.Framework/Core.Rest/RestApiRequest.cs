using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace Scada.Framework.Core.Rest;

/// <summary>
/// Represents a REST API request.
/// </summary>
public sealed class RestApiRequest
{
    private static readonly Dictionary<HttpMethod, Method> HttpMethodToRestSharpMethodMap = new()
    {
        { HttpMethod.Delete, Method.Delete },
        { HttpMethod.Get, Method.Get },
        { HttpMethod.Post, Method.Post },
        { HttpMethod.Put, Method.Put }
    };
    
    internal RestRequest RestRequestWrapper { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RestApiRequest"/> class with specified base url.
    /// </summary>
    /// <param name="relativeUrl">The relative url for API request.</param>
    /// <param name="httpMethod">The HTTP method for API request.</param>
    public RestApiRequest(string relativeUrl, HttpMethod httpMethod)
    {
        RestRequestWrapper = new RestRequest(relativeUrl, HttpMethodToRestSharpMethodMap[httpMethod]);
        RestRequestWrapper.CookieContainer = new CookieContainer();
    }

    /// <summary>
    /// Adds object as JSON body.
    /// </summary>
    /// <param name="obj">Object to be deserialized to JSON.</param>
    /// <typeparam name="T">Type of object for deserialization.</typeparam>
    public void AddObjectAsJsonBody<T>(T obj) where T : class
    {
        RestRequestWrapper.AddJsonBody(obj);
    }
    
    public override string ToString()
    {
        return JsonConvert.SerializeObject(RestRequestWrapper, Formatting.Indented);
    }
}