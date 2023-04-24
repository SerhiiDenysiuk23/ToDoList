using Microsoft.AspNetCore.Mvc;
using ToDoList.Enums;
using ToDoList.Flags;

namespace ToDoList.Controllers
{
    public class LayoutController : Controller
    {
        [HttpPost]
        public IActionResult ChangeFlag(DataBases flag) 
        {
            DBSwitchFlag.Flag = flag;
            return RedirectToRoute("/");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
