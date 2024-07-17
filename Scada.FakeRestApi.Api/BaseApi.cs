namespace Scada.FakeRestApi.Api;

/// <summary>
/// Represents a base API structure.
/// </summary>
public abstract class BaseApi
{
    protected abstract string ApiName { get; }
    protected abstract string RelativeUrl { get; }
    
    protected string BaseUrl { get; }

    private protected BaseApi(string baseUrl)
    {
        BaseUrl = baseUrl;
    }
}