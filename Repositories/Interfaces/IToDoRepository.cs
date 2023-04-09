using Repositories.Models;

namespace Repositories.IRepositories
{
    public interface IToDoRepository
    {
        Task<ToDo> Create(ToDo toDo);
        Task<ToDo> Update(ToDo toDo);
        Task<bool> Delete(int id);
        Task<ToDo> Get(int id);
        Task<IEnumerable<ToDo>> GetList();
    }
}