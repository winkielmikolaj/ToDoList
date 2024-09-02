using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.Core.ViewModels;
using ToDoList.Persistance.Repository;

namespace ToDoList.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private TaskRepository _taskRepository = new TaskRepository();

        public IActionResult Tasks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = _taskRepository.Get(userId);
            var categories = _taskRepository.GetCategories();

           //5minuta filmu

            return View();
        }
    }
}
