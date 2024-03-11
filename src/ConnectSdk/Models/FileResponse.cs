﻿namespace ConnectSdk.Models;

public class FileResponse : System.IDisposable
{
    private System.IDisposable _client;
    private System.IDisposable _response;

    public int StatusCode { get; private set; }

    public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

    public System.IO.Stream Stream { get; private set; }

    public bool IsPartial
    {
        get { return StatusCode == 206; }
    }

    public FileResponse(int statusCode, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.IO.Stream stream, System.IDisposable client, System.IDisposable response)
    {
        StatusCode = statusCode;
        Headers = headers;
        Stream = stream;
        _client = client;
        _response = response;
    }

    public void Dispose()
    {
        Stream.Dispose();
        if (_response != null)
            _response.Dispose();
        if (_client != null)
            _client.Dispose();
    }
}