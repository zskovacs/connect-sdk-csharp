using System.Net;
using ConnectSdk.Reliability;
using System;
using System.Net.Http;

namespace ConnectSdk;

/// <summary>
/// The base class for interacting with 1Password Connect API.
/// </summary>
public partial class BaseClient : IBaseClient
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseClient"/> class.
    /// </summary>
    /// <param name="options">A <see cref="BaseClientOptions"/> instance that defines the configuration settings to use with the client.</param>
    protected BaseClient(BaseClientOptions options)
        : this(httpClient: null, options)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseClient"/> class.
    /// </summary>
    /// <param name="webProxy">Web proxy.</param>
    /// <param name="options">A <see cref="BaseClientOptions"/> instance that defines the configuration settings to use with the client.</param>
    protected BaseClient(IWebProxy webProxy, BaseClientOptions options)
        : this(CreateHttpClientWithWebProxy(webProxy, options), options)
    {
        if(options is null)
            throw new ArgumentNullException(nameof(options));
        
        BaseUrl = options.BaseUrl + '/' + options.Version + '/';
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseClient"/> class.
    /// </summary>
    /// <param name="httpClient">An optional HTTP client which may me injected in order to facilitate testing.</param>
    /// <param name="options">A <see cref="BaseClientOptions"/> instance that defines the configuration settings to use with the client.</param>
    protected BaseClient(HttpClient httpClient, BaseClientOptions options)
    {
        if(options is null)
            throw new ArgumentNullException(nameof(options));
        
        _httpClient = httpClient ?? CreateHttpClientWithRetryHandler(options);
        BaseUrl = options.BaseUrl + '/' + options.Version + '/';
    }

    private static HttpClient CreateHttpClientWithRetryHandler(BaseClientOptions options)
    {
        var client = new HttpClient(new RetryDelegatingHandler(new HttpClientHandler(), options.ReliabilitySettings));
        client.DefaultRequestHeaders.Authorization = options.Auth;
        return client;
    }

    /// <summary>
    /// Create HttpClient with WebProxy
    /// </summary>
    /// <param name="webProxy">WebProxy object</param>
    /// <param name="options">A <see cref="BaseClientOptions"/> instance that defines the configuration settings to use with the client.</param>
    /// <returns>HttpClient with RetryDelegatingHandler and WebProxy if set.</returns>
    private static HttpClient CreateHttpClientWithWebProxy(IWebProxy webProxy, BaseClientOptions options)
    {
        if (webProxy != null)
        {
            var httpClientHandler = new HttpClientHandler() { Proxy = webProxy, PreAuthenticate = true, UseDefaultCredentials = false, };

            var retryHandler = new RetryDelegatingHandler(httpClientHandler, options.ReliabilitySettings);

            var client = new HttpClient(retryHandler);
            client.DefaultRequestHeaders.Authorization = options.Auth;
            return client;
        }
        else
        {
            return CreateHttpClientWithRetryHandler(options);
        }
    }
}