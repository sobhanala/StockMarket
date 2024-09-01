namespace api.DataAccess.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAllInQueryable();
        Task<T?> GetByTempEntityAsync(T tempModel);
        Task<T> AddAsync(T tempModel);
        Task<T?> UpdateAsync(T tempModel);
        Task<T?> DeleteAsync(T tempModel);
        Task<bool> IsEntityExistsAsync(T tempModel);
        Task SaveAsync();
    }
}