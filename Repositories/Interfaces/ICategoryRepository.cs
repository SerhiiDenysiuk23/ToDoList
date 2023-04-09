using Repositories.Models;

namespace Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<Category> Create(Category category);
        Task<bool> Delete(int id);
        Task<Category> Get(int id);
        Task<IEnumerable<Category>> GetList();
    }
}
