using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ConnectSdk;

/// <summary>
/// An HTTP client wrapper for interacting with 1Password Connect.
/// </summary>
public class OnePasswordConnectClient : BaseClient, IOnePasswordConnectClient
{
    private static readonly OnePasswordConnectOptions DefaultOptions = new();
    
    /// <summary>
    /// Initializes a new instance of the <see cref="OnePasswordConnectClient"/> class.
    /// </summary>
    /// <param name="webProxy">Web proxy.</param>
    /// <param name="apiKey">Your 1Password Connect API key.</param>
    /// <param name="baseUrl">Base url (e.g. http://connect.mycompany.com).</param>
    /// <param name="version">API version</param>
    public OnePasswordConnectClient(IWebProxy webProxy, string apiKey, string baseUrl = null, string version = null)
        : base(webProxy, BuildOptions(apiKey, baseUrl, version))
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="OnePasswordConnectClient"/> class.
    /// </summary>
    /// <param name="httpClient">An optional http client which may me injected in order to facilitate testing.</param>
    /// <param name="apiKey">Your 1Password Connect API key.</param>
    /// <param name="baseUrl">Base url (e.g. http://connect.mycompany.com).</param>
    /// <param name="version">API version</param>
    public OnePasswordConnectClient(HttpClient httpClient,string apiKey, string baseUrl = null, string version = null)
        : base(httpClient, BuildOptions(apiKey, baseUrl, version))
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="OnePasswordConnectClient"/> class.
    /// </summary>
    /// <param name="apiKey">Your 1Password Connect API key.</param>
    /// <param name="baseUrl">Base url (e.g. http://connect.mycompany.com).</param>
    /// <param name="version">API version</param>
    public OnePasswordConnectClient(string apiKey, string baseUrl = null, string version = null)
        : base(BuildOptions(apiKey, baseUrl, version))
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="OnePasswordConnectClient"/> class.
    /// </summary>
    /// <param name="options">A <see cref="OnePasswordConnectOptions"/> instance that defines the configuration settings to use with the client.</param>
    public OnePasswordConnectClient(OnePasswordConnectOptions options)
        : base(options)
    {
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="OnePasswordConnectClient"/> class.
    /// </summary>
    /// <param name="httpClient">An optional http client which may me injected in order to facilitate testing.</param>
    /// /// <param name="options">A <see cref="OnePasswordConnectOptions"/> instance that defines the configuration settings to use with the client.</param>
    public OnePasswordConnectClient(HttpClient httpClient, OnePasswordConnectOptions options)
        : base(httpClient, options)
    {
    }
    
    private static OnePasswordConnectOptions BuildOptions(string apiKey, string baseUrl, string version) =>
        new()
        {
            ApiKey = apiKey,
            BaseUrl = baseUrl ?? DefaultOptions.BaseUrl,
            Version = version ?? DefaultOptions.Version,
        };
}