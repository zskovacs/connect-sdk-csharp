namespace ConnectSdk;

/// <summary>
/// Defines the options to use with the client.
/// </summary>
public class BaseClientOptions
{
    private ReliabilitySettings _reliabilitySettings = new();

    /// <summary>
    /// The reliability settings to use on HTTP Requests.
    /// </summary>
    public ReliabilitySettings ReliabilitySettings
    {
        get => _reliabilitySettings;
        set => _reliabilitySettings = value ?? throw new ArgumentNullException(nameof(_reliabilitySettings));
    }
    
    private string _baseUrl;
    /// <summary>
    /// The base URL.
    /// </summary>
    public string BaseUrl
    {
        get => _baseUrl;

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(_baseUrl));
            }

            _baseUrl = value;
        }
    }

    /// <summary>
    /// The API version (defaults to "v1").
    /// </summary>
    public string Version { get; set; } = "v1";


    /// <summary>
    /// The Auth header value.
    /// </summary>
    public AuthenticationHeaderValue Auth { get; set; }
}