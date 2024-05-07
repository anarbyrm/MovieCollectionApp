
using MovieCollectionApi.Models;

namespace MovieCollectionApi.Repository;

public interface ICollectionRepository
{
    List<Collection> GetAll();
    Collection? GetOneById(int Id);
    Collection? GetOneByTitle(string title);
    bool Create(Collection newCollection);
    bool Delete(Collection collection);
    bool Update(Collection updatedCollection);
    bool Save();
}