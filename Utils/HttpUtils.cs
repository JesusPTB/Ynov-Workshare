using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Ynov_Workshare.Utils;

public class HttpUtils
{
    public HttpClient _client { get; set; } = new HttpClient();

    public HttpUtils()
    {
        _client.BaseAddress = new Uri("http://localhost:8080/");
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }
}