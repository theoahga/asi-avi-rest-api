using asi_avi_web_app.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace asi_avi_web_app.Services
{
    public class WSServiceUtilisateur : IService<Utilisateur>
    {
        private readonly HttpClient httpClient;

        public WSServiceUtilisateur()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7043/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<bool> DeleteAsync(string? nomControleur, Utilisateur? utilisateur)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Utilisateur?>> GetAllAsync(string? nomControleur)
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

        public Task<Utilisateur?> GetByIdAsync(string? nomControleur, int? id)
        {
            throw new NotImplementedException();
        }

        public Task<Utilisateur?> GetByStringAsync(string? nomControleur, string? str)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostAsync(string? nomControleur, Utilisateur? str)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutAsync(string? nomControleur, Utilisateur? str)
        {
            throw new NotImplementedException();
        }
    }
}
