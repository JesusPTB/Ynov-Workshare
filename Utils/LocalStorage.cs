using System;
using System.IO;
using Newtonsoft.Json;
using Ynov_Workshare.Models;

namespace Ynov_Workshare.Utils;

public class LocalStorage
{
    private static readonly string UserDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UserData.json");
    
    /// <summary>
    /// Fonction permettant de sauvegarder les données d'un utilisateur en cache
    /// </summary>
    /// <param name="userData">les infos de l'utilisateur à stocker</param>
    public void SaveUserData(User userData)
    {
        try
        {
            string json = JsonConvert.SerializeObject(userData);
            File.WriteAllText(UserDataPath, json);
        }
        catch (Exception ex)
        {
            // Gérer l'exception, par exemple en la journalisant ou en affichant un message d'erreur à l'utilisateur
            Console.WriteLine($"Error saving user data: {ex.Message}");
        }
    }
    /// <summary>
    /// Methode Permettant de recupérer les infos de l'utilisateur depuis la memoire cache
    /// </summary>
    /// <returns></returns>
    public User? LoadUserData()
    {
        if (!File.Exists(UserDataPath)) return null;
        try
        {
            var json = File.ReadAllText(UserDataPath);
            return JsonConvert.DeserializeObject<User>(json);
        }
        catch (Exception ex)
        {
            // Gérer l'exception, par exemple en la journalisant ou en affichant un message d'erreur à l'utilisateur
            Console.WriteLine($"Error loading user data: {ex.Message}");
        }
        return null;
    }

    public static void ClearCache()
    {
        try
        {
            if (File.Exists(UserDataPath))
            {
                File.Delete(UserDataPath);
            }
        }
        catch (Exception ex)
        {
            // Gérer l'exception, par exemple en la journalisant ou en affichant un message d'erreur à l'utilisateur
            Console.WriteLine($"Error clearing cache: {ex.Message}");
        }
    }

}