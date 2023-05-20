using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.IRepositories;
using Repositories.Models;
using ToDoList.Flags;

namespace ToDoList.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository _repository;
        //public CategoryController(ICategoryRepository repository)
        //{
        //    _repository = repository;
        //}
        public CategoryController(RepositoryFactory factory)
        {
            _repository = factory.CategoryCreate(DBSwitchFlag.Flag.ToString());
        }

        public async Task<IActionResult> Index()
        {
            var categoryList = await _repository.GetList();
            return View(categoryList);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            _ = await _repository.Create(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            _ = await _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
