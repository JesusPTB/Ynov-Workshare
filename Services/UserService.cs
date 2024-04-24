using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ynov_Workshare.Models;

namespace Ynov_Workshare;


public class UserService
{
    
    private readonly HttpClient _httpClient;
    public UserService(ApiService apiService)
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:8080");
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
    
    public string[] Paths = ["api/Users","/Login"];
    
    public async Task<UserDto?> Login(LoginForm user)
    {
        //if(token != null)
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync(Paths[0] + Paths[1], user);
        response.EnsureSuccessStatusCode();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"HTTP Error {(int)response.StatusCode}: {response.ReasonPhrase}");
        }
        
        var obj =  response.Content.ReadFromJsonAsync<UserDto>();
        
        return obj.Result;
    }
}