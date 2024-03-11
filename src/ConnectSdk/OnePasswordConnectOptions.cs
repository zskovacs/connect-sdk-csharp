using System;
using System.Net.Http.Headers;

namespace ConnectSdk;

public class OnePasswordConnectOptions : BaseClientOptions
{
    private string _apiKey;
    
    public string ApiKey
    {
        get => _apiKey;

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(_apiKey));
            }

            _apiKey = value;
            Auth = new AuthenticationHeaderValue("Bearer", _apiKey);
        }
    }
}