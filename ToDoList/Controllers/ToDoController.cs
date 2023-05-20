using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.IRepositories;
using Repositories.MSSQLRepositories;
using Repositories.Models;
using ToDoList.Models;

using ToDoList.Enums;
using ToDoList.Flags;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {
        private IToDoRepository _toDoRep;
        private ICategoryRepository _categoryRep;
        private List<ToDo> toDoList;
        private IEnumerable<Category> categoryList;

        //public ToDoController(IToDoRepository toDoRep, ICategoryRepository categoryRep)
        //{
        //    _toDoRep = toDoRep;
        //    _categoryRep = categoryRep;
        //}
        public ToDoController(RepositoryFactory factory)
        {
            _toDoRep = factory.ToDoRepCreate(DBSwitchFlag.Flag.ToString());
            _categoryRep = factory.CategoryCreate(DBSwitchFlag.Flag.ToString());
        }


        // GET: ToDoController
        public async Task<ActionResult> Index()
        {
            toDoList = (await _toDoRep.GetList()).ToList();
            categoryList = await _categoryRep.GetList();
            var toDoListModel = new ToDoListModel() { Categories = categoryList, ToDoList = toDoList };
            return View(toDoListModel);
        }


        [HttpPost]
        public async Task<ActionResult> Create(ToDo toDo)
        {
            _ = await _toDoRep.Create(toDo);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            _ = await _toDoRep.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> ChangeStatus(int id)
        {
            var toDo = await _toDoRep.Get(id);
            toDo.Status = (toDo.Status == "Completed") ? "In progress" : "Completed";
            _ = await _toDoRep.Update(toDo);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeFlag(DataBases flag)
        {
            DBSwitchFlag.Flag = flag;
            return RedirectToAction("Index");
        }
    }
}
