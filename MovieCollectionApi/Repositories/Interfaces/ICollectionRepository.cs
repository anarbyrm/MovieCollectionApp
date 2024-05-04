
using MovieCollectionApi.Models;

namespace MovieCollectionApi.Repository;

public interface ICollectionRepository
{
    List<Collection> GetAll();
    Collection? GetOneById(int Id);
    Collection? GetOneByTitle(string title);
    void Create(Collection collection);
    void Delete(int Id);
    void Update(int Id, Collection collection);
}