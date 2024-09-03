using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.Core.Models;
using ToDoList.Core.ViewModels;
using ToDoList.Persistance.Extensions;
using ToDoList.Persistance.Repository;

using Task = ToDoList.Core.Models.Domains.Task;

namespace ToDoList.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private TaskRepository _taskRepository = new TaskRepository();

        public IActionResult Tasks()
        {
            var userId = User.GetUserId();


            var vm = new TasksViewModel
            {
                FilterTasks = new FilterTasks(),
                Tasks = (IEnumerable<Core.Models.Domains.Task>)_taskRepository.Get(userId),
                Categories = (IEnumerable<Core.Models.Domains.Category>)_taskRepository.GetCategories()
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult Tasks(TasksViewModel viewModel)
        {
            var userId = User.GetUserId();

            var tasks = _taskRepository.Get(userId,
                viewModel.FilterTasks.IsExecuted,
                viewModel.FilterTasks.CategoryId,
                viewModel.FilterTasks.Title);


            return PartialView("_TasksTable", tasks);
        }

        public IActionResult Tasks(int id = 0)
        {
            var userId = User.GetUserId();

            var task = id == 0 ?
                new Task { Id = 0, UserId = userId, Term = DateTime.Today } :
                _taskRepository.Get(id, userId);

            var vm = new TaskViewModel
            {
                Task = task,
                Heading = id == 0 ?
                "Dodawanie nowego zadania" : "Edytowanie zadania",
                Categories = (IEnumerable<Core.Models.Domains.Category>)_taskRepository.GetCategories()
            };


            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Tasks(Task task)
        {
            var userId = User.GetUserId();

            task.UserId = userId;

            if (ModelState.IsValid)
            {
                var vm = new TaskViewModel
                {
                    Task = task,
                    Heading = task.Id == 0 ?
                "Dodawanie nowego zadania" : "Edytowanie zadania",
                    Categories = (IEnumerable<Core.Models.Domains.Category>)_taskRepository.GetCategories()
                };

                return View("Task", vm);
            }

            if (task.Id == 0)
            {
                _taskRepository.Add(task);
            }
            else
            {
                _taskRepository.Update(task);
            }

            return RedirectToAction("Task");
        }


        [HttpPost]
        public IActionResult Finish(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskRepository.Finish(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie do pliku
                return Json(new { succes = false, error = ex.Message });
            }

            return Json(new { succes = true });
        }
    }
}
