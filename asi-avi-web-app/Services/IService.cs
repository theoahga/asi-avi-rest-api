namespace asi_avi_web_app.Services
{
    public interface IService<TEntity>
    {
        Task<List<TEntity?>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int? id);
        Task<TEntity?> GetByEmailAsync(string? str);
        Task<bool> PostAsync(TEntity? str);
        Task<bool> PutAsync(TEntity? str);
        Task<bool> DeleteAsync(TEntity? str);
    }
}
