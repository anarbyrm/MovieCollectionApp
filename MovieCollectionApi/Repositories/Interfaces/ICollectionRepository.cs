
using MovieCollectionApi.Models;

namespace MovieCollectionApi.Repository;

public interface ICollectionRepository
{
    Task<List<Collection>> GetAllAsync();
    Task<Collection?> GetOneByIdAsync(int id);
    Task<Collection?> GetOneByTitleAsync(string title);
    Task<bool> CreateAsync(Collection newCollection);
    Task<bool> DeleteAsync(Collection collection);
    Task<bool> UpdateAsync(Collection updatedCollection);
    Task<bool> SaveAsync();
}