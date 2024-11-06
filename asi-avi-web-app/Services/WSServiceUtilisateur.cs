using asi_avi_web_app.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json.Serialization;

namespace asi_avi_web_app.Services
{
    public class WSServiceUtilisateur : IService<Utilisateur>
    {
        private readonly HttpClient httpClient;
        private readonly String nomControleur = "Utilisateurs";

        public WSServiceUtilisateur()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7043/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<bool> DeleteAsync(Utilisateur? utilisateur)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Utilisateur?>> GetAllAsync()
        {
            try
            {
                return await httpClient.GetFromJsonAsync<List<Utilisateur>>(nomControleur);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Utilisateur?> GetByIdAsync(int? id)
        {
            if (string.IsNullOrEmpty(nomControleur) || id == null)
                return null;

            try
            {
                string url = $"{nomControleur}/GetUtilisateurById/{id}";
                return await httpClient.GetFromJsonAsync<Utilisateur>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByIdAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<Utilisateur?> GetByEmailAsync(string? email)
        {
            if (string.IsNullOrEmpty(nomControleur) || string.IsNullOrEmpty(email))
                return null;

            try
            {
                string url = $"{nomControleur}/GetUtilisateurByEmail/{Uri.EscapeDataString(email)}";
                return await httpClient.GetFromJsonAsync<Utilisateur>(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetByEmailAsync: {ex.Message}");
                return null;
            }
        }


        public async Task<bool> PostAsync(Utilisateur? utilisateur)
        {
            try
            {

                Dictionary<string, object> nonNullProps = GetNonNullProperties(utilisateur);
                var response = await httpClient.PostAsJsonAsync(nomControleur, JsonConvert.SerializeObject(nonNullProps));
                Console.WriteLine(response);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PostAsync: {ex}");
                return false;
            }
        }

        public async Task<bool> PutAsync(Utilisateur? utilisateur)
        {
            if (utilisateur == null)
                return false;

            try
            {
                string url = $"{nomControleur}/{utilisateur.Idutilisateur}";
                var response = await httpClient.PutAsJsonAsync(url, utilisateur);

                if (response.IsSuccessStatusCode)
                    return true;

                Console.WriteLine($"PutAsync failed with status code: {response.StatusCode}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PutAsync: {ex}");
                return false;
            }
        }
        private static Dictionary<string, object> GetNonNullProperties(object obj)
        {
            var propertiesMap = new Dictionary<string, object>();

            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                if (value != null)
                {
                    propertiesMap.Add(property.Name, value);
                }
            }

            return propertiesMap;
        }
    }
}
