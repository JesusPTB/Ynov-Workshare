using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ynov_Workshare.Utils;

namespace Ynov_Workshare.Api;

public class Auth
{
    private HttpUtils _httpUtils = new HttpUtils();
    
    public async Task<HttpResponseMessage> LoginAsync(string username, string password)
    {
        // Exemple d'URL et de données à envoyer dans la requête POST pour la connexion
        string apiUrl = "/api/userslogin";
        var requestData = new { Username = username, Password = password };

        // Envoi de la requête POST avec les données de connexion
        HttpResponseMessage response = await _httpUtils._client.PostAsJsonAsync(apiUrl, requestData);

        return response;
    }
}