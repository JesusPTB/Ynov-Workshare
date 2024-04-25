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
    
    /// <summary>
    /// Implementation de la requête get
    /// </summary>
    /// <param name="endpoint">l'url particulier sur laquelle la requete doit etre faite</param>
    /// <param name="token">Le token permettant de verifier si on peut acceder à ces données.
    /// Il n'est pas obligatoire.</param>
    /// <returns></returns>
    public async Task<HttpResponseMessage?> Get(string endpoint, string? token)
    {
        //CheckTokenValidity();
        
        if(token !=null) 
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync(endpoint);
        
        response.EnsureSuccessStatusCode();
        return response;
    }
    
    /// <summary>
    /// Implementation générique de la methode post
    /// </summary>
    /// <param name="endpoint">l'url particulier sur laquelle la requete doit etre faite</param>
    /// <param name="data">La donnée à ajouter</param>
    /// <param name="token">Le token permettant de verifier si on peut acceder à ces données.
    /// Il n'est pas obligatoire.</param>
    /// <typeparam name="T">Le type de la donnée à ajouter</typeparam>
    /// <returns></returns>
    public async Task<HttpResponseMessage?> Post<T>(string endpoint, T data, string? token = null)
    {
        //CheckTokenValidity();
    
        if(token != null)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsJsonAsync(endpoint, data);
       
        response.EnsureSuccessStatusCode();
        return response;

    }

    /// <summary>
    /// Implementation générique de la methode put
    /// </summary>
    /// <param name="endpoint"></param>
    /// <param name="data"></param>
    /// <param name="token"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<HttpResponseMessage?> Put<T>(string endpoint, T data, string? token = null)
    {
        //CheckTokenValidity();
    
        if(token != null)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsJsonAsync(endpoint, data);
        
        response.EnsureSuccessStatusCode();
        return response;
    }

    /// <summary>
    /// Implementation générique de la methode Delete
    /// </summary>
    /// <param name="endpoint"></param>
    /// <param name="token"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<HttpResponseMessage?> Delete(string endpoint, string? token = null)
    {
        //CheckTokenValidity();
    
        if(token != null)
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();
        return response;
    }

}