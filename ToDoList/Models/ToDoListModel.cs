using Repositories.Models;

namespace ToDoList.Models
{
    public class ToDoListModel
    {
        public IEnumerable<ToDo> ToDoList { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
