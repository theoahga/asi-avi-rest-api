using asi_avi_web_app.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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

        public async Task<Utilisateur?> GetByIdAsync( int? id)
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
            if (utilisateur == null)
                return false;

            try
            {
                var response = await httpClient.PostAsJsonAsync(nomControleur, utilisateur);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in PostAsync: {ex.Message}");
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


    }
}
