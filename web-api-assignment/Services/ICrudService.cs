namespace web_api_assignment.Services
{
    public interface ICrudService<T, ID>
    {
        Task<ICollection<T>> GetAllAsync();
        
        Task<T> GetByIdAsync(ID id);
        
        Task AddAsync(T entity);
        
        Task UpdateAsync(T entity);
        
        Task DeleteByIdAsync(ID id);
    }

}
