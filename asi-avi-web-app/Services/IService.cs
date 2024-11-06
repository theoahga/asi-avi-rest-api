namespace asi_avi_web_app.Services
{
    public interface IService<TEntity>
    {
        Task<List<TEntity?>> GetAllAsync(string? nomControleur);
        Task<TEntity?> GetByIdAsync(string? nomControleur, int? id);
        Task<TEntity?> GetByStringAsync(string? nomControleur,string? str);
        Task<bool> PostAsync(string? nomControleur,TEntity? str);
        Task<bool> PutAsync(string? nomControleur, TEntity? str);
        Task<bool> DeleteAsync(string? nomControleur, TEntity? str);
    }
}
