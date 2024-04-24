using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Ynov_Workshare.Models;

namespace Ynov_Workshare;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:8080");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    private void CheckTokenValidity()
    {
        // Implémentez la logique pour vérifier la validité du jeton ici
    }

    private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"HTTP Error {(int)response.StatusCode}: {response.ReasonPhrase}");
        }
        //var obj =  response.Content.ReadFromJsonAsync<UserDto>();
        using (var responseStream = await response.Content.ReadAsStreamAsync())
        {
            return await JsonSerializer.DeserializeAsync<T>(responseStream);
        }
    }
    
    public async Task<T> Get<T>(string endpoint, string? token)
    {
        //CheckTokenValidity();
        
        if(token !=null) 
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        using var response = await _httpClient.GetAsync(endpoint);
        
        response.EnsureSuccessStatusCode();
        return await DeserializeResponse<T>(response);
    }
    
    public async Task<HttpResponseMessage?> Post<T>(string endpoint, object data, string? token = null)
    {
        //CheckTokenValidity();
    
        if(token != null)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync(endpoint, data);
        var obj =  response.Content.ReadFromJsonAsync<UserDto>();
        response.EnsureSuccessStatusCode();
        return response;

    }

    public async Task<T> Put<T>(string endpoint, object data, string? token = null)
    {
        //CheckTokenValidity();
    
        if(token != null)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        using var response = await _httpClient.PutAsJsonAsync(endpoint, data);
        
        response.EnsureSuccessStatusCode();
        return await DeserializeResponse<T>(response);
    }

    public async Task<T> Delete<T>(string endpoint, string? token = null)
    {
        //CheckTokenValidity();
    
        if(token != null)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        using var response = await _httpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return await DeserializeResponse<T>(response);
    }

}